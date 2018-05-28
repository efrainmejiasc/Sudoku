using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuParaTodos.Formularios
{
    public partial class AzulUno : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        //***********************************************************************************************************
        private EngineSudoku Funcion = new EngineSudoku();
        private EngineData Valor = EngineData.Instance();
        private EngineSudoku.LetrasJuegoFEG LetrasJuegoFEG = new EngineSudoku.LetrasJuegoFEG();
        private EngineSudoku.LetrasJuegoACB LetrasJuegoACB = new EngineSudoku.LetrasJuegoACB();

        private TextBox[,] txtSudoku = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DEL SUDOKU
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorCandidatoSinEliminados = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];
        private Button[] btnPincel = new Button[9];// ARRAY CONTENTIVO DE LOS BOTONES DE PINCELES IZQUIERDO
        private Button[] btnPincel2 = new Button[2];// ARRAY CONTENTIVO DE LOS BOTONES DE PINCELES IZQUIERDO
        private string pathArchivo = string.Empty;

        private int[] position = new int[2];
        int row = -1;
        int col = -1;
        private Color colorFondoAct;
        private bool pincelMarcador = EngineData.Falso;
        private Color colorCeldaAnt;
        private string[] solo = new string[27];
        private string[] oculto = new string[27];
        private int contadorIngresado = 0;


        public AzulUno()
        {
            InitializeComponent();
        }

        private void AzulUno_Load(object sender, EventArgs e)
        {
            ComportamientoObjetoInicio();
        }

        private TextBox[,] AsociarTxtMatriz(TextBox[,] txtSudoku)
        {
            /////////////////////////////////////////////////////////////////////////////
            txtSudoku[0, 0] = txt00; txtSudoku[0, 1] = txt01; txtSudoku[0, 2] = txt02;
            txtSudoku[1, 0] = txt10; txtSudoku[1, 1] = txt11; txtSudoku[1, 2] = txt12;
            txtSudoku[2, 0] = txt20; txtSudoku[2, 1] = txt21; txtSudoku[2, 2] = txt22;

            txtSudoku[0, 3] = txt03; txtSudoku[0, 4] = txt04; txtSudoku[0, 5] = txt05;
            txtSudoku[1, 3] = txt13; txtSudoku[1, 4] = txt14; txtSudoku[1, 5] = txt15;
            txtSudoku[2, 3] = txt23; txtSudoku[2, 4] = txt24; txtSudoku[2, 5] = txt25;

            txtSudoku[0, 6] = txt06; txtSudoku[0, 7] = txt07; txtSudoku[0, 8] = txt08;
            txtSudoku[1, 6] = txt16; txtSudoku[1, 7] = txt17; txtSudoku[1, 8] = txt18;
            txtSudoku[2, 6] = txt26; txtSudoku[2, 7] = txt27; txtSudoku[2, 8] = txt28;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku[3, 0] = txt30; txtSudoku[3, 1] = txt31; txtSudoku[3, 2] = txt32;
            txtSudoku[4, 0] = txt40; txtSudoku[4, 1] = txt41; txtSudoku[4, 2] = txt42;
            txtSudoku[5, 0] = txt50; txtSudoku[5, 1] = txt51; txtSudoku[5, 2] = txt52;

            txtSudoku[3, 3] = txt33; txtSudoku[3, 4] = txt34; txtSudoku[3, 5] = txt35;
            txtSudoku[4, 3] = txt43; txtSudoku[4, 4] = txt44; txtSudoku[4, 5] = txt45;
            txtSudoku[5, 3] = txt53; txtSudoku[5, 4] = txt54; txtSudoku[5, 5] = txt55;

            txtSudoku[3, 6] = txt36; txtSudoku[3, 7] = txt37; txtSudoku[3, 8] = txt38;
            txtSudoku[4, 6] = txt46; txtSudoku[4, 7] = txt47; txtSudoku[4, 8] = txt48;
            txtSudoku[5, 6] = txt56; txtSudoku[5, 7] = txt57; txtSudoku[5, 8] = txt58;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku[6, 0] = txt60; txtSudoku[6, 1] = txt61; txtSudoku[6, 2] = txt62;
            txtSudoku[7, 0] = txt70; txtSudoku[7, 1] = txt71; txtSudoku[7, 2] = txt72;
            txtSudoku[8, 0] = txt80; txtSudoku[8, 1] = txt81; txtSudoku[8, 2] = txt82;

            txtSudoku[6, 3] = txt63; txtSudoku[6, 4] = txt64; txtSudoku[6, 5] = txt65;
            txtSudoku[7, 3] = txt73; txtSudoku[7, 4] = txt74; txtSudoku[7, 5] = txt75;
            txtSudoku[8, 3] = txt83; txtSudoku[8, 4] = txt84; txtSudoku[8, 5] = txt85;

            txtSudoku[6, 6] = txt66; txtSudoku[6, 7] = txt67; txtSudoku[6, 8] = txt68;
            txtSudoku[7, 6] = txt76; txtSudoku[7, 7] = txt77; txtSudoku[7, 8] = txt78;
            txtSudoku[8, 6] = txt86; txtSudoku[8, 7] = txt87; txtSudoku[8, 8] = txt88;
            ////////////////////////////////////////////////////////////////////////////
            foreach (TextBox item in txtSudoku)
            {
                item.GotFocus += delegate { HideCaret(item.Handle); };
                //item.ReadOnly = EngineData.Verdadero;
            }
            return txtSudoku;
        }

        private Button[] AsociarBtnPincel(Button[] btnPincel)
        {
            btnPincel[0] = pincelA; btnPincel[1] = pincelB;
            btnPincel[2] = pincelC; btnPincel[3] = pincelD;
            btnPincel[4] = pincelE;

            btnPincel[5] = pincelG;
            btnPincel[6] = pincelH; btnPincel[7] = pincelI;
            btnPincel[8] = pincelJ;

            btnPincel2[0] = pincelK;
            btnPincel2[1] = pincelL;

            return btnPincel;
        }

        private void ComportamientoObjetoInicio()
        {
            pathArchivo = Valor.GetPathArchivo();
            if (pathArchivo == string.Empty)
            {
                return;
            }
            this.MaximumSize = new Size(1161, 680);
            this.Size = new Size(1161, 680);
            this.Text = EngineData.Titulo + EngineData.Numeros;
            txtBlueView.Text = EngineData.azul1_5;
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            btnPincel= AsociarBtnPincel(btnPincel);
            btnPincel = Funcion.ColoresPincel(btnPincel);
            ArrayList arrText = Funcion.AbrirValoresArchivo(pathArchivo);
            valorIngresado = Funcion.SetValorIngresado(arrText, valorIngresado);
            valorEliminado = Funcion.SetValorEliminado(arrText, valorEliminado);
            valorInicio = Funcion.SetValorInicio(arrText, valorInicio);
            valorSolucion = Funcion.SetValorSolucion(arrText, valorSolucion);
            bool resultado = Funcion.ExisteValorIngresado(valorIngresado);
            if (resultado)
            {
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                txtSudoku = Funcion.SetearTextBoxJuego(txtSudoku, valorIngresado, valorCandidato, valorInicio, colorA: Color.Blue, colorB: Color.Blue, lado: EngineData.Left);
            }
            else
            {
                valorIngresado = valorInicio;
                valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                txtSudoku = Funcion.SetearTextBoxJuego(txtSudoku, valorIngresado, valorCandidato, valorInicio, colorA: Color.Blue, colorB: Color.Blue, lado: EngineData.Left);
            }
            ContadorIngresado();
        }

        private void AplicarIdioma()
        {
            this.Text = RecursosLocalizables.StringResources.FormularioAzulUno;
            mIdiomas.Text = RecursosLocalizables.StringResources.mIdiomas;
            mArchivo.Text = RecursosLocalizables.StringResources.mArchivo;
            mColores.Text = RecursosLocalizables.StringResources.mColores;
            mTablero.Text = RecursosLocalizables.StringResources.mTablero;
            mContadores.Text = RecursosLocalizables.StringResources.mContadores;
            crearJuego.Text = RecursosLocalizables.StringResources.crearJuego;
            abrirJuego.Text = RecursosLocalizables.StringResources.abrirJuego;
            guardar.Text = RecursosLocalizables.StringResources.guardar;
            guardarComo.Text = RecursosLocalizables.StringResources.guardarComo;
            reiniciar.Text = RecursosLocalizables.StringResources.reiniciar;
            configuracion.Text = RecursosLocalizables.StringResources.configuracion;
            activar.Text = RecursosLocalizables.StringResources.activar;
            desactivar.Text = RecursosLocalizables.StringResources.desactivar;
            btnAyuda.Text = RecursosLocalizables.StringResources.btnAyuda;
        }

        private void Lenguaje_Click(object sender, EventArgs e)
        {
            EngineData Valor = EngineData.Instance();
            ToolStripMenuItem toolStrip = sender as ToolStripMenuItem;
            switch (toolStrip.Name)
            {
                case (EngineData.Español):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaEspañol);
                    Valor.SetIdioma(EngineData.LenguajeEspañol);
                    break;
                case (EngineData.Ingles):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaIngles);
                    Valor.SetIdioma(EngineData.LenguajeIngles);
                    break;
                case (EngineData.Portugues):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaPortugues);
                    Valor.SetIdioma(EngineData.LenguajePortugues);
                    break;
            }
            AplicarIdioma();
        }

        private void ColorMarcador_Click(object sender, EventArgs e)
        {
            Button pincel = (Button)sender;
            if (pincel.Name == "pincelK" || pincel.Name == "pincelL")
            {
                if (pincel.BackColor == Color.Silver)
                {
                    pincelMarcador = EngineData.Falso;
                    txtSudoku = Funcion.SetearTextColorInicioSinAmarillo(txtSudoku);
                 
                }
                else
                {
                    pincelMarcador = EngineData.Verdadero;
                    colorFondoAct = pincel.BackColor;
                }
            }
            else
            {
                if (pincel.BackColor == Color.Silver)
                {
                    pincelMarcador = EngineData.Falso;
                    txtSudoku = Funcion.SetearTextColorInicioConAmarillo(txtSudoku);
                    btnSelectColor.BackColor = Color.Silver;
                    btnSelectColor.FlatAppearance.BorderColor = Color.Silver;
                    btnSelectColor.FlatAppearance.BorderSize = EngineData.one;
                }
                else
                {
                    pincelMarcador = EngineData.Verdadero;
                    colorFondoAct = pincel.BackColor;
                    btnSelectColor.BackColor = colorFondoAct;
                    btnSelectColor.FlatAppearance.BorderColor = Color.Black;
                    btnSelectColor.FlatAppearance.BorderSize = EngineData.two;
                }
            }
        }

        private void NavegacionVistas(object sender, EventArgs e)
        {
            string vistazul = txtBlueView.Text;
            Button btn = (Button)sender;

            if (vistazul == EngineData.azul1_5) txtBlueView.Text = EngineData.azul5_5;
            if (vistazul == EngineData.azul2_5) txtBlueView.Text = EngineData.azul1_5;
            if (vistazul == EngineData.azul3_5) txtBlueView.Text = EngineData.azul2_5;
            if (vistazul == EngineData.azul4_5) txtBlueView.Text = EngineData.azul3_5;
            if (vistazul == EngineData.azul5_5) txtBlueView.Text = EngineData.azul4_5;

            ComportamientoVistaAzul(txtBlueView.Text);
        }

        private void ComportamientoVistaAzul(string vista)
        {
            switch (vista)
            {
                case (EngineData.azul1_5):
                    break;
                case (EngineData.azul2_5):
                    break;
                case (EngineData.azul3_5):
                    break;
                case (EngineData.azul4_5):
                    break;
                case (EngineData.azul5_5):
                    break;
            }
        }

        private void ContadorIngresado()
        {
           contadorIngresado = Funcion.ContadorIngresado(valorIngresado);
           SetSoloOculto();
           SetLetrasJuegoACB();
           SetLetrasJuegoFEG();
        }

        private void SetLetrasJuegoFEG()
        {
            LetrasJuegoFEG = Funcion.SetLetrasJuegoFEG(contadorIngresado, valorIngresado, valorCandidatoSinEliminados);
            btnF.Text = LetrasJuegoFEG.F.ToString();
            btnE.Text = LetrasJuegoFEG.E.ToString();
            btnG.Text = LetrasJuegoFEG.G.ToString();
        }

        private void SetLetrasJuegoACB()
        {
            LetrasJuegoACB = Funcion.SetLetrasJuegoACB(solo, oculto);
            btnA.Text = LetrasJuegoACB.A.ToString();
            btnB.Text = LetrasJuegoACB.B.ToString();
            if (LetrasJuegoACB.A + LetrasJuegoACB.B > 0)
            {
                btnBB.Visible = EngineData.Falso;
            }
            else
            {
                btnBB.Visible = EngineData.Verdadero;
            }
            if (!LetrasJuegoACB.C) btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Look));
            else btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
        }

        private void SetSoloOculto()
        {
            solo = Funcion.CandidatoSolo(valorIngresado, valorCandidatoSinEliminados);
            oculto = new string[27];
            ListBox valor = new ListBox();
            for (int f = 0; f <= 8; f++)
            {
                valor = Funcion.MapeoFilaCandidatoOcultoFila(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = Funcion.SetearOcultoFila(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Funcion.MapeoFilaCandidatoOcultoColumna(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = Funcion.SetearOcultoColumna(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Funcion.MapeoFilaCandidatoOcultoRecuadro(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = Funcion.SetearOcultoRecuadro(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
            }
        }

        private void txt00_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));

            if (valorInicio[row, col] != null && valorInicio[row, col] != string.Empty)
                txt.ForeColor = Color.Black;
            else txt.ForeColor = Color.Blue;

            if (pincelMarcador)
            {
                txtSudoku[row, col].BackColor = colorFondoAct;
            }
            else
            {
                colorCeldaAnt = txt.BackColor;
                txt.BackColor = Valor.GetColorCeldaAct();
            }
        }

        private void txt00_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
                if (txt.Text.Length > 0) { txt.Text = string.Empty; }
            }
        }

        private void txt00_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));
            try
            {
                if (txt.Text == EngineData.Zero)
                {
                    txt.Text = string.Empty;
                    valorIngresado[row, col] = string.Empty;
                }
                else
                {
                    valorIngresado[row, col] = txt.Text;
                   
                    if (valorInicio[row, col] != null && valorInicio[row, col] != string.Empty)
                    {
                        txt.Text = valorInicio[row, col];
                    }
                }
                valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                ContadorIngresado();
            }
            catch { }

            string sentido = e.KeyCode.ToString();
            if (sentido == EngineData.Up || sentido == EngineData.Down || sentido == EngineData.Right || sentido == EngineData.Left)
            {
                try
                {
                    position = Funcion.Position(sentido, row, col);
                    txtSudoku[position[0], position[1]].Focus();
                }
                catch { txtSudoku[row, col].Focus(); }
                return;
            }
        }

        private void txt00_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            txt.BackColor = Color.WhiteSmoke;
        }

        private void txt00_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));
            if (!pincelMarcador)
            {
                txt.BackColor = colorCeldaAnt;
            }
        }

    }
}
