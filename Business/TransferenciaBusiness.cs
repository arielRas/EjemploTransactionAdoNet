using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TransferenciaBusiness
    {
        TransferenciaDao transferenciaDao = new TransferenciaDao();

        public List<Transferencia> GetAllTransferencias()
        {
            return transferenciaDao.GetAllTransferencias();
        }
    }
}
