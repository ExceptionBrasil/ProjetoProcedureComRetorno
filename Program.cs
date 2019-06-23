using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoProcedureComRetorno
{
    class Program
    {
        static string STRING_CONNECTION = "Server=localhost;Database=DbLab;User Id=sa;Password=sa;";
        static void Main(string[] args)
        {
            using (SqlConnection conecxao = new SqlConnection(STRING_CONNECTION))
            {
                conecxao.Open();
                using (SqlCommand comando = conecxao.CreateCommand())
                {
                    //Tipo de execução
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    //Texto de execução 
                    comando.CommandText = "usp_UserTokenUpdate";

                    //Add parâmetros
                    comando.Parameters.AddWithValue("@Id", 1);
                    comando.Parameters.AddWithValue("@Token", "Teste Console Teste");
                    comando.Parameters.AddWithValue("@Expiration", "2019-06-23 15:10:00");
                    comando.Parameters.AddWithValue("@Audience", "https://localhost");
                    comando.Parameters.AddWithValue("@Issuer", "WebApiBase");
                    comando.Parameters.AddWithValue("@Result",SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("@MessageError", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;

                    //Execução
                    comando.ExecuteNonQuery();

                    Console.WriteLine(Convert.ToInt32(comando.Parameters["@Result"].Value));
                    Console.WriteLine(Convert.ToString(comando.Parameters["@MessageError"].Value));
                    
                }
            }
        }
    }
}
