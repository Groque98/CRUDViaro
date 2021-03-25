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
    public partial class Grado : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        OdbcDataAdapter dt;
        DataSet ds;
        bool IngresoC = true;
        string profesor;

        public Grado()
        {
            InitializeComponent();
            llenarTabla();
            llenarCombo();
        }

        private void Grado_Load(object sender, EventArgs e)
        {

        }

        public void llenarTabla()
        {
            try
            {
                ds = new DataSet();
                dt = new OdbcDataAdapter("SELECT g.idGrado AS Codigo, g.Nombre, p.Nombre AS Profesor FROM " +
                    "Grado g INNER JOIN Profesor p ON g.idProfesor=p.idProfesor",nueva.nuevaConexion());
                dt.Fill(ds);
                TablaGrado.DataSource = ds.Tables[0];
            }
            catch (OdbcException er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        public void llenarCombo()
        {
            try
            {

                cmd = new OdbcCommand ("SELECT p.Nombre FROM Profesor p LEFT JOIN Grado g" +
                    " ON g.idProfesor=p.idProfesor WHERE g.idProfesor != (", nueva.nuevaConexion());
                OdbcDataReader al = cmd.ExecuteReader();
                while(al.Read()==true){
                    cmbProfesro.Items.Add(al.GetValue(0));
                }
                al.Close();
            }
            catch (OdbcException er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Validar Combo
            bool valida = true;
            if (cmbProfesro.SelectedItem!=null)
            {
                string nProf = cmbProfesro.SelectedItem.ToString();
                try
                {
                    cmd = new OdbcCommand("SELECT idProfesor FROM Profesor WHERE Nombre='"+ nProf + "'",nueva.nuevaConexion());
                    OdbcDataReader al = cmd.ExecuteReader();

                    while (al.Read()==true)
                    {
                        profesor = al.GetString(0);
                    }
                }
                catch(OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                valida = false;
            }

            if (txtCodigo.Text!=""||txtGrado.Text!=""||valida==true)
            {
                try
                {
                    cmd = new OdbcCommand("INSERT INTO Grado(idGrado,Nombre, idProfesor) VALUES('" 
                        + txtCodigo.Text +"','"
                        +txtGrado.Text + "','"
                        + profesor +"')",nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch(OdbcException er)
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
                MessageBox.Show("Grado Ingresado Correctamente");
                txtCodigo.Clear();
                txtGrado.Clear();
                cmbProfesro.Items.Clear();
                llenarCombo();
            }

            llenarTabla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Validar Combo
            bool valida = true;
            if (cmbProfesro.SelectedItem != null)
            {
                string nProf = cmbProfesro.SelectedItem.ToString();
                try
                {
                    cmd = new OdbcCommand("SELECT idProfesor FROM Profesor WHERE Nombre='" + nProf + "'", nueva.nuevaConexion());
                    OdbcDataReader al = cmd.ExecuteReader();

                    while (al.Read() == true)
                    {
                        profesor = al.GetString(0);
                    }
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                valida = false;
            }

            if (txtCodigo.Text != "" || txtGrado.Text != "" || valida == true)
            {
                try
                {
                    cmd = new OdbcCommand("UPDATE Grado SET Nombre='"
                        + txtGrado.Text + "',idProfesor='"
                        + profesor + "' WHERE idGrado='"+ txtCodigo.Text + "'", nueva.nuevaConexion());
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
                MessageBox.Show("Grado Modificado Correctamente");
                txtGrado.Enabled = true;
                txtCodigo.Clear();
                txtGrado.Clear();
                cmbProfesro.Items.Clear();
                llenarCombo();
            }

            llenarTabla();
        }

        private void TablaGrado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaGrado.CurrentRow.Cells[0].Value.ToString() != "")
            {
                txtCodigo.Enabled = false;
                txtCodigo.Text = Convert.ToString(TablaGrado.CurrentRow.Cells[0].Value);
                txtGrado.Text = Convert.ToString(TablaGrado.CurrentRow.Cells[1].Value);
                cmbProfesro.Text = Convert.ToString(TablaGrado.CurrentRow.Cells[2].Value);
            }
            else {
                MessageBox.Show("Campo Vacio","ADVERTENCIA",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "")
            {
                try
                {
                    cmd = new OdbcCommand("DELETE  FROM Grado WHERE idGrado='"
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
                MessageBox.Show("Grado Eliminado Correctamente");
                txtCodigo.Clear();
                txtGrado.Clear();
                cmbProfesro.Items.Clear();
                llenarCombo();
            }
            llenarTabla();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtGrado.Clear();
            cmbProfesro.Items.Clear();
            llenarCombo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text!="") {
                try
                {
                    ds = new DataSet();
                    dt = new OdbcDataAdapter("SELECT g.idGrado AS Codigo, g.Nombre, p.Nombre AS Profesor FROM " +
                        "Grado g INNER JOIN Profesor p ON g.idProfesor=p.idProfesor WHERE idGrado='" + txtCodigo.Text + "'", nueva.nuevaConexion());
                    dt.Fill(ds);
                    TablaGrado.DataSource = ds.Tables[0];
                }
                catch (OdbcException er)
                {
                    MessageBox.Show(er.ToString());
                }

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
            else
            {
                MessageBox.Show("Campo Codigo Vacio");
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
