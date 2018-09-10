using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandImportGenerator.Logic.Builders;
using Moq;
using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Objects.ImportDefinitions.Columns;
using RandImportGenerator.Objects.ImportDefinitions;
using RandImportGenerator.Test.Fakes;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using RandImportGenerator.Utility.Validation;
using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Test.Logic.Builders
{
    [TestClass]
    [TestCategory("CSVImportBuilder_Unit")]
    public class UnitTest1
    {       
        [TestInitialize]
        public void Init()
        {

        }

        #region SetOutputPath
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetOutputPath_IncorrectFileExtension()
        {
            var path = @"C:\test\test.txt";

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock.Setup(f => f.DirectoryExists(It.IsAny<string>())).Returns(true);
            var validationMock = new Mock<IValidationHelper>();
            var bldr = new CSVImportBuilder(fileWriterMock.Object, validationMock.Object);

            bldr.SetOutputPath(path);
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void SetOutputPath_InvalidDirectory()
        {
            var path = @"C:\no_dir\test.csv";

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock.Setup(f => f.DirectoryExists(It.IsAny<string>())).Returns(false);
            var validationMock = new Mock<IValidationHelper>();
            var bldr = new CSVImportBuilder(fileWriterMock.Object, validationMock.Object);

            bldr.SetOutputPath(path);
        }

        [TestMethod]
        public void SetOutputPath_Success()
        {
            var path = @"C:\test\test.csv";

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock.Setup(f => f.DirectoryExists(It.IsAny<string>())).Returns(true);
            var validationMock = new Mock<IValidationHelper>();
            var bldr = new CSVImportBuilder(fileWriterMock.Object, validationMock.Object);

            bldr.SetOutputPath(path);

            Assert.AreEqual(path, bldr.OutputPath);
        }

        #endregion

        #region AddColumn

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddColumn_DuplicateColumn()
        {
            var bldr = GetBasicBuilder();

            bldr.AddColumn(new StaticColumn("col1"));
            bldr.AddColumn(new StaticColumn("col1"));
        }

        [TestMethod]
        public void AddColumn_AutoIncrementedColumn_CreatesColumnSequenceEntry()
        {
            var bldr = GetBasicBuilder();

            bldr.AddColumn(new AutoIncrementedColumn("col1"));

            Assert.IsTrue(bldr.ColumnSequenceExists("col1"));
        }

        [TestMethod]
        public void AddColumn_RandomizedColumn_CreatesRandOptionsEntry()
        {
            var bldr = GetBasicBuilder();

            bldr.AddColumn(new RandomizedColumn("col1"));

            Assert.IsTrue(bldr.RandOptionsExist("col1"));
        }

        [TestMethod]
        public void AddColumn_RandomizedColumn_Success()
        {
            var bldr = GetBasicBuilder();

            bldr.AddColumn(new RandomizedColumn("col1"));

            Assert.IsTrue(bldr.ColumnExists("col1"));
        }
        #endregion

        #region SetRowCount

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRowCount_NegativeCount()
        {
            var bldr = GetBasicBuilder();

            bldr.SetRowCount(-1);
        }

        [TestMethod]
        public void SetRowCount_Success()
        {
            var bldr = GetBasicBuilder();

            bldr.SetRowCount(100);

            Assert.AreEqual(100, bldr.Definition.RowCount);
        }

        #endregion

        #region SetQuoteCharacter

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetQuoteCharacter_InvalidCharacter()
        {
            var bldr = GetBasicBuilder();

            bldr.SetQuoteCharacter('?');
        }

        [TestMethod]
        public void SetQuoteCharacter_Success()
        {
            var bldr = GetBasicBuilder();

            bldr.SetQuoteCharacter('"');

            Assert.AreEqual('"', (bldr.Definition as CSVImportDefinition).QuoteCharacter);
        }

        #endregion

        #region BuildAndSaveFile
        private Dictionary<object, string> depMap = new Dictionary<object, string>()
        {
            { "opt1", "Option 1" },
            { "opt2", "Option 2" },
            { "opt3", "Option 3" },
        };
        public string Dependent1Calc(object val)
        {
            return depMap[val.ToString()];
        }
        public string Computed1Calc(params object[] args)
        {
            var date = Convert.ToDateTime(args[0]);
            var addDays = Convert.ToInt16(args[1]);
            return date.AddDays(addDays).ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BuildAndSaveFile_IncompleteModelValidationError()
        {
            var fileWriter = new TestFileWriter();
            var validationMock = new Mock<IValidationHelper>();
            var res = new List<ValidationResult>() as ICollection<ValidationResult>;
            validationMock.Setup(x => x.IsModelValid(It.IsAny<object>(), out res)).Returns(false);
            var bldr = new CSVImportBuilder(fileWriter, validationMock.Object);
            var start = 1;
            var increment = 2;


            bldr.SetOutputPath("C:\test\test.csv");
            bldr.AddColumn(new AutoIncrementedColumn("col1")
            {
                ColumnOrder = 1,
                StartingSequenceNumber = start,
                IncrementValue = increment
            });

            bldr.BuildAndSaveFile();
        }

        [TestMethod]
        public void BuildAndSaveFile_AutoIncrementIsCorrect()
        {
            var fileWriter = new TestFileWriter();
            var validationMock = new Mock<IValidationHelper>();
            var res = new List<ValidationResult>() as ICollection<ValidationResult>;
            validationMock.Setup(x => x.IsModelValid(It.IsAny<object>(), out res)).Returns(true);
            var bldr = new CSVImportBuilder(fileWriter, validationMock.Object);
            var start = 1;
            var increment = 2;
            

            bldr.SetOutputPath("C:\test\test.csv");
            bldr.AddColumn(new AutoIncrementedColumn("col1")
            {
                ColumnOrder = 1,
                StartingSequenceNumber = start,
                IncrementValue = increment
            });
            bldr.SetDelimiter(',');
            bldr.SetRowCount(4);
            bldr.BuildAndSaveFile();

            var file = fileWriter.InMemoryFileContents;

            using (StringReader rdr = new StringReader(file))
            {
                rdr.ReadLine();
                string line = rdr.ReadLine();
                int prev = Convert.ToInt32(line);
                while((line = rdr.ReadLine()) != null)
                {
                    var curr = Convert.ToInt32(line);
                    if (curr != (prev + increment))
                        throw new Exception();
                    prev = curr;
                }
            }
        }

        [TestMethod]
        public void BuildAndSaveFile_RandomizedIsCorrect()
        {
            var fileWriter = new TestFileWriter();
            var validationMock = new Mock<IValidationHelper>();
            var res = new List<ValidationResult>() as ICollection<ValidationResult>;
            validationMock.Setup(x => x.IsModelValid(It.IsAny<object>(), out res)).Returns(true);
            var bldr = new CSVImportBuilder(fileWriter, validationMock.Object);

            var opts = new string[] { "opt1", "opt2", "opt3" };


            bldr.SetOutputPath("C:\test\test.csv");
            bldr.AddColumn(new RandomizedColumn("col1")
            {
                ColumnOrder = 1,
                RandomizationOptions = opts

            });
            bldr.SetDelimiter(',');
            bldr.SetRowCount(4);
            bldr.BuildAndSaveFile();

            var file = fileWriter.InMemoryFileContents;

            using (StringReader rdr = new StringReader(file))
            {
                rdr.ReadLine();
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    if (!opts.Any(x => x == line))
                        throw new Exception();
              
                }
            }
        }

        [TestMethod]
        public void BuildAndSaveFile_DependentUsingMapIsCorrect()
        {
            var fileWriter = new TestFileWriter();
            var validationMock = new Mock<IValidationHelper>();
            var res = new List<ValidationResult>() as ICollection<ValidationResult>;
            validationMock.Setup(x => x.IsModelValid(It.IsAny<object>(), out res)).Returns(true);
            var bldr = new CSVImportBuilder(fileWriter, validationMock.Object);

            var delimiter = ',';

            var opts = new string[] { "opt1", "opt2", "opt3" }; 

            bldr.SetOutputPath("C:\test\test.csv");
            bldr.AddColumn(new RandomizedColumn("col1")
            {
                ColumnOrder = 1,
                RandomizationOptions = opts

            });
            bldr.AddColumn(new DependentColumn("col2")
            {
                ColumnOrder = 1,
                DependsOn = "col1",
                Map = depMap
            });
            bldr.SetDelimiter(delimiter);
            bldr.SetRowCount(4);
            bldr.BuildAndSaveFile();

            var file = fileWriter.InMemoryFileContents;

            using (StringReader rdr = new StringReader(file))
            {
                rdr.ReadLine();
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    var parts = line.Split(delimiter);

                    if (parts[1] != depMap[parts[0]])
                        throw new Exception();
                }
            }
        }

        [TestMethod]
        public void BuildAndSaveFile_DependentUsingCalculatorIsCorrect()
        {
            var fileWriter = new TestFileWriter();
            var validationMock = new Mock<IValidationHelper>();
            var res = new List<ValidationResult>() as ICollection<ValidationResult>;
            validationMock.Setup(x => x.IsModelValid(It.IsAny<object>(), out res)).Returns(true);

            var bldr = new CSVImportBuilder(fileWriter,validationMock.Object);

            var delimiter = ',';

            var opts = new string[] { "opt1", "opt2", "opt3" };     

            bldr.SetOutputPath("C:\test\test.csv");
            bldr.AddColumn(new RandomizedColumn("col1")
            {
                ColumnOrder = 1,
                RandomizationOptions = opts

            });
            bldr.AddColumn(new DependentColumn("col2")
            {
                ColumnOrder = 1,
                DependsOn = "col1",
                Calculator = Dependent1Calc
            });
            bldr.SetDelimiter(delimiter);
            bldr.SetRowCount(4);
            bldr.BuildAndSaveFile();

            var file = fileWriter.InMemoryFileContents;

            using (StringReader rdr = new StringReader(file))
            {
                rdr.ReadLine();
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    var parts = line.Split(delimiter);

                    if (parts[1] != depMap[parts[0]])
                        throw new Exception();
                }
            }
        }
              


        #endregion

        private CSVImportBuilder GetBasicBuilder()
        {
            var fileWriterMock = new Mock<IFileWriter>();
            var validationMock = new Mock<IValidationHelper>();
            return new CSVImportBuilder(fileWriterMock.Object, validationMock.Object);
        }
    }
}
