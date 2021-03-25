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
    public partial class Alumno_Grado : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        OdbcDataAdapter dt;
        DataSet ds;
        bool IngresoC = true;
        string alumno;
        string grado;
        public Alumno_Grado()
        {
            InitializeComponent();
            llenarTabla();
            llenarCombo();
        }

        public void llenarTabla()
        {
            try {
                ds = new DataSet();
                dt = new OdbcDataAdapter("SELECT a.idAlumno_Grado AS Codigo, a.Seccion, g.Nombre AS Grado, al.Nombre AS Alumno FROM"
                    + " Alumno_Grado a INNER JOIN Grado g ON a.idGrado=g.idGrado JOIN Alumno al ON a.idAlumno=al.idAlumno", nueva.nuevaConexion()); ;
                dt.Fill(ds);
                TablaAsignacion.DataSource = ds.Tables[0];

            }
            catch (OdbcException er) {
                MessageBox.Show(er.ToString());
            }
        }

        public void llenarCombo()
        {
            try
            {

                cmd = new OdbcCommand("SELECT Nombre FROM Grado", nueva.nuevaConexion());
                OdbcDataReader al = cmd.ExecuteReader();
                while (al.Read() == true)
                {
                    cmbGrado.Items.Add(al.GetValue(0));
                }
                al.Close();
            }
            catch (OdbcException er)
            {
                MessageBox.Show(er.ToString());
            }

            try
            {

                cmd = new OdbcCommand("SELECT Nombre FROM Alumno", nueva.nuevaConexion());
                OdbcDataReader al = cmd.ExecuteReader();
                while (al.Read() == true)
                {
                    cmbAlumno.Items.Add(al.GetValue(0));
                }
                al.Close();
            }
            catch (OdbcException er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void Alumno_Grado_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                try
                {
                    ds = new DataSet();
                    dt = new OdbcDataAdapter("SELECT a.idAlumno_Grado AS Codigo, a.Seccion, g.Nombre AS Grado, al.Nombre AS Alumno FROM"
                    + " Alumno_Grado a INNER JOIN Grado g ON a.idGrado=g.idGrado JOIN Alumno al ON a.idAlumno=al.idAlumno WHERE a.idAlumno_Grado='" + txtCodigo.Text + "'", nueva.nuevaConexion());
                    dt.Fill(ds);
                    TablaAsignacion.DataSource = ds.Tables[0];
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }

                if (ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("Regitro Encontrado");
                }
                else
                {
                    llenarTabla();
                    MessageBox.Show("Registro No Exite");
                }
            }
            else
            {
                MessageBox.Show("Campo Codigo Vacio");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Validar Combo
            bool validaG = true;
            if (cmbGrado.SelectedItem != null)
            {
                string nGrad = cmbGrado.SelectedItem.ToString();
                try
                {
                    cmd = new OdbcCommand("SELECT idGrado FROM Grado WHERE Nombre='" + nGrad + "'", nueva.nuevaConexion());
                    OdbcDataReader al = cmd.ExecuteReader();

                    while (al.Read() == true)
                    {
                        grado = al.GetString(0);
                    }
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                validaG = false;
            }

            //Validar Combo
            bool validaA = true;
            if (cmbAlumno.SelectedItem != null)
            {
                string nAlum = cmbAlumno.SelectedItem.ToString();
                try
                {
                    cmd = new OdbcCommand("SELECT idAlumno FROM Alumno WHERE Nombre='" + nAlum + "'", nueva.nuevaConexion());
                    OdbcDataReader al = cmd.ExecuteReader();

                    while (al.Read() == true)
                    {
                        alumno = al.GetString(0);
                    }
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                validaA = false;
            }

            if (txtCodigo.Text != "" || txtSeccion.Text != "" || validaA == true||validaG==true)
            {
                try
                {
                    cmd = new OdbcCommand("INSERT INTO Alumno_Grado(idAlumno_Grado,Seccion, idGrado,idAlumno) VALUES('"
                        + txtCodigo.Text + "','"
                        + txtSeccion.Text + "','"
                        + grado + "','"
                        + alumno+ "')", nueva.nuevaConexion());
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
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IngresoC = false;
            }

            if (IngresoC)
            {
                MessageBox.Show("Ingresado Correctamente");
                txtCodigo.Clear();
                txtSeccion.Clear();
                cmbAlumno.Items.Clear();
                cmbGrado.Items.Clear();
                llenarCombo();
            }

            llenarTabla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool validaG = true;
            if (cmbGrado.SelectedItem != null)
            {
                string nGrad = cmbGrado.SelectedItem.ToString();
                try
                {
                    cmd = new OdbcCommand("SELECT idGrado FROM Grado WHERE Nombre='" + nGrad + "'", nueva.nuevaConexion());
                    OdbcDataReader al = cmd.ExecuteReader();

                    while (al.Read() == true)
                    {
                        grado = al.GetString(0);
                    }
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                validaG = false;
            }

            //Validar Combo
            bool validaA = true;
            if (cmbAlumno.SelectedItem != null)
            {
                string nAlum = cmbAlumno.SelectedItem.ToString();
                try
                {
                    cmd = new OdbcCommand("SELECT idAlumno FROM Alumno WHERE Nombre='" + nAlum + "'", nueva.nuevaConexion());
                    OdbcDataReader al = cmd.ExecuteReader();

                    while (al.Read() == true)
                    {
                        alumno = al.GetString(0);
                    }
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                validaA = false;
            }

            if (txtCodigo.Text!=""||txtSeccion.Text!=""||validaA==true||validaG==true)
            {
                try
                {
                    cmd = new OdbcCommand("UPDATE Alumno_Grado SET Seccion='"
                        + txtSeccion.Text + "',idGrado='"
                        + grado + "', idAlumno='" +alumno+
                        "' WHERE idAlumno_Grado='" + txtCodigo.Text + "'", nueva.nuevaConexion());
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
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IngresoC = false;
            }

            if (IngresoC) {
                txtCodigo.Enabled = true;
                MessageBox.Show("Modificado Correctamente");
                txtCodigo.Clear();
                txtSeccion.Clear();
                cmbAlumno.Items.Clear();
                cmbGrado.Items.Clear();
                llenarCombo();
            }

            llenarTabla();
        }

        private void TablaAsignacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaAsignacion.CurrentRow.Cells[0].Value.ToString()!="")
            {
                txtCodigo.Enabled = false;
                txtCodigo.Text = Convert.ToString(TablaAsignacion.CurrentRow.Cells[0].Value);
                txtSeccion.Text = Convert.ToString(TablaAsignacion.CurrentRow.Cells[1].Value);
                cmbGrado.Text = Convert.ToString(TablaAsignacion.CurrentRow.Cells[2].Value);
                cmbAlumno.Text = Convert.ToString(TablaAsignacion.CurrentRow.Cells[3].Value);
            }
            else
            {
                MessageBox.Show("Campo Vacio", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtSeccion.Clear();
            cmbAlumno.Items.Clear();
            cmbGrado.Items.Clear();
            llenarCombo();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                try
                {
                    cmd = new OdbcCommand("DELETE FROM Alumno_Grado  WHERE idAlumno_Grado='"
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
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IngresoC = false;
            }

            if (IngresoC)
            {
                txtCodigo.Enabled = true;
                MessageBox.Show("Eliminado Correctamente");
                txtCodigo.Clear();
                txtSeccion.Clear();
                cmbGrado.Items.Clear();
                cmbAlumno.Items.Clear();
                llenarCombo();
            }
            llenarTabla();
        }
    }
}
