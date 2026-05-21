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
    internal class clsClientes
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Clientes.mdb";
        private string tabla = "Cliente";

        private Decimal deuda;
        private Int32 cantidad;
        private Decimal promedio = 0;

        public Decimal TotalDeuda
        {
            get { return deuda; }
        }

        public Int32 CantidadDeudores
        {
            get { return cantidad; }
        }

        public Decimal Promedio { 
            get { return promedio; } 
        }
        public void listar(DataGridView grilla)
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
                adaptador.Fill(DS);

                grilla.DataSource = DS.Tables[0];

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

        }

        public void listarDeudores(DataGridView grilla)
        {
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                grilla.Rows.Clear();

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {

                        if (DR.GetDecimal(2) > 0)
                        {
                            grilla.Rows.Add(DR.GetInt32(0), DR.GetString(1), DR.GetDecimal(2));
                            deuda += DR.GetDecimal(2);
                            cantidad++;
                        }
                    }
                }
                
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

 

    }

    public void generarReporte(){
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                StreamWriter AD = new StreamWriter("ReporteClientes.csv", false);

                AD.WriteLine("Listado de Clientes\n");
                AD.WriteLine("Codigo; Nombre; Deuda");
                int cantidad = 0;
                decimal deuda = 0;

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {

                        AD.Write(DR.GetInt32(0));
                        AD.Write(";");
                        AD.Write(DR.GetString(1));
                        AD.Write(";");
                        AD.WriteLine(DR.GetDecimal(2));

                        cantidad++;
                        deuda = deuda + DR.GetDecimal(2);
                    }
                }

                AD.WriteLine("Cantidad de clientes: ;;" + cantidad);
                AD.WriteLine("Deuda de los clientes: ;;" + deuda);
                AD.WriteLine("Promedio deuda: ;;" + deuda/cantidad);
                conexion.Close();
                AD.Close();
            }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    throw;
                }

            }
     }
}
