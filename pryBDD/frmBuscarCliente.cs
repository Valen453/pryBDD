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
    public partial class frmBuscarCliente : Form
    {
        public frmBuscarCliente()
        {
            InitializeComponent();
        }

        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            clsClientes x = new clsClientes();
            x.buscar(Convert.ToInt32(tbIdCliente.Text));

            lblNombre.Text = x.Nombre;
            lblDeuda.Text = Convert.ToString(x.Deuda);
        }
    }
}
