using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    internal class Cuenta
    {
        private Int64 dniTitular;

        private string nombreApellido;

        private double saldo;

        public long DniTitular { get => dniTitular; set => dniTitular = value; }
        public string NombreApellido { get => nombreApellido; set => nombreApellido = value; }
        public double Saldo { get => saldo; set => saldo = value; }
    }
}
