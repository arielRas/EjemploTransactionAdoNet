using Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class CuentaDao
    {
        public Cuenta GetCuentaPropia(Int64 dniTitular)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbActividad08"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlQuery = "SELECT DNI_TITULAR, NOMBRE_TITULAR FROM Cuenta WHERE DNI_TITULAR = @dniTitular";

            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dniTitular", dniTitular);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var cuentaPropia = new Cuenta
                                {
                                    DniTitular = reader.GetInt64(0),
                                    NombreApellido = reader.GetString(1)
                                };

                                return cuentaPropia;
                            }
                            else
                                throw new Exception("Su cuenta no esta disponible en este momento, intente mas tarde");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool ExisteCuenta(Int64 dniReceptor)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbActividad08"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlQuery = "SELECT COUNT(*) FROM Cuenta WHERE DNI_TITULAR = @dniReceptor";

            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dniReceptor", dniReceptor);

                        if(Convert.ToInt32(command.ExecuteScalar()) == 1) return true;

                            else return false;
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

        public List<Cuenta> GetAllCuentasDestino(Int64 dni)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbActividad08"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            List<Cuenta> CuentasDestino = new List<Cuenta>();

            string sqlQuery = "SELECT DNI_TITULAR, NOMBRE_TITULAR FROM Cuenta WHERE NOT DNI_TITULAR = @dni";

            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dni", dni);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cuenta cuentaDestino = new Cuenta { DniTitular = reader.GetInt64(0), NombreApellido = reader.GetString(1) };

                                CuentasDestino.Add(cuentaDestino);
                            }

                            return CuentasDestino;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

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

    }
}
