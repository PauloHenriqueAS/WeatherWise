﻿using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;

namespace WeatherWiseApi.Code.DAL
{
    public class WeatherDAL : DataBase
    {
        #region INSERTS

        /// <summary>
        /// Salvar Informações do Tempo Atual
        /// </summary>
        /// <param name="objCurrentWeather"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostCurrentWeather(CurrentWeather objCurrentWeather)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine("INSERT INTO table_name (column1, column2, column3, )");
            insertSql.AppendLine("VALUES(value1, value2, value3, ) ");
            string cmdSql = insertSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objCurrentWeather.clouds);
                this.db.AddInParameter(databaseCommand, ":val1", DbType.DateTime, objCurrentWeather.visibility);
                this.db.AddInParameter(databaseCommand, ":val2", DbType.String, objCurrentWeather.name);

                return db.ExecuteNonQuery(databaseCommand) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Salvar Informações da Previsão do Tempo
        /// </summary>
        /// <param name="objForecast"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostForecastWeather(Forecast objForecast)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine("INSERT INTO table_name (column1, column2, column3, )");
            insertSql.AppendLine("VALUES(value1, value2, value3, ) ");
            string cmdSql = insertSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objForecast.cod);
                this.db.AddInParameter(databaseCommand, ":val1", DbType.DateTime, objForecast.city);
                this.db.AddInParameter(databaseCommand, ":val2", DbType.String, objForecast.message);

                return db.ExecuteNonQuery(databaseCommand) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion

        #region INSERTS

        /// <summary>
        /// Atualização das Informações do Tempo Atual
        /// </summary>
        /// <param name="objCurrentWeather"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PutCurrentWeather(CurrentWeather objCurrentWeather)
        {
            var updateSql = new StringBuilder();

            updateSql.AppendLine("UPDATE table_name                         ");
            updateSql.AppendLine(" SET column1 = value1, column2 = value2   ");
            updateSql.AppendLine(" WHERE condition                          ");
            string cmdSql = updateSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objCurrentWeather.clouds);
                this.db.AddInParameter(databaseCommand, ":val1", DbType.DateTime, objCurrentWeather.visibility);
                this.db.AddInParameter(databaseCommand, ":val2", DbType.String, objCurrentWeather.name);

                return db.ExecuteNonQuery(databaseCommand) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualização das Informações da Previsão do Tempo
        /// </summary>
        /// <param name="objForecast"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PutForecastWeather(Forecast objForecast)
        {
            var updateSql = new StringBuilder();

            updateSql.AppendLine("UPDATE table_name                         ");
            updateSql.AppendLine(" SET column1 = value1, column2 = value2   ");
            updateSql.AppendLine(" WHERE condition                          ");
            string cmdSql = updateSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objForecast.cod);
                this.db.AddInParameter(databaseCommand, ":val1", DbType.DateTime, objForecast.city);
                this.db.AddInParameter(databaseCommand, ":val2", DbType.String, objForecast.message);

                return db.ExecuteNonQuery(databaseCommand) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion
    }
}