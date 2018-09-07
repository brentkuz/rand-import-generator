using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Objects;
using RandImportGenerator.Objects.ImportDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Logic
{
    public abstract class ImportBuilderBase
    {
        protected readonly string fileExtension;
        protected ImportDefinition definition = new ImportDefinition();
        protected string outputPath;
        protected int rowCount;
        protected Dictionary<string, int> columnSequence = new Dictionary<string, int>();
        protected IFileWriter fileWriter;

        public ImportBuilderBase(string fileExtension, IFileWriter fileWriter)
        {
            this.fileExtension = fileExtension;
            this.fileWriter = fileWriter;
        }

        public virtual void SetOutputPath(string path)
        {
            //check for correct file extension
            var parts = path.Split('\\');
            var filename = parts[parts.Length - 1];
            var filenameParts = filename.Split('.');

            if (filenameParts[filenameParts.Length - 1].ToLower() != fileExtension.ToLower())
                throw new ArgumentException("Incompatible file extension");

            outputPath = path;
        }

        public virtual void AddColumn(ColumnDefinitionBase col)
        {
            if (col is AutoIncrementedColumn)
                columnSequence.Add(col.Name, 0);

            definition.Columns.Add(col);
        }

        public virtual void SetRowCount(int count)
        {
            if (count < 0)
                throw new ArgumentException("Row Count must be a positive number.");
            rowCount = count;
        }

        public abstract void BuildAndSaveFile();



        protected virtual string CalculateIncremented(AutoIncrementedColumn col, Dictionary<string, object> cache)
        {
            int sequence = columnSequence[col.Name]++;
            var t = (col.StartingSequenceNumber + (sequence * col.IncrementValue)).ToString();
            cache.Add(col.Name, t);
            return t;
        }

        //options cache
        private Dictionary<string, string[]> randOpts = new Dictionary<string, string[]>();
        protected virtual string CalculateRandomized(RandomizedColumn col, Random rand, Dictionary<string, object> cache)
        {
            var opts = col.RandomizationOptions;
            //add to cache
            if (!randOpts.Keys.Contains(col.Name))
                randOpts.Add(col.Name, opts);

            var idx = rand.Next(0, opts.Length);

            var t = opts[idx];

            cache.Add(col.Name, t);
            return t;
        }

        protected virtual string CalculateDependent(DependentColumn col, Dictionary<string, object> cache, object val)
        {
            var t = col.Calculator(val);
            cache.Add(col.Name, t);
            return t;
        }

        protected virtual string CalculateStatic(StaticColumn col, Dictionary<string, object> cache)
        {
            var t = col.Value;
            cache.Add(col.Name, t);
            return t;
        }

        protected virtual string CalculateComputed(ComputedColumn col, Dictionary<string, object> cache, params object[] args)
        {
            var t = col.Calculator(args);
            cache.Add(col.Name, t);
            return t;
        }
    }

  
}
