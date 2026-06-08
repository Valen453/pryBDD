using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryBDD
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listadoDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 x = new Form2(); 
            x.ShowDialog();
        }

        private void listadoDeDeudoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeudores x = new frmDeudores();
            x.ShowDialog();
        }

        private void consultarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscarCliente x = new frmBuscarCliente();
            x.ShowDialog();
        }

        private void agregarNuevoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAgregarCliente x = new frmAgregarCliente();
            x.ShowDialog();
        }

        private void listarForEachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmForEach x = new frmForEach();
            x.ShowDialog(); 
        }
    }
}
