using EasyBuySDK.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace EasyBuySDK.Helpers
{
    public class AccessData : IDisposable
    {
        private OleDbConnection connection;

        public void Dispose()
        {
            connection.Dispose();
        }

        public List<Estabelecimento> GetDataFromExcel(string filepath)
        {
            string connectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                   "Data Source=" + filepath + ";" + "Extended Properties=" + "\"" + "Excel 12.0;HDR=YES;" + "\"";

            connection = new OleDbConnection(connectionstring);
            try
            {
                List<Estabelecimento> pessoas = new List<Estabelecimento>();

                string sql = "select *from [Plan1$]";

                OleDbCommand command = new OleDbCommand(sql, connection);

                connection.Open();

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pessoas.Add(new Estabelecimento
                    {
                        Nome = reader["estabelecimento"].ToString(),
                        Endereco = reader["endereco"].ToString()
                    });
                }

                return pessoas;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}