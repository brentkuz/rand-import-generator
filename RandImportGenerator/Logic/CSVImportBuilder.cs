using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandImportGenerator.Objects;
using RandImportGenerator.Objects.ImportDefinitions;
using RandImportGenerator.Logic.FileWriters;

namespace RandImportGenerator.Logic
{
    public class CSVImportBuilder : DelimitedImportBuilder
    {
        public CSVImportBuilder(IFileWriter fileWriter) : base("csv", fileWriter)
        {

        }

        

        public override void BuildAndSaveFile()
        {            
            var file = new StringBuilder();
            var rand = new Random();
            var cols = definition.Columns.OrderBy(x => x.ColumnOrder).ToArray();
            var colLen = cols.Count();
            //header row
            var hdr = "";
            for(var i = 0; i < colLen; i++)
            {                
                hdr += cols[i].Name;
                if (i < colLen - 1)
                    hdr += delimiter;
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
                    switch(c.Type)
                    {
                        case ColumnType.AutoIncremented:
                            row += CalculateIncremented(c as AutoIncrementedColumn, rowCache);
                            break;
                        case ColumnType.Randomized:
                            row += CalculateRandomized(c as RandomizedColumn, rand, rowCache);
                            break;
                        case ColumnType.Dependent:
                            DependentColumn cDep = c as DependentColumn;
                            row += CalculateDependent(cDep, rowCache, rowCache[cDep.DependsOn]);
                            break;
                        case ColumnType.Static:
                            row += CalculateStatic(c as StaticColumn, rowCache);
                            break;
                        case ColumnType.Computed:
                            row += CalculateComputed(c as ComputedColumn, rowCache, DateTime.Now, 1);
                            break;
                        default:
                            throw new KeyNotFoundException(string.Format("Column Type {0} not supported", c.Type));
                    }
                    if (j < colLen - 1)
                        row += delimiter;
                }
                file.AppendLine(row);
            }

            //write to file
            fileWriter.Write(outputPath, file.ToString());
        }



    }
}
