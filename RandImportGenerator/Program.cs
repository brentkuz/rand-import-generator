﻿using RandImportGenerator.Core;
using RandImportGenerator.Core.Logic.Builders;
using RandImportGenerator.Core.Logic.FileWriters;
using RandImportGenerator.Core.Objects.ImportDefinitions.Columns;
using RandImportGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;


namespace RandImportGenerator
{
    class Program
    {
        /*** File Config Example *************************************************
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
                    RandomizationOptions = new string[] {"opt,1", "opt2", "opt3"}
                },
                new DependentColumn("Dependent1")
                {
                    ColumnOrder = 3,
                    DependsOn = "Randomized1",
                    Map = new Dictionary<object, string>()
                            {
                                { "opt,1", "Option 1" },
                                { "opt2", "Option 2" },
                                { "opt3", "Option 3" },
                            }
                },
                new DependentColumn("Dependent2")
                {
                    ColumnOrder = 3,
                    DependsOn = "Randomized1",
                    Calculator = Dependent1Calc
                },
                new StaticColumn("Static1")
                {
                    ColumnOrder = 4,
                    Value = "static"
                }
        };
        var delimiter = ',';
        var quote = '"';
        var rowCount = 100000;
        ******************************************************************/


        static void Main(string[] args)
        {
            //init
            var container = new UnityContainerFactory().GetContainer();
            var bldrFactory = container.Resolve<IImportBuilderFactory>();
            var writer = container.Resolve<IWriter>();

            /*** File Config *************************************************/
            var fileType = FileType.CSV;
            var outputPath = @"C:\test2\DrugItems.csv";
            var columns = new ColumnDefinitionBase[]
            {
                new AutoIncrementedColumn("DrugUnitID")
                {
                    ColumnOrder = 1,
                    StartingSequenceNumber = 100000,
                    IncrementValue = 1
                },
                new AutoIncrementedColumn("PickNr")
                {
                    ColumnOrder = 2,
                    StartingSequenceNumber = 100000,
                    IncrementValue = 1
                },
                new RandomizedColumn("DrugCode")
                {
                    ColumnOrder = 3,
                    RandomizationOptions = new string[] {"A", "B", "C"}
                },
                new DependentColumn("Dependent2")
                {
                    ColumnOrder = 3,
                    DependsOn = "DrugCode",
                    Calculator = (object val) => val.ToString().ToLower()
                },
            };
            var delimiter = ',';
            var quote = QuoteType.Double;
            var rowCount = 20000;
            /******************************************************************/


            var bldr = bldrFactory.GetImportBuilder(fileType);
            if(writer is FileWriter)
                ((FileWriter)writer).OutputPath = outputPath;
            bldr.SetWriter(writer);
            
        
            foreach(var col in columns)
            {
                bldr.AddColumn(col);
            }

            if (bldr is DelimitedImportBuilder)
                (bldr as DelimitedImportBuilder).SetDelimiter(delimiter);

            if (bldr is CSVImportBuilder)
                (bldr as CSVImportBuilder).SetQuoteCharacter(quote);

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
            { "opt,1", "Option 1" },
            { "opt2", "Option 2" },
            { "opt3", "Option 3" },
        };
        public static string Dependent1Calc(object val)
        {
            return dep1Map[val.ToString()];
        }
        
        /************************************************************************************/
    }
}
