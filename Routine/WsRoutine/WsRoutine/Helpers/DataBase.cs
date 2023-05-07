using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using WsRoutine.Extensions;
using System.Data.Common;

namespace WsRoutine.Helpers
{
    public class DataBase : IDisposable
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Instância da base de dados.
        /// </summary>
        public Database db;

        /// <summary>
        /// Comando executado.
        /// </summary>
        public DbCommand databaseCommand;

        /// <summary>
        /// String de conexão
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Busca a configuração padrão da Web Config.
        /// </summary>
        public DataBase(IConfiguration configuration)
        {
            _configuration = configuration;
            this.ConnectionString = ConnnectionsStrings.GetDatabaseConnectionString(configuration);
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
