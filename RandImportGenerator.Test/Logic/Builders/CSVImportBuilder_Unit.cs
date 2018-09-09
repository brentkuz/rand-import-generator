using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandImportGenerator.Logic.Builders;
using Moq;
using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Objects.ImportDefinitions.Columns;

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
            var bldr = new CSVImportBuilder(fileWriterMock.Object);

            bldr.SetOutputPath(path);
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void SetOutputPath_InvalidDirectory()
        {
            var path = @"C:\no_dir\test.csv";

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock.Setup(f => f.DirectoryExists(It.IsAny<string>())).Returns(false);
            var bldr = new CSVImportBuilder(fileWriterMock.Object);

            bldr.SetOutputPath(path);
        }

        [TestMethod]
        public void SetOutputPath_Success()
        {
            var path = @"C:\test\test.csv";

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock.Setup(f => f.DirectoryExists(It.IsAny<string>())).Returns(true);
            var bldr = new CSVImportBuilder(fileWriterMock.Object);

            bldr.SetOutputPath(path);

            Assert.AreEqual(path, bldr.OutputPath);
        }

        #endregion

        #region AddColumn

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddColumn_DuplicateColumn()
        {
            var fileWriterMock = new Mock<IFileWriter>();
            var bldr = new CSVImportBuilder(fileWriterMock.Object);

            bldr.AddColumn(new StaticColumn("col1"));
            bldr.AddColumn(new StaticColumn("col1"));
        }

        [TestMethod]
        public void AddColumn_AutoIncrementedColumn_CreatesColumnSequenceEntry()
        {
            var fileWriterMock = new Mock<IFileWriter>();
            var bldr = new CSVImportBuilder(fileWriterMock.Object);

            bldr.AddColumn(new AutoIncrementedColumn("col1"));

            Assert.IsTrue(bldr.ColumnSequenceExists("col1"));
        }

        [TestMethod]
        public void AddColumn_RandomizedColumn_CreatesRandOptionsEntry()
        {
            var fileWriterMock = new Mock<IFileWriter>();
            var bldr = new CSVImportBuilder(fileWriterMock.Object);

            bldr.AddColumn(new RandomizedColumn("col1"));

            Assert.IsTrue(bldr.RandOptionsExist("col1"));
        }

        #endregion


        private CSVImportBuilder GetBasicBuilder()
        {
            var fileWriterMock = new Mock<IFileWriter>();
            return new CSVImportBuilder(fileWriterMock.Object);
        }
    }
}
