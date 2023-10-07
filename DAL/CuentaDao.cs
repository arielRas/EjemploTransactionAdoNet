using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CuentaDao
    {
        public double GetSaldo(Int64 dniTitular)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbActividad08"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlQuery = "SELECT saldo FROM Cuenta WHERE DNI_TITULAR = @dniTitular";
            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dniTitular", dniTitular);

                        return Convert.ToDouble(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void RealizarTransferencia(Int64 dniReceptor, Int64 dniEmisor, double monto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbActividad08"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string queryDebitar = "UPDATE Cuenta SET SALDO = SALDO - @monto WHERE DNI_TITULAR = @dniEmisor";                   

                    using (SqlCommand commandDebitar = new SqlCommand(queryDebitar, connection))
                    {
                        commandDebitar.Parameters.AddWithValue("@dniEmisor", dniEmisor);

                        commandDebitar.ExecuteNonQuery();
                    }

                    string queryAcreditar = "UPDATE Cuenta SET SALDO = SALDO + @monto WHERE DNI_TITULAR = @dniReceptor";

                    using (SqlCommand commandAcreditar = new SqlCommand(queryAcreditar, connection))
                    {
                        commandAcreditar.Parameters.AddWithValue("@dniReceptor", dniReceptor);

                        commandAcreditar.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }   
        }
    }
}
