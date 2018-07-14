using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuParaTodos.Formularios
{
    public partial class Salir : Form
    {
        private EngineData Valor = EngineData.Instance();

        public Salir()
        {
            InitializeComponent();
        }

        private void Salir_Load(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            label2.Text = Valor.TituloEtiqueta2(Valor.GetNombreIdioma());
            button1.Text = Valor.TituloButton1(Valor.GetNombreIdioma());
            button2.Text = Valor.TituloButton2(Valor.GetNombreIdioma());
            this.Text = Valor.TituloEtiqueta1(Valor.GetNombreIdioma());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var procesos = Process.GetProcesses();
            foreach (Process item in procesos)
            {
                if (item.ProcessName == "SudokuParaTodos")
                {
                    item.Kill();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
