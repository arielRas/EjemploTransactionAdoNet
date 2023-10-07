using Business;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private CuentaBusiness cuentaBusiness = new CuentaBusiness();

        private Cuenta cuentaPropia = null;

        private void ListarCuentasDeDestino(Int64 miDni) //CARGA DE CUENTAS DE DESTINO
        {
            txtBoxCuentas.Items.Clear();

            txtBoxCuentas.DataSource = null;

            txtBoxCuentas.DataSource = cuentaBusiness.GetAllCuentasDestino(cuentaPropia.DniTitular);

            txtBoxCuentas.DisplayMember = "dniTitular";

            txtBoxCuentas.ValueMember = "nombreApellido";
        }


        private void Form1_Load(object sender, EventArgs e) //CARGA DE FORMULARIO
        {
            try
            {
                cuentaPropia = cuentaBusiness.GetCuentaPropia(35027567);

                ListarCuentasDeDestino(cuentaPropia.DniTitular);

                txtSaldoActual.Text = cuentaBusiness.GetSaldo(cuentaPropia).ToString() + "$";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"AVISO");
            }            
        }


        
    }
}
