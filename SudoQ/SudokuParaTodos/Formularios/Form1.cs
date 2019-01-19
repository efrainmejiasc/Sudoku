using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace SudokuParaTodos
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        //***********************************************************************************************************
        private EngineSudoku Funcion = new EngineSudoku();
        private EngineData Valor = EngineData.Instance();
        private EngineSudoku.LetrasJuegoFEG LetrasJuegoFEG = new EngineSudoku.LetrasJuegoFEG();
        private EngineSudoku.LetrasJuegoACB LetrasJuegoACB= new EngineSudoku.LetrasJuegoACB();

        private TextBox[,] txtSudoku = new TextBox[9,9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DEL SUDOKU
        private TextBox[,] txtSudoku2 = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DE CANDIDATOS
        private Button[] btnPincel = new Button[9];// ARRAY CONTENTIVO DE LOS BOTONES DE PINCELES IZQUIERDO
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorCandidatoSinEliminados = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];
        private string [] solo = new string[27];
        private string [] oculto = new string[27];

        private int[] position = new int[2];
        private string openFrom = string.Empty;
        private bool vInit = EngineData.Falso;//Valores Iniciales 
        private int contadorIngresado = 0;
        private int row = -1;
        private int col = -1;
        private Color colorFondoAct;
        private bool pincelMarcador = EngineData.Falso;
        private Color colorCeldaAnt;

        private bool lenguajeSi = EngineData.Falso;


        public Form1()
        {
            InitializeComponent();
            LimpiarParaNuevoJuego();
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            SetPantallaInicio();
        }

        public Form1(string idioma)
        {
            InitializeComponent();
            mIdiomas.Visible = false;
            LimpiarParaNuevoJuego();
            lblSudoku.Visible = EngineData.Falso;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(idioma);
            Valor.SetIdioma(idioma);
            Funcion.AsociarExtension();
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            btnPincel = AsociarBtnPincel(btnPincel);
            ComportamientoObjInicio2();
            ComportamientoObjExpandido();
            AplicarIdioma();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = EngineData.Titulo;
            if (!Funcion.ExisteClaveRegWin()){Funcion.AgregarClaveRegWin();}
            Funcion.AsociarExtension();
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            btnPincel = AsociarBtnPincel(btnPincel);
            ComportamientoObjInicio();
            ComportamientoObjExpandido();
        }

        private void LimpiarParaNuevoJuego()
        {
            txtSudoku = Funcion.SetearTextBoxLimpio(txtSudoku);
            txtSudoku2 = Funcion.SetearTextBoxLimpio(txtSudoku2);
            valorIngresado = new string[9, 9];
            valorEliminado = new string[9, 9];
            valorInicio = new string[9, 9];
            valorSolucion = new string[9, 9];
            valorCandidatoSinEliminados = new string[9, 9];
            valorCandidato = new string[9, 9];
            Valor.SetValorIngresado(valorIngresado);
            Valor.SetValorEliminado(valorEliminado);
            Valor.SetValorInicio(valorInicio);
            Valor.SetValorSolucion(valorSolucion);
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
                item.GotFocus += delegate { HideCaret(item.Handle); } ;
                item.ReadOnly = EngineData.Verdadero;
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

        private Button[] AsociarBtnPincel(Button [] btnPincel)
        {
            btnPincel[0] = pincelA; btnPincel[1] = pincelB;
            btnPincel[2] = pincelC; btnPincel[3] = pincelD;
            btnPincel[4] = pincelE;

            btnPincel[5] = pincelG;
            btnPincel[6] = pincelH; btnPincel[7] = pincelI;
            btnPincel[8] = pincelJ;
            return btnPincel;
        }

        private void ComportamientoObjInicio()
        {
            this.Size = new Size(586 , 680);
            this.MaximumSize = new Size(586, 680);
            btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
            mArchivo.Visible = EngineData.Falso;
            mTablero.Visible = EngineData.Falso;
            mColores.Visible = EngineData.Falso;
            mContadores.Visible = EngineData.Falso;
            foreach (Button btn in btnPincel) { btn.Visible = EngineData.Falso; }
            pnlJuego.Visible = EngineData.Falso;
            mArchivo.Visible = EngineData.Falso;
            mTablero.Visible = EngineData.Falso;
            mColores.Visible = EngineData.Falso;
            mContadores.Visible = EngineData.Falso;
            btnA.Visible = EngineData.Falso;
            btnB.Visible = EngineData.Falso;
            btnC.Visible = EngineData.Falso;
            pnlLetra.Visible = EngineData.Falso;
            valorCandidato = Funcion.CandidatosJuego(valorSolucion, valorCandidato);
            valorCandidatoSinEliminados = valorCandidato;
            txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorSolucion, valorCandidato, valorInicio,Color.Green, Color.Blue);
     

            string idioma = CultureInfo.InstalledUICulture.NativeName;
            if (idioma.Contains(EngineData.english)) mIdiomas.Text = EngineData.LANGUAGES;
            else if (idioma.Contains(EngineData.english)) mIdiomas.Text = EngineData.LANGUAGES;
            else mIdiomas.Text = EngineData.IDIOMAS;
        }

        private void SetPantallaInicio()
        {
            string[,] pantallaIni = new string[9, 9];
            pantallaIni[0, 0] = "7";
            txtSudoku[0, 0].ForeColor = Color.Green;
            pantallaIni[0, 7] = "8";
            txtSudoku[0, 7].ForeColor = Color.Blue;
            pantallaIni[0, 8] = "1";
            txtSudoku[0, 8].ForeColor = Color.Orange;
            pantallaIni[1, 7] = "5";
            txtSudoku[1, 7].ForeColor = Color.Crimson;
            pantallaIni[2, 1] = "2";
            txtSudoku[2, 1].ForeColor = Color.Red;
            pantallaIni[2, 3] = "5";
            txtSudoku[2, 3].ForeColor = Color.YellowGreen;
            pantallaIni[2, 4] = "9";
            txtSudoku[2, 4].ForeColor = Color.Violet;
            pantallaIni[3, 0] = "4";
            txtSudoku[3, 0].ForeColor = Color.Black;
            pantallaIni[4, 2] = "6";
            txtSudoku[4, 2].ForeColor = Color.Cyan;
            pantallaIni[4, 4] = "3";
            txtSudoku[4, 4].ForeColor = Color.Cyan;
            pantallaIni[5, 4] = "4";
            txtSudoku[5, 4].ForeColor = Color.Green;
            pantallaIni[3, 6] = "8";
            txtSudoku[3, 6].ForeColor = Color.Orange;
            pantallaIni[6, 1] = "1";
            txtSudoku[6, 1].ForeColor = Color.Blue;
            pantallaIni[6, 3] = "9";
            txtSudoku[6, 3].ForeColor = Color.Black;
            pantallaIni[6, 5] = "6";
            txtSudoku[6, 5].ForeColor = Color.Violet;
            pantallaIni[5, 8] = "2";
            txtSudoku[5, 8].ForeColor = Color.Crimson;
            pantallaIni[7, 6] = "7";
            txtSudoku[7, 6].ForeColor = Color.YellowGreen;
            pantallaIni[8, 6] = "5";
            txtSudoku[8, 6].ForeColor = Color.Cyan;
            pantallaIni[8, 0] = "3";
            txtSudoku[8, 0].ForeColor = Color.Orange;
            pantallaIni[8, 8] = "9";
            txtSudoku[8, 8].ForeColor = Color.Red;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    txtSudoku[f, c].Text = pantallaIni[f, c];
                }
            }

        }

        private void ComportamientoObjInicio2()
        {
            this.Size = new Size(586, 680);
            this.MaximumSize = new Size(586, 680);
            btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
            mArchivo.Visible = EngineData.Falso;
            mTablero.Visible = EngineData.Falso;
            mColores.Visible = EngineData.Falso;
            mContadores.Visible = EngineData.Falso;
            foreach (Button btn in btnPincel) { btn.Visible = EngineData.Falso; }
            pnlJuego.Visible = EngineData.Falso;
            mArchivo.Visible = EngineData.Falso;
            mTablero.Visible = EngineData.Falso;
            mColores.Visible = EngineData.Falso;
            mContadores.Visible = EngineData.Falso;
            btnA.Visible = EngineData.Falso;
            btnB.Visible = EngineData.Falso;
            btnC.Visible = EngineData.Falso;
            pnlLetra.Visible = EngineData.Falso;
            valorCandidato = Funcion.CandidatosJuego(valorSolucion, valorCandidato);
            valorCandidatoSinEliminados = valorCandidato;
            txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorSolucion, valorCandidato, valorInicio, Color.Green, Color.Blue);
            lenguajeSi = EngineData.Verdadero;
    }

        private void ComportamientoObjExpandido()
        {
            if (Valor.GetIdioma() != string.Empty)
            {
                this.MaximumSize = new Size(1161, 680);
                this.Size = new Size(1161 , 680);
                this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                foreach (Button btn in btnPincel) { btn.Visible = EngineData.Verdadero; }
                btnPincel = Funcion.ColoresPincel(btnPincel);
                pnlJuego.Visible = EngineData.Verdadero;
                btnAbrir.Visible = EngineData.Verdadero;
                btnOtro.Visible = EngineData.Falso;
                btnGuardar.Visible = EngineData.Falso;
                btnSolucion.Visible = EngineData.Falso;
                pnlLetra.Visible = EngineData.Verdadero;
                foreach (TextBox item in txtSudoku)
                {
                    item.GotFocus += delegate { HideCaret(item.Handle); };
                    item.ReadOnly = false;
                }
                SetLetrasJuegoFEG();
            }
        }

        private void AplicarIdioma()
        {
            this.Text = RecursosLocalizables.StringResources.Form1;
            mIdiomas.Text = RecursosLocalizables.StringResources.mIdiomas;
            mArchivo.Text = RecursosLocalizables.StringResources.mArchivo;
            mColores.Text = RecursosLocalizables.StringResources.mIdiomas;
            mTablero.Text = RecursosLocalizables.StringResources.mTablero;
            mContadores.Text = RecursosLocalizables.StringResources.mContadores;
            btnAbrir.Text = RecursosLocalizables.StringResources.btnAbrir;
            btnGuardar.Text = RecursosLocalizables.StringResources.btnGuardar;
            btnOtro.Text =  RecursosLocalizables.StringResources.btnOtro;
            btnSolucion.Text = RecursosLocalizables.StringResources.btnSolucion;
            this.Text = Valor.TituloForm(Valor.GetNombreIdioma());
            string etiqueta = Valor.EiquetaCrearJuego(Valor.GetNombreIdioma());
            string [] p = etiqueta.Split('/');
            label1.Text = p[0].ToUpper();
            label2.Text = p[1].ToUpper();
        }

        private int ContadorIngresado()
        {
            contadorIngresado = Funcion.ContadorIngresado(valorSolucion);
            if (contadorIngresado >= 17)
            {
                if (!vInit)
                {
                    btnGuardar.Visible = EngineData.Verdadero;
                    try { txtSudoku[row, col].Focus(); } catch { }
                    SetSoloOculto();
                    SetLetrasJuegoACB();
                }
            }
            else if (contadorIngresado < 17)
            {
                btnGuardar.Visible = EngineData.Falso;
                btnA.Visible = EngineData.Falso;
                btnB.Visible = EngineData.Falso;
                btnC.Visible = EngineData.Falso;
            }
            if (contadorIngresado == 81)
            {
                btnSolucion.Visible = EngineData.Verdadero;
                btnAbrir.Visible = EngineData.Falso;
                btnOtro.Visible = EngineData.Falso;
                btnGuardar.Visible = EngineData.Falso;
            }
            else if (contadorIngresado < 81)
            {
                btnSolucion.Visible = EngineData.Falso;
            }
            SetLetrasJuegoFEG();
            VisibilidadACB();
            return contadorIngresado;
        }

        private void SetSoloOculto()
        {
            solo = Funcion.CandidatoSolo(valorSolucion, valorCandidatoSinEliminados);
            oculto = new string[27];
            ListBox valor = new ListBox();
            for (int f = 0; f <= 8; f++)
            {
                valor = Funcion.MapeoFilaCandidatoOcultoFila(valorSolucion, valorCandidatoSinEliminados, f);
                oculto = Funcion.SetearOcultoFila(oculto, valor, f,valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Funcion.MapeoFilaCandidatoOcultoColumna(valorSolucion, valorCandidatoSinEliminados, f);
                oculto = Funcion.SetearOcultoColumna(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Funcion.MapeoFilaCandidatoOcultoRecuadro(valorSolucion, valorCandidatoSinEliminados, f);
                oculto = Funcion.SetearOcultoRecuadro(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
            }            
        }

        private void SetLetrasJuegoFEG()
        {
            LetrasJuegoFEG = Funcion.SetLetrasJuegoFEG(contadorIngresado, valorSolucion, valorCandidatoSinEliminados);
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
                btnA.Visible = EngineData.Verdadero;
                btnB.Visible = EngineData.Verdadero;
                btnC.Visible = EngineData.Verdadero;
            }
            else
            {
                btnA.Visible = EngineData.Falso;
                btnB.Visible = EngineData.Falso;
                btnC.Visible = EngineData.Falso;
            }
            if (!LetrasJuegoACB.C) btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Look));
            else btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
        }

        private void InicializarACB()
        {
            btnA.Text = EngineData.Zero;
            btnB.Text = EngineData.Zero;
            btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Look));
            btnA.Visible = EngineData.Falso;
            btnB.Visible = EngineData.Falso;
            btnC.Visible = EngineData.Falso;
        }

        private void VisibilidadACB()
        {
            if (vInit)
            {
                btnA.Visible = EngineData.Falso;
                btnC.Visible = EngineData.Falso;
                btnB.Visible = EngineData.Falso;
            }
            else
            {
                if (contadorIngresado >= 17)
                {
                    btnA.Visible = EngineData.Verdadero;
                    btnC.Visible = EngineData.Verdadero;
                    btnB.Visible = EngineData.Verdadero;
                }
                else
                {
                    btnA.Visible = EngineData.Falso;
                    btnC.Visible = EngineData.Falso;
                    btnB.Visible = EngineData.Falso;
                }
            }
        }

        ////////////EVENTOS////////////////////////////////////////////////////////////////////////

        private void Lenguaje_Click(object sender, EventArgs e)
        {
            lblSudoku.Visible = EngineData.Falso;
            if (!lenguajeSi)
            {
                Funcion.SetearTextBoxLimpio(txtSudoku);
            }
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
            if (Valor.GetPathArchivo () == string.Empty)
            {
                ComportamientoObjExpandido();
                lenguajeSi = EngineData.Verdadero;
                mIdiomas.Visible = false;
            }
            else
            {
                string pathArchivo = Valor.GetPathArchivo();
                ArrayList arrText = Funcion.AbrirValoresArchivo(pathArchivo);
                valorIngresado = Funcion.SetValorIngresado(arrText, valorIngresado);
                valorEliminado = Funcion.SetValorEliminado(arrText, valorEliminado);
                valorInicio = Funcion.SetValorInicio(arrText, valorInicio);
                valorSolucion = Funcion.SetValorSolucion(arrText, valorSolucion);
                valorCandidato = Funcion.ElejiblesInstantaneos(valorSolucion, valorCandidato);
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorSolucion, valorCandidato, valorEliminado);

                Valor.SetValorIngresado(valorIngresado);
                Valor.SetValorEliminado(valorEliminado);
                Valor.SetValorInicio(valorInicio);
                Valor.SetValorSolucion(valorSolucion);
                Valor.SetNombreJuego(Funcion.NombreJuego(pathArchivo));
                lenguajeSi = EngineData.Verdadero;
                mIdiomas.Visible = false;
                Formularios.AzulUno F = new Formularios.AzulUno();
                F.Show();
                this.Hide();
            }
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

        private void BotonesJuego_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string pathArchivo = string.Empty;
            string nombreIdioma = Valor.GetNombreIdioma();
            switch (btn.Name)
            {
                case (EngineData.BtnAbrirJuego):
                    this.openFileDialog1.FileName = string.Empty;
                    this.openFileDialog1.Filter = Valor.NombreAbrirJuego(nombreIdioma);
                    this.openFileDialog1.Title = Valor.TextoAbrirJuego(nombreIdioma);
                    this.openFileDialog1.DefaultExt = EngineData.ExtensionFile;
                    this.openFileDialog1.ShowDialog();
                    pathArchivo = openFileDialog1.FileName;

                    if (pathArchivo == string.Empty) return;
                    Valor.SetPathArchivo(pathArchivo);
                    Valor.SetOpenFrom (openFrom);

                    ArrayList arrText = Funcion.AbrirValoresArchivo(pathArchivo);
                    valorIngresado = Funcion.SetValorIngresado(arrText, valorIngresado);
                    valorEliminado = Funcion.SetValorEliminado(arrText, valorEliminado);
                    valorInicio = Funcion.SetValorInicio(arrText, valorInicio);
                    valorSolucion = Funcion.SetValorSolucion(arrText, valorSolucion);
                    valorCandidato = Funcion.ElejiblesInstantaneos(valorSolucion, valorCandidato);
                    valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorSolucion, valorCandidato, valorEliminado);

                    Valor.SetValorIngresado(valorIngresado);
                    Valor.SetValorEliminado(valorEliminado );
                    Valor.SetValorInicio(valorInicio);
                    Valor.SetValorSolucion(valorSolucion);
                    Valor.SetNombreJuego(Funcion.NombreJuego(pathArchivo));

                    //Valor.SetOpenFrom(EngineData.File);

                    Formularios.AzulUno F = new Formularios.AzulUno();
                    F.Show();
                    this.Hide();
                    break;
                case (EngineData.BtnGuardarJuego):
                    if (Valor.GetPathArchivo() == string.Empty)
                    {
                        this.saveFileDialog1.FileName = string.Empty;
                        this.saveFileDialog1.Filter = Valor.NombreJuegoFileFiltro(nombreIdioma);
                        this.saveFileDialog1.Title = Valor.TituloGuardarJuego(nombreIdioma);
                        this.saveFileDialog1.DefaultExt = EngineData.ExtensionFile;
                        this.saveFileDialog1.ShowDialog();
                        pathArchivo = saveFileDialog1.FileName;

                        if (pathArchivo == string.Empty) return;
                        Valor.SetPathArchivo(pathArchivo);
                    }
                    else
                    {
                        pathArchivo = Valor.GetPathArchivo();
                        if (pathArchivo == string.Empty) return;
                    }
                    GuardarJuego(pathArchivo);

                    if (openFrom != EngineData.File)
                    {
                        btnOtro.Visible = EngineData.Verdadero;
                        btnAbrir.Visible = EngineData.Verdadero;
                        btnSolucion.Visible = EngineData.Falso;
                    }
                    btnGuardar.Visible = EngineData.Falso;
                    if (vInit == EngineData.Falso)
                    {
                        vInit = EngineData.Verdadero;
                    }
                    break;
                case (EngineData.BtnOtroJuego):
                    pathArchivo = Valor.GetPathArchivo();
                    if (pathArchivo == string.Empty)
                    {
                        MessageBox.Show(Valor.Mensaje1(Valor.GetIdioma()), Valor.TituloMensaje(Valor.GetIdioma()), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    GuardarJuego(pathArchivo);
                    pathArchivo = string.Empty;
                    Valor.SetPathArchivo(pathArchivo);

                    contadorIngresado = 0;
                    valorIngresado = new string[9, 9];
                    valorCandidato = new string[9, 9];
                    valorEliminado = new string[9, 9];
                    valorCandidatoSinEliminados = new string[9, 9];
                    valorInicio = new string[9, 9];
                    valorSolucion = new string[9, 9];

                    valorCandidato = Funcion.CandidatosJuego(valorSolucion, valorCandidato);
                    valorCandidatoSinEliminados = valorCandidato;
                    txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorSolucion, valorCandidato, valorInicio, Color.Green, Color.Blue);
                    txtSudoku = Funcion.SetearTextBoxLimpio(txtSudoku);

                    btnGuardar.Visible = EngineData.Falso;
                    btnOtro.Visible = EngineData.Falso;
                    btnAbrir.Visible = EngineData.Verdadero;
                    btnSolucion.Visible = EngineData.Falso;
                    openFrom = EngineData.Exe;
                    Valor.SetOpenFrom(openFrom);
                    vInit = EngineData.Falso;
                    InicializarACB();
                    SetLetrasJuegoFEG();
                    break;
                case (EngineData.BtnSolucion):
                    pathArchivo = Valor.GetPathArchivo();
                    if(pathArchivo==string.Empty)
                    {
                        MessageBox.Show(Valor.Mensaje1(Valor.GetNombreIdioma()), Valor.TituloMensaje(Valor.GetNombreIdioma()), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    GuardarJuego(pathArchivo);
                    pathArchivo = string.Empty;
                    Valor.SetPathArchivo(pathArchivo);

                    contadorIngresado = 0;
                    valorIngresado = new string[9, 9];
                    valorCandidato = new string[9, 9];
                    valorEliminado = new string[9, 9];
                    valorCandidatoSinEliminados = new string[9, 9];
                    valorInicio = new string[9, 9];
                    valorSolucion = new string[9, 9];

                    valorCandidato = Funcion.CandidatosJuego(valorSolucion, valorCandidato);
                    valorCandidatoSinEliminados = valorCandidato;
                    txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorSolucion, valorCandidato, valorInicio, Color.Green, Color.Blue);
                    txtSudoku = Funcion.SetearTextBoxLimpio(txtSudoku);

                    btnGuardar.Visible = EngineData.Falso;
                    btnOtro.Visible = EngineData.Falso;
                    btnAbrir.Visible = EngineData.Verdadero;
                    btnSolucion.Visible = EngineData.Falso;
                    openFrom = EngineData.Exe;
                    Valor.SetOpenFrom(openFrom);
                    vInit = EngineData.Falso;
                    InicializarACB();
                    SetLetrasJuegoFEG();
                    break;
            }
        }

        private void GuardarJuego(string pathArchivo )
        {
            string idioma = Valor.GetNombreIdioma();
            bool existeValor = Funcion.ExisteValorIngresado(valorSolucion);

            if (existeValor == EngineData.Falso)
            {
                MessageBox.Show(Valor.Mensaje2(Valor.GetNombreIdioma()), Valor.TituloMensaje(Valor.GetNombreIdioma()), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Funcion.ExiteArchivo(pathArchivo)){ Funcion.ReadWriteTxt(pathArchivo); }
            Funcion.GuardarValoresIngresados(pathArchivo, valorIngresado);
            Funcion.GuardarValoresEliminados(pathArchivo, valorEliminado);
            Funcion.GuardarValoresInicio(pathArchivo, valorInicio);
            Funcion.GuardarValoresSolucion(pathArchivo, valorSolucion);
            if (Funcion.ExiteArchivo(pathArchivo)) { Funcion.OnlyReadTxt(pathArchivo); }
        }

        //************************************************************************************

        private void txt00_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(3, 1));
            col = Int32.Parse(txt.Name.Substring(4, 1));

            if (!vInit)
            {
               txt.ForeColor = Color.Black;
            }
            else
            {
                if(valorInicio[row,col] != null && valorInicio[row,col]!= string.Empty)
                txt.ForeColor = Color.Black;
                else txt.ForeColor = Color.Blue;
            }
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
                if (txt.Text.Length > 0 ) { txt.Text = string.Empty; }
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
                    valorSolucion[row, col] = string.Empty;
                    if (vInit == EngineData.Falso) {valorInicio[row, col] = string.Empty;}
                }
                else
                {
                    valorSolucion[row, col] = txt.Text;
                    if (vInit == EngineData.Falso)
                    {
                        valorInicio[row, col] = txt.Text;
                    }

                    if (valorInicio[row, col] != null && valorInicio[row, col] != string.Empty)
                    {
                        txt.Text = valorInicio[row, col];
                    }
                }
                valorCandidato = Funcion.ElejiblesInstantaneos(valorSolucion, valorCandidato);
                txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2,valorSolucion,valorCandidato, valorInicio, Color.Green,Color.Blue);
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorSolucion, valorCandidato, valorEliminado);
                txtSudoku2 = Funcion.SetearTextBoxJuegoSinEliminados(txtSudoku2, valorCandidatoSinEliminados);
                ContadorIngresado();
            }
            catch {}

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

        //*************************************************************************************

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
        }

        private void t00_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            txt.BackColor = Color.WhiteSmoke;
        }

        private void t00_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));
            if (valorSolucion[row,col] != null && valorSolucion[row, col] != string.Empty)
            {
                txtSudoku2[row, col].Text = valorSolucion[row, col];
            }
            else
            {
                valorCandidato = Funcion.ElejiblesInstantaneos(valorSolucion, valorCandidato);
                txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorSolucion, valorCandidato, valorInicio, Color.Green, Color.Blue);
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorSolucion, valorCandidato, valorEliminado);
                txtSudoku2 = Funcion.SetearTextBoxJuegoSinEliminados(txtSudoku2, valorCandidatoSinEliminados);
                ContadorIngresado();
            }
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lenguajeSi)
            {
                if (Valor.GetSalirJuego() == false)
                {
                    Formularios.SalirIni F = new Formularios.SalirIni();
                    F.ShowDialog();
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
