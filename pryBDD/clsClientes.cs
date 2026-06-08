using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace pryBDD
{
    internal class clsClientes
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..//..//BDDClientes//Clientes.mdb";
        public string tabla = "Cliente";

        private Decimal deuda;
        private Int32 cantidad;
        private Decimal promedio = 0;

        private Int32 idCli;
        private String nombre;
        private decimal deu;
        private decimal lim;
        private Int32 idAut;


        public Decimal TotalDeuda
        {
            get { return deuda; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public Int32 IdCliente
        {
            get { return idCli; }
            set { idCli = value; }
        }

        public Int32 idCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }

        public Decimal Deuda
        {
            get { return deu; }
            set { deu = value; }
        }

        public Decimal Limite
        {
            get { return lim; }
            set { lim = value; }
        }

        public Int32 Auto
        {
            get { return idAut; }
            set { idAut = value; }
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

        public void buscar(Int32 idCliente)
        {
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = tabla;
                bool encontro = false;
                OleDbDataReader DR = comando.ExecuteReader();

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {

                        if (DR.GetInt32(0) == idCliente)
                        {
                            idCli = DR.GetInt32(0);
                            nombre = DR.GetString(1);
                            deu = DR.GetDecimal(2);
                            lim = DR.GetDecimal(3);
                            idAut = DR.GetInt32(4);
                            encontro = true;
                            break;
                        }

                    }

                    if (encontro == false)
                    {
                        MessageBox.Show("Cliente no encontrado");
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

        public void agregar()
        {
            try
            {
                string tabla = "Cliente";
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, tabla);

                DataTable Tabla = DS.Tables[tabla];
                DataRow fila = Tabla.NewRow();

                fila["Nombre"] = nombre;
                fila["Deuda"] = 0;
                fila["Limite"] = lim;
                fila["Automovil"] = idAut;

                Tabla.Rows.Add(fila);
                OleDbCommandBuilder ConciliaCambios = new OleDbCommandBuilder(adaptador);
                adaptador.Update(DS, tabla);
                conexion.Close();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void listarForEach(DataGridView grilla) {
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

                if (DS.Tables[tabla].Rows.Count > 0)
                {
                    foreach (DataRow f in DS.Tables[tabla].Rows)
                    {
                        grilla.Rows.Add(f["Nombre"], f["Automovil"]);
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

        public void generarReporte(string nombreArchivo)
        {
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                StreamWriter AD = new StreamWriter(nombreArchivo, false);

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
                AD.WriteLine("Promedio deuda: ;;" + deuda / cantidad);
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
