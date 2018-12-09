using System;
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
    public partial class RojoTres : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        //***********************************************************************************************************

        private EngineSudoku Funcion = new EngineSudoku();
        private EngineData Valor = EngineData.Instance();
        private EngineSudoku.LetrasJuegoFEG LetrasJuegoFEG = new EngineSudoku.LetrasJuegoFEG();
        private EngineSudoku.LetrasJuegoACB LetrasJuegoACB = new EngineSudoku.LetrasJuegoACB();

        private TextBox[,] txtSudoku = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DEL SUDOKU
        private TextBox[,] txtSudoku2 = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DE CANDIDATOS
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorCandidatoSinEliminados = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        //private string[,] valorSolucion = new string[9, 9];
        private Button[] btnPincel = new Button[9];// ARRAY CONTENTIVO DE LOS BOTONES DE PINCELES IZQUIERDO
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
        private bool contadorActivado = EngineData.Falso;

        string idiomaCultura = string.Empty;
        string idiomaNombre = string.Empty;
        string lado = string.Empty;
        int numeroFiltrado = 0;
        private string[,] valorFiltrado = new string[9, 9];
        private static RojoDos F = new RojoDos();
        private static AzulDos G = new AzulDos();


        public RojoTres()
        {
            InitializeComponent();
        }


        private void RojoTres_Activated(object sender, EventArgs e)
        {
            valorInicio = Valor.GetValorInicio();
            valorIngresado = Valor.GetValorIngresado();
            valorEliminado = Valor.GetValorEliminado();
            SetearJuego();
            ContadorIngresado();
        }

        private void RojoTres_Load(object sender, EventArgs e)
        {
            idiomaCultura = Valor.GetIdioma();
            if (idiomaCultura == string.Empty)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaEspañol);
                Valor.SetIdioma(EngineData.CulturaEspañol);
                Valor.SetNombreIdioma(EngineData.LenguajeEspañol);
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(idiomaCultura);
                Valor.SetIdioma(idiomaCultura);
                idiomaNombre = Valor.NombreIdiomaCultura(idiomaCultura);
                Valor.SetNombreIdioma(idiomaNombre);
            }
            AplicarIdioma();
            ComportamientoObjetoInicio();
        }

        private void AplicarIdioma()
        {
            this.Text = Valor.TituloFormR3(Valor.GetNombreIdioma()) + Valor.GetNombreJuego();
            mIdiomas.Text = RecursosLocalizables.StringResources.mIdiomas;
            mContadores.Text = RecursosLocalizables.StringResources.mContadores;
            activar.Text = RecursosLocalizables.StringResources.activar;
            desactivar.Text = RecursosLocalizables.StringResources.desactivar;
            string etiqueta = Valor.EtiquetaRojoTres(Valor.GetNombreIdioma());
            string[] p = etiqueta.Split('/');
            label2.Text = p[0].ToUpper();
            label3.Text = p[1].ToUpper();
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
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            btnPincel = AsociarBtnPincel(btnPincel);
            btnPincel = Funcion.ColoresPincel(btnPincel);
            ActivarDesactivarContadores(EngineData.Falso);
        }

        private void SetearJuego()
        {
            valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
            valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
            txtSudoku2 = Funcion.SetearTextBoxCandidatos(txtSudoku2, valorIngresado , valorCandidatoSinEliminados);
            ActualizarContadoresCandidatos();
        }

        private void ContadorIngresado()
        {
            contadorIngresado = Funcion.ContadorIngresado(valorIngresado);
            SetSoloOculto();
            SetLetrasJuegoACB();
            SetLetrasJuegoFEG();
            if (!contadorActivado)
            {
                btnA.Visible = EngineData.Falso;
                btnB.Visible = EngineData.Falso;
            }
            else
            {
                btnA.Visible = EngineData.Verdadero;
                btnB.Visible = EngineData.Verdadero;
            }
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

        private void SetLetrasJuegoACB()
        {
            LetrasJuegoACB = Funcion.SetLetrasJuegoACB(solo, oculto);
            btnA.Text = LetrasJuegoACB.A.ToString();
            btnB.Text = LetrasJuegoACB.B.ToString();
            if (!LetrasJuegoACB.C)
            {
                btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Look));
            }
            else
            {
                btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
            }
        }

        private void SetLetrasJuegoFEG()
        {
            LetrasJuegoFEG = Funcion.SetLetrasJuegoFEG(contadorIngresado, valorIngresado, valorCandidatoSinEliminados);
            btnC.Visible = Funcion.Visibilidad70(LetrasJuegoFEG.F);
            btnF.Text = LetrasJuegoFEG.F.ToString();
            btnE.Text = LetrasJuegoFEG.E.ToString();
            btnG.Text = LetrasJuegoFEG.G.ToString();
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

        private TextBox[,] AsociarTxtMatriz2(TextBox[,] txtSudoku2)
        {
            /////////////////////////////////////////////////////////////////////////////
            txtSudoku2[0, 0] = t00; txtSudoku2[0, 1] = t01; txtSudoku2[0, 2] = t02;
            txtSudoku2[1, 0] = t10; txtSudoku2[1, 1] = t11; txtSudoku2[1, 2] = t12;
            txtSudoku2[2, 0] = t20; txtSudoku2[2, 1] = t21; txtSudoku2[2, 2] = t22;

            txtSudoku2[0, 3] = t03; txtSudoku2[0, 4] = t04; txtSudoku2[0, 5] = t05;
            txtSudoku2[1, 3] = t13; txtSudoku2[1, 4] = t14; txtSudoku2[1, 5] = t15;
            txtSudoku2[2, 3] = t23; txtSudoku2[2, 4] = t24; txtSudoku2[2, 5] = t25;

            txtSudoku2[0, 6] = t06; txtSudoku2[0, 7] = t07; txtSudoku2[0, 8] = t08;
            txtSudoku2[1, 6] = t16; txtSudoku2[1, 7] = t17; txtSudoku2[1, 8] = t18;
            txtSudoku2[2, 6] = t26; txtSudoku2[2, 7] = t27; txtSudoku2[2, 8] = t28;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku2[3, 0] = t30; txtSudoku2[3, 1] = t31; txtSudoku2[3, 2] = t32;
            txtSudoku2[4, 0] = t40; txtSudoku2[4, 1] = t41; txtSudoku2[4, 2] = t42;
            txtSudoku2[5, 0] = t50; txtSudoku2[5, 1] = t51; txtSudoku2[5, 2] = t52;

            txtSudoku2[3, 3] = t33; txtSudoku2[3, 4] = t34; txtSudoku2[3, 5] = t35;
            txtSudoku2[4, 3] = t43; txtSudoku2[4, 4] = t44; txtSudoku2[4, 5] = t45;
            txtSudoku2[5, 3] = t53; txtSudoku2[5, 4] = t54; txtSudoku2[5, 5] = t55;

            txtSudoku2[3, 6] = t36; txtSudoku2[3, 7] = t37; txtSudoku2[3, 8] = t38;
            txtSudoku2[4, 6] = t46; txtSudoku2[4, 7] = t47; txtSudoku2[4, 8] = t48;
            txtSudoku2[5, 6] = t56; txtSudoku2[5, 7] = t57; txtSudoku2[5, 8] = t58;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku2[6, 0] = t60; txtSudoku2[6, 1] = t61; txtSudoku2[6, 2] = t62;
            txtSudoku2[7, 0] = t70; txtSudoku2[7, 1] = t71; txtSudoku2[7, 2] = t72;
            txtSudoku2[8, 0] = t80; txtSudoku2[8, 1] = t81; txtSudoku2[8, 2] = t82;

            txtSudoku2[6, 3] = t63; txtSudoku2[6, 4] = t64; txtSudoku2[6, 5] = t65;
            txtSudoku2[7, 3] = t73; txtSudoku2[7, 4] = t74; txtSudoku2[7, 5] = t75;
            txtSudoku2[8, 3] = t83; txtSudoku2[8, 4] = t84; txtSudoku2[8, 5] = t85;

            txtSudoku2[6, 6] = t66; txtSudoku2[6, 7] = t67; txtSudoku2[6, 8] = t68;
            txtSudoku2[7, 6] = t76; txtSudoku2[7, 7] = t77; txtSudoku2[7, 8] = t78;
            txtSudoku2[8, 6] = t86; txtSudoku2[8, 7] = t87; txtSudoku2[8, 8] = t88;
            ////////////////////////////////////////////////////////////////////////////
            foreach (TextBox item in txtSudoku2)
            {
                item.GotFocus += delegate { HideCaret(item.Handle); };
                //item.ReadOnly = true;
            }
            return txtSudoku2;
        }

        private Button[] AsociarBtnPincel(Button[] btnPincel)
        {
            btnPincel[0] = pincelA; btnPincel[1] = pincelB;
            btnPincel[2] = pincelC; btnPincel[3] = pincelD;
            btnPincel[4] = pincelE;

            btnPincel[5] = pincelG;
            btnPincel[6] = pincelH; btnPincel[7] = pincelI;
            btnPincel[8] = pincelJ;

            return btnPincel;
        }

        private void ActivarDesactivarContadores(bool action)
        {
            if (action)
            {
                activar.Checked = EngineData.Verdadero;
                desactivar.Checked = EngineData.Falso;
                contadorActivado = EngineData.Verdadero;
            }
            else
            {
                desactivar.Checked = EngineData.Verdadero;
                activar.Checked = EngineData.Falso;
                contadorActivado = EngineData.Falso;
            }
        }

        private void ActualizarContadoresCandidatos()
        {
            lbl1.Text = Funcion.ContadorCandidatoEspecifico(EngineData.uno, valorIngresado, valorCandidatoSinEliminados);
            lbl2.Text = Funcion.ContadorCandidatoEspecifico(EngineData.dos, valorIngresado, valorCandidatoSinEliminados);
            lbl3.Text = Funcion.ContadorCandidatoEspecifico(EngineData.tres, valorIngresado, valorCandidatoSinEliminados);
            lbl4.Text = Funcion.ContadorCandidatoEspecifico(EngineData.cuatro, valorIngresado, valorCandidatoSinEliminados);
            lbl5.Text = Funcion.ContadorCandidatoEspecifico(EngineData.cinco, valorIngresado, valorCandidatoSinEliminados);
            lbl6.Text = Funcion.ContadorCandidatoEspecifico(EngineData.seis, valorIngresado, valorCandidatoSinEliminados);
            lbl7.Text = Funcion.ContadorCandidatoEspecifico(EngineData.siete, valorIngresado, valorCandidatoSinEliminados);
            lbl8.Text = Funcion.ContadorCandidatoEspecifico(EngineData.ocho, valorIngresado, valorCandidatoSinEliminados);
            lbl9.Text = Funcion.ContadorCandidatoEspecifico(EngineData.nueve, valorIngresado, valorCandidatoSinEliminados);
        }

        private void ContadoresCandidatos_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case (EngineData.Btn1):
                    lbl1.Text = Funcion.ContadorCandidatoEspecifico(EngineData.uno, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.uno, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 1;
                    break;
                case (EngineData.Btn2):
                    lbl2.Text = Funcion.ContadorCandidatoEspecifico(EngineData.dos, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.dos, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 2;
                    break;
                case (EngineData.Btn3):
                    lbl3.Text = Funcion.ContadorCandidatoEspecifico(EngineData.tres, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.tres, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 3;
                    break;
                case (EngineData.Btn4):
                    lbl4.Text = Funcion.ContadorCandidatoEspecifico(EngineData.cuatro, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.cuatro, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 4;
                    break;
                case (EngineData.Btn5):
                    lbl5.Text = Funcion.ContadorCandidatoEspecifico(EngineData.cinco, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.cinco, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 5;
                    break;
                case (EngineData.Btn6):
                    lbl6.Text = Funcion.ContadorCandidatoEspecifico(EngineData.seis, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.seis, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 6;
                    break;
                case (EngineData.Btn7):
                    lbl7.Text = Funcion.ContadorCandidatoEspecifico(EngineData.siete, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.siete, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 7;
                    break;
                case (EngineData.Btn8):
                    lbl8.Text = Funcion.ContadorCandidatoEspecifico(EngineData.ocho, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.ocho, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 8;
                    break;
                case (EngineData.Btn9):
                    lbl9.Text = Funcion.ContadorCandidatoEspecifico(EngineData.nueve, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Funcion.SetearTextBoxCandidatoEspecifico(EngineData.nueve, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 9;
                    break;
            }
            valorFiltrado = Valor.GetNumFiltro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Valor.SetValorIngresado(valorIngresado);
            Valor.SetValorInicio(valorInicio);
            Valor.SetValorEliminado(valorEliminado);
            F.Show();
            this.Hide();
        }

        private void ColorMarcador_Click(object sender, EventArgs e)
        {
            Button pincel = (Button)sender;
            if (pincel.BackColor == Color.Silver)
            {
                pincelMarcador = EngineData.Falso;
                txtSudoku = Funcion.SetearTextColorInicio(txtSudoku);
                txtSudoku2 = Funcion.SetearTextColorInicio(txtSudoku2);
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

        private void EliminarRestablecerCandidato_Click(object sender, EventArgs e)
        {
            if (lado != EngineData.btnIzquierda) return;
            string candidatoEliminar = txtSudoku[row, col].Text;
            if (candidatoEliminar == string.Empty) return;
            if (valorEliminado[row, col] != null)
            {
                if (valorEliminado[row, col].Contains(candidatoEliminar))
                {
                    ActualizarContadoresCandidatos();
                    SetearJuego();
                    return;
                }
            }
            valorEliminado[row, col] = valorEliminado[row, col] + " " + candidatoEliminar;
            valorEliminado[row, col] = Funcion.OrdenarCadena(valorEliminado[row, col]);
            txtSudoku[row, col].BackColor = Color.WhiteSmoke;
            txtSudoku[row, col].Text = string.Empty;
            ActualizarContadoresCandidatos();
            SetearJuego();
            ContadorIngresado();
        }

        private void btnAA_Click(object sender, EventArgs e)
        {
            Valor.SetValorIngresado(valorIngresado);
            Valor.SetValorInicio(valorInicio);
            Valor.SetValorEliminado(valorEliminado);
            G.Show();
            this.Hide();
        }

        private void Lenguaje_Click(object sender, EventArgs e)
        {
            EngineData Valor = EngineData.Instance();
            ToolStripMenuItem toolStrip = sender as ToolStripMenuItem;
            switch (toolStrip.Name)
            {
                case (EngineData.Español):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaEspañol);
                    Valor.SetIdioma(EngineData.CulturaEspañol);
                    Valor.SetNombreIdioma(EngineData.LenguajeEspañol);
                    break;
                case (EngineData.Ingles):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaIngles);
                    Valor.SetIdioma(EngineData.CulturaIngles);
                    Valor.SetNombreIdioma(EngineData.LenguajeIngles);
                    break;
                case (EngineData.Portugues):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(EngineData.CulturaPortugues);
                    Valor.SetIdioma(EngineData.CulturaPortugues);
                    Valor.SetNombreIdioma(EngineData.LenguajePortugues);
                    break;
            }
            AplicarIdioma();
        }

        private void Filtro23Candidatos_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case (EngineData.BtnRes23):
                    txtSudoku2 = Funcion.SetearTextColorInicio(txtSudoku2);
                    break;
                case (EngineData.BtnDos):
                    txtSudoku2 = Funcion.SetearTextColorInicio(txtSudoku2);
                    txtSudoku2 = Funcion.CandidatosFinalistas2(txtSudoku2, Color.Chartreuse);
                    break;
                case (EngineData.BtnTres):
                    txtSudoku2 = Funcion.SetearTextColorInicio(txtSudoku2);
                    txtSudoku2 = Funcion.CandidatosFinalistas3(txtSudoku2, Color.Orange,Color.Chartreuse);
                    break;
            }
        }

        //*************************************************************************************************
        private void txt00_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));

            if (pincelMarcador)
            {
                txtSudoku[row, col].BackColor = colorFondoAct;
            }
            else
            {
                colorCeldaAnt = txt.BackColor;
                txt.BackColor = Valor.GetColorCeldaAct();
            }

            lado = EngineData.btnIzquierda;
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

            if (txt.Text == EngineData.Zero)
            {
                txt.Text = valorFiltrado[row, col];
            }
            else
            {
                if (txt.Text != valorFiltrado[row, col])
                {
                    txt.Text = valorFiltrado[row, col];
                }
            }


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
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));

            if (txt.Text == string.Empty)
                txt.BackColor = Color.WhiteSmoke;
            else
                txt.BackColor = Color.LightCyan;
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

        //****************************************************************************************************
        private void t00_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));
            if (pincelMarcador)
            {
                txtSudoku2[row, col].BackColor = colorFondoAct;
            }
            else
            {
                colorCeldaAnt = txt.BackColor;
                txt.BackColor = Valor.GetColorCeldaAct();
            }
            lado = EngineData.btnDerecha;
        }

        private void t00_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));

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

        private void t00_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));

            SetearJuego();

            string sentido = e.KeyCode.ToString();
            if (sentido == EngineData.Up || sentido == EngineData.Down || sentido == EngineData.Right || sentido == EngineData.Left)
            {
                try
                {
                    position = Funcion.Position(sentido, row, col);
                    txtSudoku2[position[0], position[1]].Focus();
                }
                catch { txtSudoku2[row, col].Focus(); }
                return;
            }
        }

        private void t00_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));
            txt.BackColor = Color.WhiteSmoke;
        }

        private void t00_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));
            if (!pincelMarcador)
            {
                txt.BackColor = colorCeldaAnt;
            }
        }

        private void RojoTres_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }


        //**************************************************************************************************
    }
}
