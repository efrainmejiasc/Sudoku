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
    public partial class RojoDos : Form
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
        private string[,] valorSolucion = new string[9, 9];
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

        string filaRecuadroColumna = string.Empty;
        string circuito = string.Empty;
        private static AzulDos F = new AzulDos();
        private static RojoTres G = new RojoTres();
        private static RojoUno H = new RojoUno();

        private object obj2 = null;
        private EventArgs eve2 = null;


        public RojoDos()
        {
            InitializeComponent();
        }

        private void RojoDos_Activated(object sender, EventArgs e)
        {
            pnl1.Visible = false;
            pnl2.Visible = false;
            valorInicio = Valor.GetValorInicio();
            valorIngresado = Valor.GetValorIngresado();
            valorEliminado = Valor.GetValorEliminado();
            valorSolucion = Valor.GetValorSolucion();
            txtSudoku = Funcion.SetearTextBoxLimpio(txtSudoku);
            txtSudoku2 = Funcion.SetearTextBoxLimpio(txtSudoku2);
            AplicarIdioma();

            SetearJuego();
            ContadorIngresado();
            label1.Text = filaRecuadroColumna;
            if (obj2 != null)
            {
                if (circuito == "FILA")
                {
                    FilaEstado_Click(obj2, eve2);
                }
                else if (circuito == "COLUMNA")
                {
                    ColumnaEstado_Click(obj2, eve2);
                }
                else if (circuito == "RECUADRO")
                {
                    EstadoRecuadro_Click(obj2, eve2);
                }
            }
            timer1.Interval = 100;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            pnl1.Visible = true;
            pnl2.Visible = true;
        }

        private void RojoDos_Load(object sender, EventArgs e)
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
           // AplicarIdioma();
            ComportamientoObjetoInicio();
            QuitarCosete();
        }

        private void QuitarCosete()
        {
            tC1.GotFocus += delegate { HideCaret(tC1.Handle); };
            tC2.GotFocus += delegate { HideCaret(tC2.Handle); };
            tC3.GotFocus += delegate { HideCaret(tC3.Handle); };
            tC4.GotFocus += delegate { HideCaret(tC4.Handle); };
            tC5.GotFocus += delegate { HideCaret(tC5.Handle); };
            tC6.GotFocus += delegate { HideCaret(tC6.Handle); };
            tC7.GotFocus += delegate { HideCaret(tC7.Handle); };
            tC8.GotFocus += delegate { HideCaret(tC8.Handle); };
            tC9.GotFocus += delegate { HideCaret(tC9.Handle); };
            textBox10.GotFocus += delegate { HideCaret(textBox10.Handle); };
            textBox11.GotFocus += delegate { HideCaret(textBox11.Handle); };
            textBox12.GotFocus += delegate { HideCaret(textBox12.Handle); };
            textBox13.GotFocus += delegate { HideCaret(textBox13.Handle); };
            textBox14.GotFocus += delegate { HideCaret(textBox14.Handle); };
            textBox15.GotFocus += delegate { HideCaret(textBox15.Handle); };
            textBox16.GotFocus += delegate { HideCaret(textBox16.Handle); };
            textBox17.GotFocus += delegate { HideCaret(textBox17.Handle); };
            textBox18.GotFocus += delegate { HideCaret(textBox18.Handle); };
        }

        private void AplicarIdioma()
        {
            this.Text = Valor.TituloFormR3(Valor.GetNombreIdioma()) + Valor.GetNombreJuego();
            mIdiomas.Text = RecursosLocalizables.StringResources.mIdiomas;
            mContadores.Text = RecursosLocalizables.StringResources.mContadores;
            activar.Text = RecursosLocalizables.StringResources.activar;
            desactivar.Text = RecursosLocalizables.StringResources.desactivar;
            mFilas.Text = RecursosLocalizables.StringResources.mFilas;
            mColumnas.Text = RecursosLocalizables.StringResources.mColumnas;
            mRecuadros.Text = RecursosLocalizables.StringResources.mRecuadros;
            lblCelda.Text = RecursosLocalizables.StringResources.lblCelda;
            string etiqueta = Valor.EtiquetaRojoDos(Valor.GetNombreIdioma());
            string[] p = etiqueta.Split('/');
            label3.Text = p[0].ToUpper();
            label4.Text = p[1].ToUpper();
        }

        private void ComportamientoObjetoInicio()
        {
            pathArchivo = Valor.GetPathArchivo();
            if (pathArchivo == string.Empty)
            {
                MessageBox.Show("");
                return;
            }
            this.MaximumSize = new Size(1220 , 712);
            this.Size = new Size(1220 , 712);
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            btnPincel = AsociarBtnPincel(btnPincel);
            btnPincel = Funcion.ColoresPincel(btnPincel);
            ActivarDesactivarContadores(EngineData.Falso);
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

        private void SetearJuego()
        {
            valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
            valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
            txtSudoku2 = Funcion.SetearTextBoxEliminados(txtSudoku2, valorEliminado);
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

        private void btnAA_Click(object sender, EventArgs e)
        {
            Valor.SetValorIngresado(valorIngresado);
            Valor.SetValorInicio(valorInicio);
            Valor.SetValorEliminado(valorEliminado);
            F.Show();
            this.Hide();
        }

        private void activar_Click(object sender, EventArgs e)
        {
            ActivarDesactivarContadores(EngineData.Verdadero);
            ContadorIngresado();
        }

        private void desactivar_Click(object sender, EventArgs e)
        {
            ActivarDesactivarContadores(EngineData.Falso);
            ContadorIngresado();
        }

        private void FilaEstado_Click(object sender, EventArgs e)
        {
            string nombreFila = string.Empty;
            ToolStripMenuItem miPlantilla = new ToolStripMenuItem();
            miPlantilla = (ToolStripMenuItem)sender;
            int fila = -1;
            if (miPlantilla.Name == "fila1ToolStripMenuItem") { fila = 0; nombreFila = "Fila Nº 1"; txtNota.Text = "F1"; txtNota2.Text = "1"; }
            else if (miPlantilla.Name == "fila2ToolStripMenuItem") { fila = 1; nombreFila = "Fila Nº 2"; txtNota.Text = "F2"; txtNota2.Text = "2"; }
            else if (miPlantilla.Name == "fila3ToolStripMenuItem") { fila = 2; nombreFila = "Fila Nº 3"; txtNota.Text = "F3"; txtNota2.Text = "3"; }
            else if (miPlantilla.Name == "fila4ToolStripMenuItem") { fila = 3; nombreFila = "Fila Nº 4"; txtNota.Text = "F4"; txtNota2.Text = "4"; }
            else if (miPlantilla.Name == "fila5ToolStripMenuItem") { fila = 4; nombreFila = "Fila Nº 5"; txtNota.Text = "F5"; txtNota2.Text = "5"; }
            else if (miPlantilla.Name == "fila6ToolStripMenuItem") { fila = 5; nombreFila = "Fila Nº 6"; txtNota.Text = "F6"; txtNota2.Text = "6"; }
            else if (miPlantilla.Name == "fila7ToolStripMenuItem") { fila = 6; nombreFila = "Fila Nº 7"; txtNota.Text = "F7"; txtNota2.Text = "7"; }
            else if (miPlantilla.Name == "fila8ToolStripMenuItem") { fila = 7; nombreFila = "Fila Nº 8"; txtNota.Text = "F8"; txtNota2.Text = "8"; }
            else if (miPlantilla.Name == "fila9ToolStripMenuItem") { fila = 8; nombreFila = "Fila Nº 9"; txtNota.Text = "F9"; txtNota2.Text = "9"; }


            string[,] valorPlantilla = new string[9, 9];
            bool existeValor = Funcion.ExisteValorIngresado(valorIngresado);
            if (!existeValor)
            {
                valorPlantilla = Funcion.InicioPlantillaVacio(valorPlantilla);
                txtSudoku = Funcion.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            else
            {
                valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                //valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                valorPlantilla = Funcion.ObtenerSetearValoresFila(valorIngresado, valorCandidato, valorEliminado, fila);
                txtSudoku = Funcion.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            string[] f = new string[9];
            f = Valor.GetNumeroFila(fila);
            tC1.Text = f[0];
            tC2.Text = f[1];
            tC3.Text = f[2];
            tC4.Text = f[3];
            tC5.Text = f[4];
            tC6.Text = f[5];
            tC7.Text = f[6];
            tC8.Text = f[7];
            tC9.Text = f[8];
            txt00.BackColor = Color.WhiteSmoke;
        }

        private void ColumnaEstado_Click(object sender, EventArgs e)
        {
            String nombreColumna = string.Empty;
            ToolStripMenuItem miPlantilla = new ToolStripMenuItem();
            miPlantilla = (ToolStripMenuItem)sender;
            int columna = -1;
            if (miPlantilla.Name == "columna1ToolStripMenuItem") { columna = 0; nombreColumna = "Columna Nº 1"; txtNota.Text = "C1"; txtNota2.Text = "1"; }
            else if (miPlantilla.Name == "columna2ToolStripMenuItem") { columna = 1; nombreColumna = "Columna Nº 2"; txtNota.Text = "C2"; txtNota2.Text = "2"; }
            else if (miPlantilla.Name == "columna3ToolStripMenuItem") { columna = 2; nombreColumna = "Columna Nº 3"; txtNota.Text = "C3"; txtNota2.Text = "3"; }
            else if (miPlantilla.Name == "columna4ToolStripMenuItem") { columna = 3; nombreColumna = "Columna Nº 4"; txtNota.Text = "C4"; txtNota2.Text = "4"; }
            else if (miPlantilla.Name == "columna5ToolStripMenuItem") { columna = 4; nombreColumna = "Columna Nº 5"; txtNota.Text = "C5"; txtNota2.Text = "5"; }
            else if (miPlantilla.Name == "columna6ToolStripMenuItem") { columna = 5; nombreColumna = "Columna Nº 6"; txtNota.Text = "C6"; txtNota2.Text = "6"; }
            else if (miPlantilla.Name == "columna7ToolStripMenuItem") { columna = 6; nombreColumna = "Columna Nº 7"; txtNota.Text = "C7"; txtNota2.Text = "7"; }
            else if (miPlantilla.Name == "columna8ToolStripMenuItem") { columna = 7; nombreColumna = "Columna Nº 8"; txtNota.Text = "C8"; txtNota2.Text = "8"; }
            else if (miPlantilla.Name == "columna9ToolStripMenuItem") { columna = 8; nombreColumna = "Columna Nº 9"; txtNota.Text = "C9"; txtNota2.Text = "9"; }

            string[,] valorPlantilla = new string[9, 9];
            bool existeValor = Funcion.ExisteValorIngresado(valorIngresado);
            if (!existeValor)
            {
                valorPlantilla = Funcion.InicioPlantillaVacio(valorPlantilla);
                txtSudoku = Funcion.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            else
            {
                valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                //valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                valorPlantilla = Funcion.ObtenerSetearValoresColumna(valorIngresado, valorCandidato,valorEliminado, columna);
                txtSudoku = Funcion.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }

            string[] c = new string[9];
            c = Valor.GetNumeroColumna(columna);
            tC1.Text = c[0];
            tC2.Text = c[1];
            tC3.Text = c[2];
            tC4.Text = c[3];
            tC5.Text = c[4];
            tC6.Text = c[5];
            tC7.Text = c[6];
            tC8.Text = c[7];
            tC9.Text = c[8];
        }

        private void EstadoRecuadro_Click(object sender, EventArgs e)
        {
            String nombreRecuadro = string.Empty ;
            ToolStripMenuItem miPlantilla = new ToolStripMenuItem();
            miPlantilla = (ToolStripMenuItem)sender;
            int rangoF = -1, rangoC = -1, rec = -1;
            if (miPlantilla.Name == "recuadro1ToolStripMenuItem") { rangoF = 0; rangoC = 0; nombreRecuadro = "Recuadro Nº 1";  rec = 0; txtNota.Text = "R1"; txtNota2.Text = "1"; }
            else if (miPlantilla.Name == "recuadro2ToolStripMenuItem") { rangoF = 0; rangoC = 3; nombreRecuadro = "Recuadro Nº 2"; rec = 1; txtNota.Text = "R2"; txtNota2.Text = "2"; }
            else if (miPlantilla.Name == "recuadro3ToolStripMenuItem") { rangoF = 0; rangoC = 6; nombreRecuadro = "Recuadro Nº 3"; rec = 2; txtNota.Text = "R3"; txtNota2.Text = "3"; }
            else if (miPlantilla.Name == "recuadro4ToolStripMenuItem") { rangoF = 3; rangoC = 0; nombreRecuadro = "Recuadro Nº 4"; rec = 3; txtNota.Text = "R4"; txtNota2.Text = "4"; }
            else if (miPlantilla.Name == "recuadro5ToolStripMenuItem") { rangoF = 3; rangoC = 3; nombreRecuadro = "Recuadro Nº 5"; rec = 4; txtNota.Text = "R5"; txtNota2.Text = "5"; }
            else if (miPlantilla.Name == "recuadro6ToolStripMenuItem") { rangoF = 3; rangoC = 6; nombreRecuadro = "Recuadro Nº 6"; rec = 5; txtNota.Text = "R6"; txtNota2.Text = "6"; }
            else if (miPlantilla.Name == "recuadro7ToolStripMenuItem") { rangoF = 6; rangoC = 0; nombreRecuadro = "Recuadro Nº 7"; rec = 6; txtNota.Text = "R7"; txtNota2.Text = "7"; }
            else if (miPlantilla.Name == "recuadro8ToolStripMenuItem") { rangoF = 6; rangoC = 3; nombreRecuadro = "Recuadro Nº 8"; rec = 7; txtNota.Text = "R8"; txtNota2.Text = "8"; }
            else if (miPlantilla.Name == "recuadro9ToolStripMenuItem") { rangoF = 6; rangoC = 6; nombreRecuadro = "Recuadro Nº 9"; rec = 8; txtNota.Text = "R9"; txtNota2.Text = "9"; }

            string[,] valorPlantilla = new string[9, 9];
            bool existeValor = Funcion.ExisteValorIngresado(valorIngresado);
            if (!existeValor)
            {
                valorPlantilla = Funcion.InicioPlantillaVacio(valorPlantilla);
                txtSudoku = Funcion.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            else
            {
                valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                //valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                valorPlantilla = Funcion.ObtenerSetearValoresRecuadro(valorIngresado, valorCandidato, valorEliminado,rangoF, rangoC);
                txtSudoku = Funcion.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }

            string[] r = new string[9];
            r = Valor.GetNumeroRecuadro(rec);
            tC1.Text = r[0];
            tC2.Text = r[1];
            tC3.Text = r[2];
            tC4.Text = r[3];
            tC5.Text = r[4];
            tC6.Text = r[5];
            tC7.Text = r[6];
            tC8.Text = r[7];
            tC9.Text = r[8];
        }

        private void EliminarRestablecerCandidato_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case (EngineData.eliminar):
                    if (lado != EngineData.btnIzquierda) return;
                    if (txtNota.Text == string.Empty) return;
                    string candidatoEliminar = txtSudoku[row, col].Text;
                    if (candidatoEliminar == string.Empty) return;
                    int[] fc = new int[2];
                    fc = ProcesoEliminar();
                    valorEliminado[fc[0],fc[1]] = valorEliminado[fc[0], fc[1]] + " " + candidatoEliminar;
                    valorEliminado[fc[0], fc[1]] = Funcion.OrdenarCadena(valorEliminado[fc[0], fc[1]]);
                    txtSudoku[row, col].BackColor = Color.WhiteSmoke;
                    txtSudoku[row, col].Text = string.Empty;
                    SetearJuego();
                    GrupoActualizar();
                    break;
                case (EngineData.restablecer):
                    if (lado != EngineData.btnDerecha) return;
                    string candidatoRestablecer = txtSudoku2[row, col].Text.Trim();
                    if (candidatoRestablecer == string.Empty) return;
                    if (candidatoRestablecer.Length == 1)
                    {
                        valorEliminado[row, col] = valorEliminado[row, col].Replace(candidatoRestablecer, "");
                    }
                    else if (candidatoRestablecer.Length > 1)
                    {
                        return;
                    }
                    txtSudoku2[row, col].Text = valorEliminado[row, col];
                    SetearJuego();
                    GrupoActualizar();
                    break;
            }
            ContadorIngresado();
            GuardarJuego(pathArchivo);
        }

        private void GuardarJuego(string pathArchivo)
        {
            if (pathArchivo == string.Empty || pathArchivo == null) return;
            if (Funcion.ExiteArchivo(pathArchivo)) { Funcion.ReadWriteTxt(pathArchivo); }
            Funcion.GuardarValoresIngresados(pathArchivo, valorIngresado);
            Funcion.GuardarValoresEliminados(pathArchivo, valorEliminado);
            Funcion.GuardarValoresInicio(pathArchivo, valorInicio);
            Funcion.GuardarValoresSolucion(pathArchivo, valorSolucion);
            if (Funcion.ExiteArchivo(pathArchivo)) { Funcion.OnlyReadTxt(pathArchivo); }
        }

        public int [] ProcesoEliminar ()
        {
            int[] fc = new int [2];
            string celda = string.Empty;
            if (row == 0) { celda = tC1.Text; }
            else if (row == 1) { celda = tC2.Text; }
            else if (row == 2) { celda = tC3.Text; }
            else if (row == 3) { celda = tC4.Text; }
            else if (row == 4) { celda = tC5.Text; }
            else if (row == 5) { celda = tC6.Text; }
            else if (row == 6) { celda = tC7.Text; }
            else if (row == 7) { celda = tC8.Text; }
            else if (row == 8) { celda = tC9.Text; }
            fc[0] = Convert.ToInt16(celda.Trim().Substring(0, 1)) - 1;
            fc[1] = Convert.ToInt16(celda.Trim().Substring(1, 1)) - 1;
            return fc;
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

        private void fILASToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            circuito = "FILA";
            filaRecuadroColumna = Valor.FilaColumnaRecuadro(Valor.GetNombreIdioma(), circuito);
            txtNota.Text = string.Empty;
        }

        private void cOLUMNASToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            circuito = "COLUMNA";
            filaRecuadroColumna = Valor.FilaColumnaRecuadro(Valor.GetNombreIdioma(), circuito);
            txtNota.Text = string.Empty;
        }

        private void rECUADROToolStripMenuItem_Click(object sender, EventArgs e)
        {
            circuito = "RECUADRO";
            filaRecuadroColumna = Valor.FilaColumnaRecuadro(Valor.GetNombreIdioma(), circuito);
            txtNota.Text = string.Empty;
        }

        private void btnGrup_Click(object sender, EventArgs e)
        {
            string cadena = txtNota.Text;
            object obj = null;
            EventArgs eve = null;

            if (circuito == "FILA")
            {
                if (cadena == string.Empty || cadena.Contains ("C") || cadena.Contains("R"))
                {
                    cadena = "F1";
                    txtNota.Text = cadena;
                    obj = fila1ToolStripMenuItem;
                }
                else
                {
                    switch (cadena)
                    {
                        case ("F1"):
                            txtNota.Text = "F2";
                            obj = fila2ToolStripMenuItem;
                            break;
                        case ("F2"):
                            txtNota.Text = "F3";
                            obj = fila3ToolStripMenuItem;
                            break;
                        case ("F3"):
                            txtNota.Text = "F4";
                            obj = fila4ToolStripMenuItem;
                            break;
                        case ("F4"):
                            txtNota.Text = "F5";
                            obj = fila5ToolStripMenuItem;
                            break;
                        case ("F5"):
                            txtNota.Text = "F6";
                            obj = fila6ToolStripMenuItem;
                            break;
                        case ("F6"):
                            txtNota.Text = "F7";
                            obj = fila7ToolStripMenuItem;
                            break;
                        case ("F7"):
                            txtNota.Text = "F8";
                            obj = fila8ToolStripMenuItem;
                            break;
                        case ("F8"):
                            txtNota.Text = "F9";
                            obj = fila9ToolStripMenuItem;
                            break;
                        case ("F9"):
                            txtNota.Text = "F1";
                            obj = fila1ToolStripMenuItem;
                            break;
                    }
                }
                FilaEstado_Click(obj, eve);
            }
            else if (circuito == "COLUMNA")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("R"))
                {
                    cadena = "C1";
                    txtNota.Text = cadena;
                    obj = columna1ToolStripMenuItem;
                }
                else
                {
                    switch (cadena)
                    {
                        case ("C1"):
                            txtNota.Text = "C2";
                            obj = columna2ToolStripMenuItem;
                            break;
                        case ("C2"):
                            txtNota.Text = "C3";
                            obj = columna3ToolStripMenuItem;
                            break;
                        case ("C3"):
                            txtNota.Text = "C4";
                            obj = columna4ToolStripMenuItem;
                            break;
                        case ("C4"):
                            txtNota.Text = "C5";
                            obj = columna5ToolStripMenuItem;
                            break;
                        case ("C5"):
                            txtNota.Text = "C6";
                            obj = columna6ToolStripMenuItem;
                            break;
                        case ("C6"):
                            txtNota.Text = "C7";
                            obj = columna7ToolStripMenuItem;
                            break;
                        case ("C7"):
                            txtNota.Text = "C8";
                            obj = columna8ToolStripMenuItem;
                            break;
                        case ("C8"):
                            txtNota.Text = "C9";
                            obj = columna9ToolStripMenuItem;
                            break;
                        case ("C9"):
                            txtNota.Text = "C1";
                            obj = columna1ToolStripMenuItem;
                            break;
                    }
                }
                ColumnaEstado_Click(obj, eve);
            }
            else if (circuito == "RECUADRO")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("C"))
                {
                    cadena = "R1";
                    txtNota.Text = cadena;
                    obj = recuadro1ToolStripMenuItem;
                }
                else
                {
                    switch (cadena)
                    {
                        case ("R1"):
                            txtNota.Text = "R2";
                            obj = recuadro2ToolStripMenuItem;
                            break;
                        case ("R2"):
                            txtNota.Text = "R3";
                            obj = recuadro3ToolStripMenuItem;
                            break;
                        case ("R3"):
                            txtNota.Text = "R4";
                            obj = recuadro4ToolStripMenuItem;
                            break;
                        case ("R4"):
                            txtNota.Text = "R5";
                            obj = recuadro5ToolStripMenuItem;
                            break;
                        case ("R5"):
                            txtNota.Text = "R6";
                            obj = recuadro6ToolStripMenuItem;
                            break;
                        case ("R6"):
                            txtNota.Text = "R7";
                            obj = recuadro7ToolStripMenuItem;
                            break;
                        case ("R7"):
                            txtNota.Text = "R8";
                            obj = recuadro8ToolStripMenuItem;
                            break;
                        case ("R8"):
                            txtNota.Text = "R9";
                            obj = recuadro9ToolStripMenuItem;
                            break;
                        case ("R9"):
                            txtNota.Text = "R1";
                            obj = recuadro1ToolStripMenuItem;
                            break;
                    }
                }
                EstadoRecuadro_Click(obj, eve);
            }
            else
            {
                MessageBox.Show(Valor.Mensaje22(Valor.GetNombreIdioma()), Valor.TituloMensaje(Valor.GetNombreIdioma()), MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            obj2 = obj;
            label1.Text = filaRecuadroColumna ;
        }

        private void btnGrup2_Click(object sender, EventArgs e)
        {
            string cadena = txtNota.Text;
            object obj = null;
            EventArgs eve = null;

            if (circuito == "FILA")
            {
                if (cadena == string.Empty || cadena.Contains("C") || cadena.Contains("R"))
                {
                    cadena = "F9";
                    txtNota.Text = cadena;
                    obj = fila9ToolStripMenuItem;
                }
                else
                {
                    switch (cadena)
                    {
                        case ("F9"):
                            txtNota.Text = "F8";
                            obj = fila8ToolStripMenuItem;
                            break;
                        case ("F8"):
                            txtNota.Text = "F7";
                            obj = fila7ToolStripMenuItem;
                            break;
                        case ("F7"):
                            txtNota.Text = "F6";
                            obj = fila6ToolStripMenuItem;
                            break;
                        case ("F6"):
                            txtNota.Text = "F5";
                            obj = fila5ToolStripMenuItem;
                            break;
                        case ("F5"):
                            txtNota.Text = "F4";
                            obj = fila4ToolStripMenuItem;
                            break;
                        case ("F4"):
                            txtNota.Text = "F3";
                            obj = fila3ToolStripMenuItem;
                            break;
                        case ("F3"):
                            txtNota.Text = "F2";
                            obj = fila2ToolStripMenuItem;
                            break;
                        case ("F2"):
                            txtNota.Text = "F1";
                            obj = fila1ToolStripMenuItem;
                            break;
                        case ("F1"):
                            txtNota.Text = "F9";
                            obj = fila9ToolStripMenuItem;
                            break;
                    }
                }
                FilaEstado_Click(obj, eve);
            }
            else if (circuito == "COLUMNA")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("R"))
                {
                    cadena = "C9";
                    txtNota.Text = cadena;
                    obj = columna9ToolStripMenuItem;
                }
                else
                {
                    switch (cadena)
                    {
                        case ("C9"):
                            txtNota.Text = "C8";
                            obj = columna8ToolStripMenuItem;
                            break;
                        case ("C8"):
                            txtNota.Text = "C7";
                            obj = columna7ToolStripMenuItem;
                            break;
                        case ("C7"):
                            txtNota.Text = "C6";
                            obj = columna6ToolStripMenuItem;
                            break;
                        case ("C6"):
                            txtNota.Text = "C5";
                            obj = columna5ToolStripMenuItem;
                            break;
                        case ("C5"):
                            txtNota.Text = "C4";
                            obj = columna4ToolStripMenuItem;
                            break;
                        case ("C4"):
                            txtNota.Text = "C3";
                            obj = columna3ToolStripMenuItem;
                            break;
                        case ("C3"):
                            txtNota.Text = "C2";
                            obj = columna2ToolStripMenuItem;
                            break;
                        case ("C2"):
                            txtNota.Text = "C1";
                            obj = columna1ToolStripMenuItem;
                            break;
                        case ("C1"):
                            txtNota.Text = "C9";
                            obj = columna9ToolStripMenuItem;
                            break;
                    }
                }
                ColumnaEstado_Click(obj, eve);
            }
            else if (circuito == "RECUADRO")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("C"))
                {
                    cadena = "R9";
                    txtNota.Text = cadena;
                    obj = recuadro9ToolStripMenuItem;
                }
                else
                {
                    switch (cadena)
                    {
                        case ("R9"):
                            txtNota.Text = "R8";
                            obj = recuadro8ToolStripMenuItem;
                            break;
                        case ("R8"):
                            txtNota.Text = "R7";
                            obj = recuadro7ToolStripMenuItem;
                            break;
                        case ("R7"):
                            txtNota.Text = "R6";
                            obj = recuadro6ToolStripMenuItem;
                            break;
                        case ("R6"):
                            txtNota.Text = "R5";
                            obj = recuadro5ToolStripMenuItem;
                            break;
                        case ("R5"):
                            txtNota.Text = "R4";
                            obj = recuadro4ToolStripMenuItem;
                            break;
                        case ("R4"):
                            txtNota.Text = "R3";
                            obj = recuadro3ToolStripMenuItem;
                            break;
                        case ("R3"):
                            txtNota.Text = "R2";
                            obj = recuadro2ToolStripMenuItem;
                            break;
                        case ("R2"):
                            txtNota.Text = "R1";
                            obj = recuadro1ToolStripMenuItem;
                            break;
                        case ("R1"):
                            txtNota.Text = "R9";
                            obj = recuadro9ToolStripMenuItem;
                            break;
                    }
                }
                EstadoRecuadro_Click(obj, eve);
            }
            else
            {
                MessageBox.Show(Valor.Mensaje22(Valor.GetNombreIdioma()), Valor.TituloMensaje(Valor.GetNombreIdioma()), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            obj2 = obj;
            label1.Text = filaRecuadroColumna;
        }

        private void GrupoActualizar()
        {
            string cadena = txtNota.Text;
            object obj = null;
            EventArgs eve = null;
            switch (cadena)
            {
                case ("F1"):
                    obj = fila1ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F2"):
                    obj = fila2ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F3"):
                    obj = fila3ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F4"):
                    obj = fila4ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F5"):
                    obj = fila5ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F6"):
                    obj = fila6ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F7"):
                    obj = fila7ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F8"):
                    obj = fila8ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F9"):
                    obj = fila9ToolStripMenuItem;
                    FilaEstado_Click(obj, eve);
                    break;
                case ("C1"):
                    obj = columna1ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C2"):
                    obj = columna2ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C3"):
                    obj = columna3ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C4"):
                    obj = columna4ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C5"):
                    obj = columna5ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C6"):
                    obj = columna6ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C7"):
                    obj = columna7ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C8"):
                    obj = columna8ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C9"):
                    obj = columna9ToolStripMenuItem;
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("R1"):
                    obj = recuadro1ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R2"):
                    obj = recuadro2ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R3"):
                    obj = recuadro3ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R4"):
                    obj = recuadro4ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R5"):
                    obj = recuadro5ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R6"):
                    obj = recuadro6ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R7"):
                    obj = recuadro7ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R8"):
                    obj = recuadro8ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R9"):
                    obj = recuadro9ToolStripMenuItem;
                    EstadoRecuadro_Click(obj, eve);
                    break;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Valor.SetValorIngresado(valorIngresado);
            Valor.SetValorInicio(valorInicio);
            Valor.SetValorEliminado(valorEliminado);
            txtNota2.Text = string.Empty;
            circuito = string.Empty;
            filaRecuadroColumna = string.Empty;
            G.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Valor.SetValorIngresado(valorIngresado);
            Valor.SetValorInicio(valorInicio);
            Valor.SetValorEliminado(valorEliminado);
            txtNota2.Text = string.Empty;
            circuito = string.Empty;
            filaRecuadroColumna = string.Empty;
            H.Show();
            this.Hide();
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

            GrupoActualizar();

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

        //************************************************************************************************

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
            txt.Text = string.Empty;
        }

        private void t00_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Select(0, 0);
            row = Int32.Parse(txt.Name.Substring(1, 1));
            col = Int32.Parse(txt.Name.Substring(2, 1));

            if (txt.Text == EngineData.Zero || txt.Text == string.Empty)
            {
                txt.Text = valorEliminado[row, col];
            }
            else
            {
                if (!valorEliminado[row, col].Contains(txt.Text)) { txt.Text = valorEliminado[row, col]; }
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

        private void RojoDos_FormClosing(object sender, FormClosingEventArgs e)
        {
            Valor.SetSalirJuego(true);
            Application.Exit();
        }

        //***********************************************************************************************
    }
}
