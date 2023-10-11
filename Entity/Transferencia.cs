using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Transferencia
    {
        private Int64 nroCuentaDestino;

        private string titularCuentaDestino;

        private double monto;

        private DateTime fecha;

        private string hora;

        public long NroCuentaDestino { get => nroCuentaDestino; set => nroCuentaDestino = value; }
        public string TitularCuentaDestino { get => titularCuentaDestino; set => titularCuentaDestino = value; }
        public double Monto { get => monto; set => monto = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
    }
}
