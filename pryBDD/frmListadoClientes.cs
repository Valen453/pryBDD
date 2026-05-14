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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        clsClientes x = new clsClientes();

        private void button1_Click(object sender, EventArgs e)
        {
            x.listar(dgvClientes);
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
