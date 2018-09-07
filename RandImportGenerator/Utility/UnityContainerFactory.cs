using RandImportGenerator.Logic;
using RandImportGenerator.Logic.FileWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Microsoft.Practices.Unity;

namespace RandImportGenerator.Utility
{
    public class UnityContainerFactory
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();
            Register(container);
            return container;
        }

        private void Register(IUnityContainer container)
        {
            container.RegisterType<IFileWriter, FileWriter>();
            container.RegisterType<IImportBuilderFactory, ImportBuilderFactory>();
        }
    }
}
