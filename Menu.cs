using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viaro
{
    public partial class Menu : Form
    {
        Conexion nueva = new Conexion();
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Alumno a = new Alumno();
            a.Show();
        }

        private void btnProfesor_Click(object sender, EventArgs e)
        {
            Profesor pr = new Profesor();
            pr.Show();
        }

        private void btnGrado_Click(object sender, EventArgs e)
        {
            Grado g = new Grado();
            g.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Alumno_Grado al = new Alumno_Grado();
            al.Show();
        }
    }
}
