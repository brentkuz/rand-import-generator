using RandImportGenerator.Logic;
using RandImportGenerator.Objects.ImportDefinitions;
using RandImportGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;


namespace RandImportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //init
            var container = new UnityContainerFactory().GetContainer();
            var bldrFactory = container.Resolve<IImportBuilderFactory>();

            /*** File Config *************************************************/
            var fileType = FileType.CSV;
            var outputPath = @"C:\test\test.csv";
            var columns = new ColumnDefinitionBase[]
            {
                new AutoIncrementedColumn("Incremented1")
                {
                    ColumnOrder = 1,
                    StartingSequenceNumber = 1000,
                    IncrementValue = 1
                },
                new RandomizedColumn("Randomized1")
                {
                    ColumnOrder = 2,
                    RandomizationOptions = new string[] {"opt1", "opt2", "opt3"}
                },
                new DependentColumn("Dependent1")
                {
                    ColumnOrder = 3,
                    DependsOn = "Randomized1",
                    Calculator = Dependent1Calc
                },
                new StaticColumn("Static1")
                {
                    ColumnOrder = 4,
                    Value = "static"
                },
                new ComputedColumn("Computed1")
                {
                    ColumnOrder = 5,
                    Calculator = Computed1Calc
                }
            };
            var delimiter = ',';
            var rowCount = 10000;
            /******************************************************************/


            var bldr = bldrFactory.GetImportBuilder(fileType);

            bldr.SetOutputPath(outputPath);
        
            foreach(var col in columns)
            {
                bldr.AddColumn(col);
            }

            if (bldr is DelimitedImportBuilder)
                (bldr as DelimitedImportBuilder).SetDelimiter(delimiter);

            bldr.SetRowCount(rowCount);


            var st = new Stopwatch();
            st.Start();

            bldr.BuildAndSaveFile();

            st.Stop();
            Console.WriteLine(st.Elapsed);

            Console.ReadKey();
        }



        /*** Delegate Config ****************************************************************/
        private static Dictionary<string, string> dep1Map = new Dictionary<string, string>()
        {
            { "opt1", "Option 1" },
            { "opt2", "Option 2" },
            { "opt3", "Option 3" },
        };
        public static string Dependent1Calc(object val)
        {
            return dep1Map[val.ToString()];
        }
        public static string Computed1Calc(params object[] args)
        {
            var date = Convert.ToDateTime(args[0]);
            var addDays = Convert.ToInt16(args[1]);
            return date.AddDays(addDays).ToString();
        }
        /************************************************************************************/
    }
}
