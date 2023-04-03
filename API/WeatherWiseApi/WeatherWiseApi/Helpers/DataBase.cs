using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System;

namespace WeatherWiseApi.Helpers
{
    public class DataBase : IDisposable
    {
        /// <summary>
        /// Instância da base de dados.
        /// </summary>
        public Database db;

        /// <summary>
        /// Comando executado.
        /// </summary>
        public DbCommand databaseCommand;

        /// <summary>
        /// Busca a configuração padrão da Web Config.
        /// </summary>
        public DataBase()
        {
            this.db = new DatabaseProviderFactory(new SystemConfigurationSource()).CreateDefault();
        }

        public void Dispose()
        {
            if (this.databaseCommand != null && this.databaseCommand.Connection != null)
            {
                this.databaseCommand.Connection.Close();
                this.databaseCommand.Connection.Dispose();
            }

            if (this.databaseCommand != null)
            {
                this.databaseCommand.Dispose();
            }
        }
    }
}
