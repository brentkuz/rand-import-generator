using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandImportGenerator.Objects.ImportDefinitions;
using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Objects.ImportDefinitions.Columns;

namespace RandImportGenerator.Logic.Builders
{
    public class CSVImportBuilder : DelimitedImportBuilder
    {
        public CSVImportBuilder(IFileWriter fileWriter) : base("csv", fileWriter)
        {
            definition = new CSVImportDefinition();
        }

        public virtual void SetQuoteCharacter(char? quoteChar)
        {
            var csvDef = definition as CSVImportDefinition;
            csvDef.QuoteCharacter = quoteChar;
        }

        public override void BuildAndSaveFile()
        {
            var csvDef = definition as CSVImportDefinition;

            var file = new StringBuilder();
            var rand = new Random();
            var cols = csvDef.Columns.OrderBy(x => x.ColumnOrder).ToArray();
            var colLen = cols.Count();

            //header row
            var hdr = "";
            for(var i = 0; i < colLen; i++)
            {                
                hdr += cols[i].Name;
                if (i < colLen - 1)
                    hdr += csvDef.Delimiter;
            }
            file.AppendLine(hdr);

            //data rows
            for(var i = 0; i < rowCount; i++)
            {
                var row = "";
                var rowCache = new Dictionary<string, object>();
                for (var j = 0; j < colLen; j++)
                {
                    var c = cols[j];
                    string temp;
                    switch(c.Type)
                    {
                        case ColumnType.AutoIncremented:
                            temp = CalculateIncremented(c as AutoIncrementedColumn, rowCache);
                            break;
                        case ColumnType.Randomized:
                            temp = CalculateRandomized(c as RandomizedColumn, rand, rowCache);
                            break;
                        case ColumnType.Dependent:
                            DependentColumn cDep = c as DependentColumn;
                            temp = CalculateDependent(cDep, rowCache, rowCache[cDep.DependsOn]);
                            break;
                        case ColumnType.Static:
                            temp = CalculateStatic(c as StaticColumn, rowCache);
                            break;
                        case ColumnType.Computed:
                            temp = CalculateComputed(c as ComputedColumn, rowCache, DateTime.Now, 1);
                            break;
                        default:
                            throw new KeyNotFoundException(string.Format("Column Type {0} not supported", c.Type));
                    }

                    //wrap in quotes if contains set delimiter
                    if(temp.Contains(csvDef.Delimiter))
                    {
                        temp = csvDef.QuoteCharacter + temp + csvDef.QuoteCharacter;                        
                    }

                    row += temp;

                    if (j < colLen - 1)
                        row += csvDef.Delimiter;
                }
                file.AppendLine(row);
            }

            //write to file
            fileWriter.Write(outputPath, file.ToString());
        }



    }
}
