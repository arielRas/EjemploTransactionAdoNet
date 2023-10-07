using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CuentaPropia : Cuenta
    {
        private double saldo;

        public double Saldo { get => saldo; set => saldo = value; }
    }
}
