using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIA.presentacion
{
    public partial class Form1 : Form
    {
        RegistrarEstudiante registrar = new RegistrarEstudiante();

        public Form1()
        {
            InitializeComponent();
            panel1.Controls.Add(new FormInicio());
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {

        }

        private void registrarEstudianteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(registrar);
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(new FormInicio());
        }
    }
}
