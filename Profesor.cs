using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
namespace Viaro
{
    public partial class Profesor : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        OdbcDataAdapter dt;
        DataSet ds;
        bool IngresoC = true;
        public Profesor()
        {
            InitializeComponent();
            llenarTabla();
        }

        public void llenarTabla()
        {
            try
            {
                ds = new DataSet();
                dt = new OdbcDataAdapter("SELECT idProfesor AS Codigo, Nombre, Apellidos, Genero FROM Profesor", nueva.nuevaConexion());
                dt.Fill(ds);
                TablaProfesor.DataSource = ds.Tables[0];
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Profesor_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text!=""||txtNombre.Text != ""||txtApellidos.Text != ""||txtGenero.Text != "") {
                try
                {
                    cmd = new OdbcCommand("INSERT INTO Profesor(idProfesor, Nombre, Apellidos, Genero)"
                        + " VALUES ('"
                        + txtCodigo.Text + "','"
                        + txtNombre.Text + "','"
                        + txtApellidos.Text + "','"
                        + txtGenero.Text + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException ex)
                {
                    MessageBox.Show(ex.ToString());
                    IngresoC = false;
                }
            }
            else
            {
                MessageBox.Show("Hacen Falta Campos Por Llenar", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IngresoC = false;
            }

            if (IngresoC)
            {
                MessageBox.Show("Profesor Ingresado Correctamente");
                txtCodigo.Clear();
                txtNombre.Clear();
                txtApellidos.Clear();
                txtGenero.Clear();
            }
            llenarTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text!="") {
                try
                {
                    ds = new DataSet();
                    dt = new OdbcDataAdapter("SELECT idProfesor AS Codigo, Nombre, Apellidos, Genero FROM Profesor WHERE Nombre LIKE '"+txtBusqueda.Text +"%'", nueva.nuevaConexion());
                    dt.Fill(ds);
                    TablaProfesor.DataSource = ds.Tables[0];

                    if (ds.Tables[0].Rows.Count!=0)
                    {
                        MessageBox.Show("Regitro Encontrado");
                    }
                    else
                    {
                        llenarTabla();
                        MessageBox.Show("Registro No Exite");
                    }
                }
                catch (OdbcException t)
                {
                    MessageBox.Show(t.ToString());
                }
            }
            else
            {
                ds = new DataSet();
                dt = new OdbcDataAdapter("SELECT idProfesor AS Codigo, Nombre, Apellidos, Genero FROM Profesor", nueva.nuevaConexion());
                dt.Fill(ds);
                TablaProfesor.DataSource = ds.Tables[0];
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" || txtNombre.Text != "" || txtApellidos.Text != "" || txtGenero.Text != "") {
                try
                {
                    cmd = new OdbcCommand("UPDATE Profesor SET Nombre='"
                        + txtNombre.Text + "', Apellidos='"
                        + txtApellidos.Text + "',Genero='"
                        + txtGenero.Text + "' WHERE idProfesor='"
                        + txtCodigo.Text + "'",nueva.nuevaConexion());

                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                    IngresoC = false;
                }
            }
            else
            {
                MessageBox.Show("Hacen Falta Campos Por Llenar", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IngresoC = false;
            }

            if (IngresoC) {
                MessageBox.Show("Profesor Modificado Correctamente");
                txtCodigo.Enabled = true;
                txtCodigo.Clear();
                txtNombre.Clear();
                txtApellidos.Clear();
                txtGenero.Clear();
            }
            llenarTabla();
        }

        private void TablaProfesor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaProfesor.CurrentRow.Cells[0].Value.ToString()!="")
            {
                txtCodigo.Enabled = false;
                txtCodigo.Text = Convert.ToString(TablaProfesor.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(TablaProfesor.CurrentRow.Cells[1].Value);
                txtApellidos.Text = Convert.ToString(TablaProfesor.CurrentRow.Cells[2].Value);
                txtGenero.Text = Convert.ToString(TablaProfesor.CurrentRow.Cells[3].Value);
            }
            else
            {
                MessageBox.Show("Campo Vacio","ADVERTENCIA",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text!="")
            {
                try
                {
                    cmd = new OdbcCommand("DELETE FROM Profesor WHERE idProfesor='"
                    + txtCodigo.Text + "'", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                    IngresoC = false;
                }
            }
            else
            {
                MessageBox.Show("Campo Codigo Vacio", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IngresoC = false;
            }

            if (IngresoC)
            {
                MessageBox.Show("Profesor Eliminado Correctamente");
                txtCodigo.Enabled = true;
                txtCodigo.Clear();
                txtNombre.Clear();
                txtApellidos.Clear();
                txtGenero.Clear();
            }
            llenarTabla();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtGenero.Clear();
        }

        private void Codigo_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
