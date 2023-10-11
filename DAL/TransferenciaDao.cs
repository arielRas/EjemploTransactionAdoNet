using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TransferenciaDao
    {
        public List<Transferencia> GetAllTransferencias()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbActividad08"].ConnectionString;

            List<Transferencia> transferencias = new List<Transferencia>();

            string sqlQuery = "SELECT HT.DNI_CUENTA_DESTINO, C.NOMBRE_TITULAR, MONTO, FECHA FROM Historial_Transferencias AS HT JOIN Cuenta AS C ON HT.DNI_CUENTA_DESTINO = C.DNI_TITULAR ORDER BY HT.FECHA DESC";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            //DateTime fechaHora = reader.GetDateTime(3);

                            Transferencia transferencia = new Transferencia
                            {
                                NroCuentaDestino = reader.GetInt64(0),
                                TitularCuentaDestino = reader.GetString(1),
                                Monto = Convert.ToDouble(reader.GetValue(2)),
                                Fecha = reader.GetDateTime(3).Date,
                                Hora = reader.GetDateTime(3).ToLongTimeString(),
                            };

                            transferencias.Add(transferencia);
                        }

                        return transferencias;
                    }
                }
            }
        }
    }
}
