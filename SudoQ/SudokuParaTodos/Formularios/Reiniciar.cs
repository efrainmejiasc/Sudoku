using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuParaTodos.Formularios
{
    public partial class Reiniciar : Form
    {
        private EngineData Valor = EngineData.Instance();

        public Reiniciar()
        {
            InitializeComponent();
        }

        private void Reiniciar_Load(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            label2.Text = Valor.TituloReiniciar(Valor.GetNombreIdioma());
            button1.Text = Valor.TituloButton1(Valor.GetNombreIdioma());
            button2.Text = Valor.TituloButton2(Valor.GetNombreIdioma());
            this.Text = Valor.TituloEtiqueta1(Valor.GetNombreIdioma());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Valor.SetContinuar(true);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Valor.SetContinuar(false);
            this.Close();
        }
    }
}
