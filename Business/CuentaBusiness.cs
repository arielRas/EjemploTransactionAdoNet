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

        public void RealizarTransferecia(Cuenta cuentaPropia, Int64 dniReceptor, double monto)
        {
            if (monto <= 0) throw new Exception("El monto a enviar no puede ser menor o igual a cero");

            if (cuentaDao.GetSaldo(cuentaPropia.DniTitular) < monto) throw new Exception("El saldo de su cuenta no es suficiente para realizar esta transferencia");

            if (!cuentaDao.ExisteCuenta(dniReceptor)) throw new Exception("La cuenta de destino no existe o no se encuentra disponible");

            cuentaDao.RealizarTransferencia(dniReceptor, cuentaPropia.DniTitular, monto, DateTime.Now);
        }

        public double GetSaldo(Cuenta cuentaPropia)
        {
            return cuentaDao.GetSaldo(cuentaPropia.DniTitular);
        }

        public List<Cuenta> GetAllCuentasDestino(Int64 dni)
        {
            return cuentaDao.GetAllCuentasDestino(dni);
        }

        public Cuenta GetCuentaPropia(Int64 dniTitular)
        {
            return cuentaDao.GetCuentaPropia(dniTitular);
        }
    }
}
