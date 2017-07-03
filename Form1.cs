using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CargaPesada
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Instancianto GA
            GA ga = new GA(
                Convert.ToInt32(txtTipoA.Text),
                Convert.ToInt32(txtTipoB.Text),
                Convert.ToInt32(txtTipoC.Text)
                );

            // Chamando a função que desenha as cargas
            pictureBox1.Image = GUI.DesenhaCarga(ga.FindSolution());
        }
    }
}