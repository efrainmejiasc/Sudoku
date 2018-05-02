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

namespace SudokuParaTodos
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        //***********************************************************************************************************
        private EngineSudoku Funcion = new EngineSudoku();
        private EngineData Valor = EngineData.Instance();
        private TextBox[,] txtSudoku = new TextBox[9,9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DEL SUDOKU
        private TextBox[,] txtSudoku2 = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DE CANDIDATOS
        private Button[] btnPincel = new Button[7];// ARRAY CONTENTIVO DE LOS BOTONES DE PINCELES IZQUIERDO
        private Button[] btnPincel2 = new Button[5];// ARRAY CONTENTIVO DE LOS BOTONES DE PINCELES DERECHO
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorCandidatoSinEliminados = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];
        private int[] position = new int[2];
        private int row = -1;
        private int col = -1;
        private int contadorIngresado = -1;
        private string openFrom = string.Empty;
        private bool juegoGuardado;
        private bool crearOtro;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = EngineData.Titulo;
            if (!Funcion.ExisteClaveRegWin()){Funcion.AgregarClaveRegWin();}
            Funcion.AsociarExtension();
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            btnPincel = AsociarBtnPincel(btnPincel);
            btnPincel2 = AsociarBtnPincel2(btnPincel2);
            ComportamientoObjInicio();
            openFrom = Valor.GetOpenFrom();
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
                item.GotFocus += delegate { HideCaret(item.Handle); };
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
                item.GotFocus += delegate { HideCaret(item.Handle); };
            return txtSudoku2;
        }

        private Button[] AsociarBtnPincel(Button [] btnPincel)
        {
            btnPincel[0] = pincelA; btnPincel[1] = pincelB;
            btnPincel[2] = pincelC; btnPincel[3] = pincelD;
            btnPincel[4] = pincelE; btnPincel[5] = pincelK;
            btnPincel[6] = pincelL;
            return btnPincel;
        }

        private Button[] AsociarBtnPincel2(Button[] btnPincel2)
        {
            btnPincel2[0] = pincelF; btnPincel2[1] = pincelG;
            btnPincel2[2] = pincelH; btnPincel2[3] = pincelI;
            btnPincel2[4] = pincelJ;

            return btnPincel2;
        }

        private void ComportamientoObjInicio()
        {
            this.Size = new Size(592 , 682);
            btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
            mArchivo.Visible = EngineData.Falso;
            mTablero.Visible = EngineData.Falso;
            mColores.Visible = EngineData.Falso;
            mContadores.Visible = EngineData.Falso;
            txtNota.Visible = EngineData.Falso;
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
            btn1.Visible = EngineData.Falso;
            btn2.Visible = EngineData.Falso;
            btn3.Visible = EngineData.Falso;
            btn4.Visible = EngineData.Falso;
            btn5.Visible = EngineData.Falso;
            btn6.Visible = EngineData.Falso;
            btn7.Visible = EngineData.Falso;
            btn8.Visible = EngineData.Falso;
            btn9.Visible = EngineData.Falso;
            lbl1.Visible = EngineData.Falso;
            lbl2.Visible = EngineData.Falso;
            lbl3.Visible = EngineData.Falso;
            lbl4.Visible = EngineData.Falso;
            lbl5.Visible = EngineData.Falso;
            lbl6.Visible = EngineData.Falso;
            lbl7.Visible = EngineData.Falso;
            lbl8.Visible = EngineData.Falso;
            lbl9.Visible = EngineData.Falso;
            btnR.Visible = EngineData.Falso;
            btnEL.Visible = EngineData.Falso;
            btnAA.Visible = EngineData.Falso;
            btnBB.Visible = EngineData.Falso;
            pincelK.Visible = EngineData.Falso;
            pincelL.Visible = EngineData.Falso;
            lblIndice.Visible = EngineData.Falso;
            txtIndice.Visible = EngineData.Falso;
            btnIzq1.Visible = EngineData.Falso;
            btnIzq2.Visible = EngineData.Falso;
            btnDer1.Visible = EngineData.Falso;
            btnDer2.Visible = EngineData.Falso;
            //juegoGuardado = EngineData.Falso;
            btnDelete.Visible = EngineData.Falso;
            btnTres.Visible = EngineData.Falso;
            btnDos.Visible = EngineData.Falso;

            //crearOtro = EngineData.Falso;
            valorCandidato = Funcion.CandidatosJuego(valorIngresado, valorCandidato);
            txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorIngresado, valorCandidato, Color.Green, Color.Blue);
            string idioma = CultureInfo.InstalledUICulture.NativeName;
            if (idioma.Contains(EngineData.english)) mIdiomas.Text = EngineData.LANGUAGES;
            else if (idioma.Contains(EngineData.english)) mIdiomas.Text = EngineData.LANGUAGES;
            else mIdiomas.Text = EngineData.IDIOMAS;
        }

        private void ComportamientoObjExpandido()
        {
            if (Valor.GetIdioma() != string.Empty)
            {
                this.Size = new Size(1168, 682);
                foreach (Button btn in btnPincel) { btn.Visible = EngineData.Verdadero; }
                btnPincel = Funcion.ColoresPincel(btnPincel);
                btnPincel2 = Funcion.ColoresPincel2(btnPincel2);
                pnlJuego.Visible = EngineData.Verdadero;
                btnAbrir.Visible = EngineData.Verdadero;
                btnOtro.Visible = EngineData.Falso;
                btnGuardar.Visible = EngineData.Falso;
                btnSolucion.Visible = EngineData.Falso;
                txtNota.Visible = EngineData.Falso;
                pnlLetra.Visible = EngineData.Verdadero;
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
            this.Text = Valor.TituloForm(Valor.GetIdioma()); 
        }

        private int  ContadorIngresado()
        {
            contadorIngresado = Funcion.ContadorIngresado(valorIngresado);
            if (contadorIngresado >= 1 && juegoGuardado == EngineData.Falso)
            {
                btnGuardar.Visible = EngineData.Verdadero;
                btnA.Visible = EngineData.Verdadero;
                btnB.Visible = EngineData.Verdadero;
                btnC.Visible = EngineData.Verdadero;
            }
            else if (contadorIngresado < 1)
            {
                btnGuardar.Visible = EngineData.Falso;
            }
            if (contadorIngresado == 81)
            {
                btnSolucion.Visible = EngineData.Verdadero;
            }
            else if (contadorIngresado < 81)
            {
                btnSolucion.Visible = EngineData.Falso;
            }

            return contadorIngresado;
        }

        ////////////EVENTOS////////////////////////////////////////////////////////////////////////

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
            ComportamientoObjExpandido();

        }

        private void BotonesJuego_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string pathArchivo = string.Empty;
            string idioma = Valor.GetIdioma();
            switch (btn.Name)
            {
                case (EngineData.BtnGuardarJuego):
                    this.saveFileDialog1.FileName = string.Empty;
                    this.saveFileDialog1.Filter = Valor.NombreJuegoFileFiltro(idioma);
                    this.saveFileDialog1.Title = Valor.TituloGuardarJuego(idioma);
                    this.saveFileDialog1.DefaultExt = EngineData.ExtensionFile;
                    this.saveFileDialog1.ShowDialog();
                    pathArchivo = saveFileDialog1.FileName;

                    if (pathArchivo == string.Empty) return;

                    bool existeValor = Funcion.ExisteValorIngresado(valorInicio);

                    if (existeValor == EngineData.Falso)
                    {
                        MessageBox.Show("DEBE INGRESAR VALORES DE INICIO DEL JUEGO", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    Funcion.GuardarValoresIngresados(pathArchivo, valorIngresado);
                    Funcion.GuardarValoresEliminados(pathArchivo, valorEliminado);
                    Funcion.GuardarValoresInicio(pathArchivo, valorInicio);
                    Funcion.GuardarValoresSolucion(pathArchivo, valorInicio);
                    valorIngresado = Funcion.LimpiarArreglo(valorIngresado);
                    valorEliminado = Funcion.LimpiarArreglo(valorEliminado);

                    openFrom = EngineData.File;
                    juegoGuardado = EngineData.Verdadero;
                    break;
                case (EngineData.BtnAbrirJuego):
                    this.openFileDialog1.FileName = string.Empty;
                    this.openFileDialog1.Filter = Valor.NombreAbrirJuego(idioma);
                    this.openFileDialog1.Title = Valor.TextoAbrirJuego(idioma);
                    this.openFileDialog1.DefaultExt = EngineData.ExtensionFile;
                    this.openFileDialog1.ShowDialog();
                    pathArchivo = openFileDialog1.FileName;

                    if (pathArchivo == string.Empty) return;

                    Funcion.ReadWriteTxt(pathArchivo);
                    Valor.SetPathArchivo(pathArchivo);
                    ArrayList arrText = Funcion.AbrirValoresArchivo(pathArchivo);

                    valorIngresado = Funcion.SetValorIngresado(arrText, valorIngresado);
                    valorEliminado = Funcion.SetValorEliminado(arrText, valorEliminado);
                    valorInicio = Funcion.SetValorInicio(arrText, valorInicio);
                    valorSolucion = Funcion.SetValorSolucion(arrText, valorSolucion);

                    txtSudoku = Funcion.SetearTextBoxLimpio(txtSudoku);
                    valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                    valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                    txtSudoku = Funcion.SetearTextBoxJuego(txtSudoku, valorIngresado, valorCandidato, colorA: Color.Blue, colorB: Color.Blue, lado: EngineData.Left);
                    txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2, valorIngresado, valorCandidato, Color.Green, Color.Blue);
                    txtSudoku2 = Funcion.SetearTextBoxJuegoSinEliminados(txtSudoku2, valorCandidatoSinEliminados);
                    break;
                case (EngineData.BtnOtroJuego):
                    //crearOtro = EngineData.Verdadero;
                    MessageBox.Show("Crear");
                    break;
                case (EngineData.BtnSolucion):
                    MessageBox.Show("Solucion");
                    break;
            }
        }

        private void BotonesContadores_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case (EngineData.Btn1):
                    break;
                case (EngineData.Btn2):
                    break;
                case (EngineData.Btn3):
                    break;
                case (EngineData.Btn4):
                    break;
                case (EngineData.Btn5):
                    break;
                case (EngineData.Btn6):
                    break;
                case (EngineData.Btn7):
                    break;
                case (EngineData.Btn8):
                    break;
                case (EngineData.Btn9):
                    break;
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
                    valorIngresado[row, col] = string.Empty;
                    if (openFrom == EngineData.Exe) {valorInicio[row, col] = string.Empty;}
                }
                else
                {
                    valorIngresado[row, col] = txt.Text;
                    if (openFrom == EngineData.Exe) {valorInicio[row, col] = string.Empty;}
                }
                valorCandidato = Funcion.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                txtSudoku2 = Funcion.SetearTextBoxJuego(txtSudoku2,valorIngresado,valorCandidato,Color.Green,Color.Blue);
                valorCandidatoSinEliminados = Funcion.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
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


    }
}
