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
    public partial class Alumno : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        OdbcDataAdapter dt;
        DataSet ds;
        bool IngresoC = true;
        public Alumno()
        {
            InitializeComponent();
            llenar_tabla();
        }

        public void llenar_tabla()
        {
            try
            {
                ds = new DataSet();
                dt = new OdbcDataAdapter("SELECT idAlumno AS Codigo, Nombre, Apellidos, Genero, Fecha_Nacimiento FROM Alumno", nueva.nuevaConexion());
                dt.Fill(ds);
                TablaAlumno.DataSource = ds.Tables[0];

            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TablaAlumno_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Alumno_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtApellido.Text!=""||txtCodigo.Text != ""||txtFecha.Text != ""||txtGenero.Text != ""||txtNombre.Text != "") {
                try
                {
                    cmd = new OdbcCommand("INSERT INTO Alumno(idAlumno,Nombre,Apellidos,Genero,Fecha_Nacimiento)" +
                        "VALUES('"+txtCodigo.Text + "','"
                        + txtNombre.Text + "','"
                        + txtApellido.Text + "','"
                        + txtGenero.Text + "','"
                        + txtFecha.Text + "')",nueva.nuevaConexion());
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
                MessageBox.Show("Hacen Falta Campos Por Llenar","ADVERTENCIA",MessageBoxButtons.OK,MessageBoxIcon.Information);
                IngresoC = false;
            }

            if (IngresoC)
            {
                MessageBox.Show("Alumno Ingresado Correctamente");
                txtCodigo.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtGenero.Clear();
                txtFecha.Clear();
            }

            llenar_tabla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtApellido.Text != "" || txtCodigo.Text != "" || txtFecha.Text != "" || txtGenero.Text != "" || txtNombre.Text != "") {
                try
                {
                    cmd = new OdbcCommand("UPDATE Alumno SET Nombre='"
                        + txtNombre.Text + "',Apellidos='"
                        + txtApellido.Text + "',Genero='"
                        + txtGenero.Text + "',Fecha_Nacimiento='"
                        + txtFecha.Text + "' WHERE idAlumno='"
                        + txtCodigo.Text + "'", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException r)
                {
                    MessageBox.Show(r.ToString());
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
                MessageBox.Show("Alumno Modificado Correctamente");
                txtCodigo.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtGenero.Clear();
                txtFecha.Clear();
                txtCodigo.Enabled = true;
            }
            llenar_tabla();
        }

        private void TablaAlumno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaAlumno.CurrentRow.Cells[0].Value.ToString()!="")
            {
                txtCodigo.Enabled = false;
                txtCodigo.Text = Convert.ToString(TablaAlumno.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(TablaAlumno.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(TablaAlumno.CurrentRow.Cells[2].Value);
                txtGenero.Text = Convert.ToString(TablaAlumno.CurrentRow.Cells[3].Value);
                txtFecha.Text = Convert.ToString(TablaAlumno.CurrentRow.Cells[4].Value);
            }
            else
            {
                MessageBox.Show("Campo Vacio", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new OdbcDataAdapter("SELECT idAlumno AS Codigo, Nombre, Apellidos, Genero, Fecha_Nacimiento FROM Alumno WHERE idAlumno='"
                    + txtCodigo.Text + "'", nueva.nuevaConexion());
                ds = new DataSet();
                dt.Fill(ds);
                TablaAlumno.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count!=0)
                {
                    MessageBox.Show("Regitro Encontrado");
                }
                else
                {
                    llenar_tabla();
                    MessageBox.Show("Registro No Exite");
                }
            }
            catch (OdbcException err)
            {
                llenar_tabla();
                MessageBox.Show(err.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OdbcCommand("DELETE FROM Alumno WHERE idAlumno='"
                    + txtCodigo.Text + "'",nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException er)
            {
                MessageBox.Show(er.ToString());
                IngresoC = false;
            }

            if (IngresoC)
            {
                MessageBox.Show("Alumno Eliminado Correctamente");
                txtCodigo.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtGenero.Clear();
                txtFecha.Clear();
                txtCodigo.Enabled = true;
            }
            llenar_tabla();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtGenero.Clear();
            txtFecha.Clear();
            txtCodigo.Enabled = true;
        }
    }
}
