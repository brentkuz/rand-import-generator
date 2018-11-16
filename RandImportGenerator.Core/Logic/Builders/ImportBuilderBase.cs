using RandImportGenerator.Core.Logic.FileWriters;
using RandImportGenerator.Core.Objects.ImportDefinitions;
using RandImportGenerator.Core.Objects.ImportDefinitions.Columns;
using RandImportGenerator.Core.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandImportGenerator.Core.Logic.Builders
{
    public abstract class ImportBuilderBase
    {
        private Dictionary<string, string[]> randOptions = new Dictionary<string, string[]>();

        protected readonly string fileExtension;
        protected ImportDefinition definition;
        protected string outputPath;        
        protected Dictionary<string, int> columnSequence = new Dictionary<string, int>();
        protected IFileWriter fileWriter;
        protected IValidationHelper validation;

        public ImportBuilderBase(string fileExtension, IFileWriter fileWriter, IValidationHelper validation)
        {
            this.fileExtension = fileExtension;
            this.fileWriter = fileWriter;
            this.validation = validation;
        }

        public ImportDefinition Definition { get { return definition; } }

        public string OutputPath { get { return outputPath; } }

        public virtual void SetOutputPath(string path)
        {
            if (!fileWriter.DirectoryExists(path))
                throw new SystemException(string.Format("The path {0} does not exist.", path));

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
            if (ColumnExists(col.Name))
                throw new Exception(string.Format("A column with the name {0} already exists in the column collection", col.Name));

            if (col is AutoIncrementedColumn && !ColumnSequenceExists(col.Name))
                columnSequence.Add(col.Name, 0);

            if(col is RandomizedColumn)
            {
                if (!randOptions.Keys.Contains(col.Name))
                    randOptions.Add(col.Name, (col as RandomizedColumn).RandomizationOptions);
            }

            definition.Columns.Add(col);
        }

        public virtual void SetRowCount(int count)
        {
            if (count < 0)
                throw new ArgumentException("Row Count must be a positive number.");
            definition.RowCount = count;
        }

        public virtual bool ColumnSequenceExists(string colName)
        {
            return columnSequence.Keys.Contains(colName);
        }

        public virtual bool RandOptionsExist(string colName)
        {
            return randOptions.Keys.Contains(colName);
        }

        public virtual bool ColumnExists(string colName)
        {
            return definition.Columns.Any(x => x.Name == colName);
        }

        public abstract void BuildAndSaveFile();

        protected virtual string CalculateIncremented(AutoIncrementedColumn col, Dictionary<string, object> cache)
        {
            int sequence = columnSequence[col.Name]++;
            var t = (col.StartingSequenceNumber + (sequence * col.IncrementValue)).ToString();
            cache.Add(col.Name, t);
            return t;
        }
        
        protected virtual string CalculateRandomized(RandomizedColumn col, Random rand, Dictionary<string, object> cache)
        {
            var opts = randOptions[col.Name];

            var idx = rand.Next(0, opts.Length);
            var t = opts[idx];
            cache.Add(col.Name, t);

            return t;
        }

        protected virtual string CalculateDependent(DependentColumn col, Dictionary<string, object> cache, object val)
        {
            string t;

            if (col.Calculator != null)
            {
                t = col.Calculator(val);
            }
            else
            {
                t = col.Map[val];
            }

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

        protected void CheckDefinitionType(Type type, string message)
        {
            if (definition.GetType() != type && !definition.GetType().IsSubclassOf(type))
                throw new InvalidCastException(message);
        }

        
    }

  
}
