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
    public partial class DialogSave : Form
    {
        private EngineData Valor = EngineData.Instance();
        private EngineSudoku Funcion = new EngineSudoku();
        private string pathArchivo = string.Empty;
        private string[,] valorIngresado = new string[9, 9];
        private string[,] valorEliminado = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];

        public DialogSave()
        {
            InitializeComponent();
        }

        public DialogSave(string [,] vI, string[,] vE, string[,] vIn, string[,] vS,string pA)
        {
            InitializeComponent();
            this.valorIngresado = vI;
            this.valorEliminado = vE;
            this.valorInicio = vIn;
            this.valorSolucion = vS;
            this.pathArchivo = pA;
        }

        private void DialogSave_Load(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            label2.Text = Valor.TituloEtiquetaSave(Valor.GetNombreIdioma());
            button1.Text = Valor.TituloButton1(Valor.GetNombreIdioma());
            button2.Text = Valor.TituloButton2(Valor.GetNombreIdioma());
            button3.Text = Valor.TituloButton3(Valor.GetNombreIdioma());
            this.Text = Valor.TituloEtiqueta1(Valor.GetNombreIdioma());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Guardar Cambios
            pathArchivo = Valor.GetPathArchivo();
            GuardarJuego(pathArchivo);
            Valor.SetContinuar(true);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // No Guardar cambios
            Valor.SetContinuar(true);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Cancelar
            Valor.SetContinuar (false);
            Valor.SetPathArchivo(this.pathArchivo);
            this.Close();
        }

        private void GuardarJuego(string pathArchivo)
        {
            if (Funcion.ExiteArchivo(pathArchivo)) { Funcion.ReadWriteTxt(this.pathArchivo); }
            Funcion.GuardarValoresIngresados(pathArchivo, this.valorIngresado);
            Funcion.GuardarValoresEliminados(pathArchivo, this.valorEliminado);
            Funcion.GuardarValoresInicio(pathArchivo, this.valorInicio);
            Funcion.GuardarValoresSolucion(pathArchivo, this.valorSolucion);
            if (Funcion.ExiteArchivo(pathArchivo)) { Funcion.OnlyReadTxt(this.pathArchivo); }
        }
    }
}
