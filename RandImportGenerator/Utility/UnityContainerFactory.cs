using RandImportGenerator.Core.Logic.FileWriters;
using Unity;
using RandImportGenerator.Core.Logic.Builders;
using RandImportGenerator.Core.Utility.Validation;

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
            container.RegisterType<IValidationHelper, ModelValidationHelper>();
            container.RegisterType<IImportBuilderFactory, ImportBuilderFactory>();

        }
    }
}
