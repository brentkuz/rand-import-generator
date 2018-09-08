using RandImportGenerator.Logic.FileWriters;
using Unity;
using RandImportGenerator.Logic.Builders;

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
