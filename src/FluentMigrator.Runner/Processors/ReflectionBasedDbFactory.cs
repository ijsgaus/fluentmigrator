namespace FluentMigrator.Runner.Processors
{
    using System;
    using System.Data.Common;

    public class ReflectionBasedDbFactory : DbFactoryBase
    {
        private readonly string assemblyName;
        private readonly string dbProviderFactoryTypeName;

        public ReflectionBasedDbFactory(string assemblyName, string dbProviderFactoryTypeName)
        {
            this.assemblyName = assemblyName;
            this.dbProviderFactoryTypeName = dbProviderFactoryTypeName;
        }

        protected override DbProviderFactory CreateFactory()
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyName);
            var type = assembly.GetType(dbProviderFactoryTypeName);

            return (DbProviderFactory) Activator.CreateInstance(type);
        }
    }
}