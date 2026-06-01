using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace pryBDD
{
    internal class clsAutomoviles
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..//..//BDDClientes//Clientes.mdb";
        private string tabla = "Automovil";


        public void listar(ComboBox combo)
        {
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, tabla);

                combo.DataSource = DS.Tables[tabla];
                combo.DisplayMember = "Nombre";
                combo.ValueMember = "idAutomovil";
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

        }
    }
}
