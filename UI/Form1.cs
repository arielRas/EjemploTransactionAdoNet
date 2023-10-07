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

        private void ListarCuentasDeDestino() //CARGA DE CUENTAS DE DESTINO
        {           

            txtBoxCuentas.DataSource = null;

            txtBoxCuentas.DataSource = cuentaBusiness.GetAllCuentasDestino(cuentaPropia.DniTitular);

            txtBoxCuentas.DisplayMember = "nombreApellido";

            txtBoxCuentas.ValueMember = "dniTitular";

            txtBoxCuentas.SelectedIndex = -1;
        }


        private void Form1_Load(object sender, EventArgs e) //CARGA DE FORMULARIO
        {
            try
            {
                cuentaPropia = cuentaBusiness.GetCuentaPropia(35027567);

                ListarCuentasDeDestino();

                txtSaldoActual.Text = cuentaBusiness.GetSaldo(cuentaPropia).ToString() + "$";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"AVISO");
            }            
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtBoxCuentas.SelectedValue != null)
                {
                    Int64 dniReceptor = Convert.ToInt64(txtBoxCuentas.SelectedValue.ToString());

                    double monto = Convert.ToDouble(txtMonto.Text);

                    cuentaBusiness.RealizarTransferecia(cuentaPropia, dniReceptor, monto);

                    MessageBox.Show($"La transferencia a {txtBoxCuentas.Text} se realizo con exito", "AVISO");

                    txtMonto.Text = cuentaBusiness.GetSaldo(cuentaPropia).ToString() + "$";

                    ListarCuentasDeDestino();

                    txtMonto.Text = string.Empty;                    
                }
                else
                {
                    MessageBox.Show("Seleccione el destinatario de la trasferencia", "AVISO");
                }
                
            }
            catch (FormatException)
            {
                MessageBox.Show("El campo monto solo acepta caracteres numerico", "AVISO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AVISO");
            }
        }
    }
}
