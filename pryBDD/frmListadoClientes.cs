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
            x.listar(dgvGrilla);
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            SaveFileDialog objArchivo = new SaveFileDialog();

            objArchivo.Title = "Guardar reporte";
            objArchivo.RestoreDirectory = true;
            objArchivo.Filter = "Archivo separados por coma (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";

            objArchivo.ShowDialog();
            x.generarReporte(objArchivo.FileName);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
