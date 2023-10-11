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

        private TransferenciaBusiness transferenciaBusiness = new TransferenciaBusiness();

        private void ListarCuentasDeDestino() //CARGA DE CUENTAS DE DESTINO
        {           

            txtBoxCuentas.DataSource = null;

            txtBoxCuentas.DataSource = cuentaBusiness.GetAllCuentasDestino(cuentaPropia.DniTitular);

            txtBoxCuentas.DisplayMember = "nombreApellido";

            txtBoxCuentas.ValueMember = "dniTitular";

            txtBoxCuentas.SelectedIndex = -1;
        }

        private void ListarHistorialTransferencias() //CARGA DATA GRID VIEW CON TRANSFERENCIAS REALIZADAS
        {            
            dataGridView1.DataSource = null;

            dataGridView1.DataSource = transferenciaBusiness.GetAllTransferencias();

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void Form1_Load(object sender, EventArgs e) //CARGA DE FORMULARIO
        {
            try
            {
                cuentaPropia = cuentaBusiness.GetCuentaPropia(11111111);

                ListarCuentasDeDestino();

                txtSaldoActual.Text = cuentaBusiness.GetSaldo(cuentaPropia).ToString() + "$";

                ListarHistorialTransferencias();
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

                    txtSaldoActual.Text = cuentaBusiness.GetSaldo(cuentaPropia).ToString() + "$";

                    ListarCuentasDeDestino();

                    ListarHistorialTransferencias();

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
