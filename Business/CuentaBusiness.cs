using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CuentaBusiness
    {
        private CuentaDao cuentaDao = new CuentaDao();

        public void RealizarTransferecia(Cuenta miCuenta, Int64 dniReceptor, double monto)
        {
            if (cuentaDao.GetSaldo(miCuenta.DniTitular) < monto) throw new Exception("El saldo de su cuenta no es suficiente para realizar esta transferencia");

            if (!cuentaDao.ExisteCuenta(dniReceptor)) throw new Exception("La cuenta de destino no existe o no se encuentra disponible");

            cuentaDao.RealizarTransferencia(dniReceptor, miCuenta.DniTitular, monto);
        }

        public double GetSaldo(Cuenta miCuenta)
        {
            return cuentaDao.GetSaldo(miCuenta.DniTitular);
        }


    }
}
