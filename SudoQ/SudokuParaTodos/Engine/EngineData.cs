using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuParaTodos
{
    public class EngineData
    {
        private static EngineData valor;

        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }

        public const string Titulo = "Sudoku Para Todos ";
        public const string Numeros = " - Numeros:";
        public const string ClaveRegWin = "SudokuParaTodos";
        public const string FechaDeCreacion = "FechaDeCreacion";
        public const string Clave = "Clave";
        public const string Extension = ".jll";
        public const string ArchivoEjecutable = "SudokuParaTodos.exe";
        public const string ProgramaId = "SudokuParaTodos";
        public const string Comando = "open";
        public const string DescripcionPrograma = "SudokuParaTodos File";
        public const bool Falso = false;
        public const bool Verdadero = true;
        public const string Español = "mIEspañol";
        public const string CulturaEspañol = "ES-VE";
        public const string Ingles = "mIIngles";
        public const string CulturaIngles = "EN-US";
        public const string Portugues = "mIPortugues";
        public const string CulturaPortugues = "PT-PT";
        public const string LenguajeEspañol = "Español";
        public const string LenguajeIngles = "Ingles";
        public const string LenguajePortugues = "Portugues";
        public const string BtnAbrirJuego = "btnAbrir";
        public const string BtnGuardarJuego = "btnGuardar";
        public const string BtnOtroJuego = "btnOtro";
        public const string BtnSolucion = "btnSolucion";
        public const string TipoLetra = "Microsoft Sans Serif";
        public const string BtnEspañol = "btnEspañol";
        public const string BtnIngles = "btnIngles";
        public const string BtnPortugues = "btnPortugues";

        public const string FiltroFile = " | *.jll";
        public const string ExtensionFile = "jll";

        public const string portu = "portu";
        public const string english = "english";
        public const string IDIOMAS = "IDIOMAS - LANGUAGES";
        public const string LANGUAGES = "LANGUAGES";

        public const string Btn1 = "btn1";
        public const string Btn2 = "btn2";
        public const string Btn3 = "btn3";
        public const string Btn4 = "btn4";
        public const string Btn5 = "btn5";
        public const string Btn6 = "btn6";
        public const string Btn7 = "btn7";
        public const string Btn8 = "btn8";
        public const string Btn9 = "btn9";

        public const string BtnRes23 = "btnRes23";
        public const string BtnDos = "btnDos";
        public const string BtnTres = "btnTres";
        public const string BtnN = "btnN";

        public const string btnIzquierda = "btnIzquierda";
        public const string btnDerecha = "btnDerecha";

        public const string Up = "Up";
        public const string Down = "Down";
        public const string Right = "Right";
        public const string Left = "Left";

        public const string Exe = "Exe";
        public const string File = "File";

        public const int one = 1;
        public const int two = 2;

        public const string Zero = "0";
        public const string uno = "1";
        public const string dos = "2";
        public const string tres = "3";
        public const string cuatro = "4";
        public const string cinco = "5";
        public const string seis = "6";
        public const string siete = "7";
        public const string ocho = "8";
        public const string nueve = "9";

        public const string eliminar = "btnEL";
        public const string restablecer = "btnR";



        public Color GetColorCeldaAct () { return Color.DarkGray; }

        private string openFrom = string.Empty;

        public void SetOpenFrom (string vOpen) { openFrom = vOpen; }

        public string GetOpenFrom () { return openFrom; }

        private string nombreIdioma = string.Empty;

        public void SetNombreIdioma(string v) { nombreIdioma = v; }

        public string GetNombreIdioma()
        {
            return nombreIdioma;
        }

        public string NombreIdiomaCultura(string vCultura)
        {
            switch (vCultura)
            {
                case (CulturaEspañol):
                    nombreIdioma = LenguajeEspañol ;
                    break;
                case (CulturaIngles):
                    nombreIdioma = LenguajeIngles;
                    break;
                case (CulturaPortugues):
                    nombreIdioma = LenguajePortugues;
                    break;
                default:
                    nombreIdioma = LenguajeEspañol;
                    break;
            }
            return nombreIdioma;
        }

        private string idioma = string.Empty;

        public void SetIdioma(string v) { idioma = v; }

        public string GetIdioma() { return idioma; }

        public string TituloForm(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos";
                    break;
                default:
                    titulo = "Sudoku Para Todos";
                    break;
            }
            return titulo;
        }

        public string TituloFormA2(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos: ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string TituloFormR1(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string TituloFormR2(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string TituloFormR3(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string TituloFormY1(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string TituloFormY2(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string TituloFormY3(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos : ";
                    break;
                default:
                    titulo = "Sudoku Para Todos : ";
                    break;
            }
            return titulo;
        }

        public string EiquetaCrearJuego(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Números/Números y Candidatos";
                    break;
                case ("Ingles"):
                    titulo = "Numbers/Numbers and Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Números/Números e Candidatos";
                    break;
                default:
                    titulo = "Números/Números y Candidatos";
                    break;
            }
            return titulo;
        }

        public string EiquetaAzulUno(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Números";
                    break;
                case ("Ingles"):
                    titulo = "Numbers";
                    break;
                case ("Portugues"):
                    titulo = "Números";
                    break;
                default:
                    titulo = "Números";
                    break;
            }
            return titulo;
        }

        public string EtiquetaAzulDos(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Números/Números y Candidatos Excluidos";
                    break;
                case ("Ingles"):
                    titulo = "Numbers/Numbers and Excluded Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Números/Números e Candidatos Excluídos";
                    break;
                default:
                    titulo = "Números/Números y Candidatos Excluidos";
                    break;
            }
            return titulo;
        }

        public string EtiquetaRojoUno(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Candidatos Filtrados/Candidatos Excluidos";
                    break;
                case ("Ingles"):
                    titulo = "Filtered Candidates/Excluded Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Candidatos Filtrados/Candidatos Excluídos";
                    break;
                default:
                    titulo = "Candidatos Filtrados/Candidatos Excluidos";
                    break;
            }
            return titulo;
        }

        public string EtiquetaRojoDos(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Candidatos Ordenados/Candidatos Excluidos";
                    break;
                case ("Ingles"):
                    titulo = "Sorted Candidates/Excluded Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Candidatos Encomendados/Candidatos Excluídos";
                    break;
                default:
                    titulo = "Candidatos Ordenados/Candidatos Excluidos";
                    break;
            }
            return titulo;
        }

        public string EtiquetaRojoTres(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Candidatos Filtrados/Candidatos";
                    break;
                case ("Ingles"):
                    titulo = "Filtered Candidates/Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Candidatos Filtrados/Candidatos";
                    break;
                default:
                    titulo = "Candidatos Filtrados/Candidatos";
                    break;
            }
            return titulo;
        }

        public string EtiquetaAyuda1(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Números/Celdas Vacias/AYUDA1";
                    break;
                case ("Ingles"):
                    titulo = "Numbers/Empty Squares/HELP1";
                    break;
                case ("Portugues"):
                    titulo = "Números/Quadrados Vazias/AJUDA1";
                    break;
                default:
                    titulo = "Números/Celdas Vacias/AYUDA1";
                    break;
            }
            return titulo;
        }

        public string EtiquetaAyuda2(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Solucion/Numeros/AYUDA2";
                    break;
                case ("Ingles"):
                    titulo = "Solution/Numbers/HELP2";
                    break;
                case ("Portugues"):
                    titulo = "Solução/Números/AJUDA2";
                    break;
                default:
                    titulo = "Solucion/Numeros/AYUDA2";
                    break;
            }
            return titulo;
        }

        public string EtiquetaAyuda3(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Solucion/Candidatos Excluidos/AYUDA3";
                    break;
                case ("Ingles"):
                    titulo = "Solution/Excluded Candidates/HELP3";
                    break;
                case ("Portugues"):
                    titulo = "Solução/Candidatos Excluídos/AJUDA3";
                    break;
                default:
                    titulo = "Solucion/Candidatos Excluidos/AYUDA3";
                    break;
            }
            return titulo;
        }

        public string NombreJuegoFileFiltro(string lenguaje)
        {
            string nombreJuego = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    nombreJuego = "Sudoku Para Todos" + FiltroFile;
                    break;
                case ("Ingles"):
                    nombreJuego = "Sudoku For All" + FiltroFile;
                    break;
                case ("Portugues"):
                    nombreJuego = "Sudoku Para Todos" + FiltroFile;
                    break;
                default:
                    nombreJuego = "Sudoku Para Todos" + FiltroFile;
                    break;
            }
            return nombreJuego;
        }

        public string TituloGuardarJuego(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Guardar Sudoku Como";
                    break;
                case ("Ingles"):
                    titulo = "Save Sudoku As";
                    break;
                case ("Portugues"):
                    titulo = "Salve o Sudoku Como";
                    break;
                default:
                    titulo = "Guardar Sudoku Como";
                    break;
            }
            return titulo;
        }

        public string NombreAbrirJuego(string lenguaje)
        {
            string nombreJuego = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    nombreJuego = "Archivos de Texto" + FiltroFile;
                    break;
                case ("Ingles"):
                    nombreJuego = "Text Files" + FiltroFile;
                    break;
                case ("Portugues"):
                    nombreJuego = "Arquivos de Textos" + FiltroFile;
                    break;
                default:
                    nombreJuego = "Archivos de Texto" + FiltroFile;
                    break;
            }
            return nombreJuego;
        }

        public string TextoAbrirJuego(string lenguaje)
        {
            string nombreJuego = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    nombreJuego = "Abrir Juego";
                    break;
                case ("Ingles"):
                    nombreJuego = "Open Game";
                    break;
                case ("Portugues"):
                    nombreJuego = "Jogo Aberto";
                    break;
                default:
                    nombreJuego = "Abrir Juego";
                    break;
            }
            return nombreJuego;
        }

        public string Mensaje1(string lenguaje)
        {
            string mensaje = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    mensaje = "DEBE GUARDAR VALORES DE INICIO DEL JUEGO ANTES DE GUARDAR LA SOLUCION";
                    break;
                case ("Ingles"):
                    mensaje = "YOU MUST SAVE VALUES TO START THE GAME BEFORE SAVING THE SOLUTION";
                    break;
                case ("Portugues"):
                    mensaje = "VOCÊ DEVE SALVAR VALORES PARA INICIAR O JOGO ANTES DE SALVAR A SOLUÇÃO";
                    break;
                default:
                    mensaje = "DEBE GUARDAR VALORES DE INICIO DEL JUEGO ANTES DE GUARDAR LA SOLUCION";
                    break;
            }
            return mensaje;
        }

        public string Mensaje2(string lenguaje)
        {
            string mensaje = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    mensaje = "DEBE INGRESAR VALORES DE INICIO DEL JUEGO";
                    break;
                case ("Ingles"):
                    mensaje = "MUST ENTER VALUES STARTING THE GAME";
                    break;
                case ("Portugues"):
                    mensaje = "DEVE INSERIR VALORES INICIANDO O JOGO";
                    break;
                default:
                    mensaje = "DEBE INGRESAR VALORES DE INICIO DEL JUEGO";
                    break;
            }
            return mensaje;
        }

        public string TituloMensaje(string lenguaje)
        {
            string mensaje = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    mensaje = "INFORMACION DEL SISTEMA";
                    break;
                case ("Ingles"):
                    mensaje = "SYSTEM INFORMATION";
                    break;
                case ("Portugues"):
                    mensaje = "INFORMAÇÃO DO SISTEMA";
                    break;
                default:
                    mensaje = "INFORMACION DEL SISTEMA";
                    break;
            }
            return mensaje;
        }

        private string pathArchivo = string.Empty;

        public string GetPathArchivo()
        {
            return pathArchivo;
        }

        public void SetPathArchivo(string pArchivo)
        {
            pathArchivo = pArchivo;
        }

        private string nombreJuego = string.Empty ;

        public string GetNombreJuego()
        {
            return nombreJuego;
        }

        public void SetNombreJuego(string nArchivo)
        {
            nombreJuego= nArchivo;
        }

        private ArrayList arrText = new ArrayList();

        public ArrayList GetArrText() { return arrText; }

        public void SetArrText(ArrayList vArrText) { arrText = vArrText; }

        //**********************************************************************************
        private string[,] valorIngresado = new string[9, 9];

        public string[,] GetValorIngresado()
        {
            return valorIngresado;
        }

        public void SetValorIngresado(string[,] vIngresado)
        {
            this.valorIngresado = new string[9, 9];
            this.valorIngresado = vIngresado;
        }

        private string[,] valorEliminado = new string[9, 9];

        public string[,] GetValorEliminado()
        {
            return valorEliminado;
        }

        public void SetValorEliminado(string[,] vEliminado)
        {
            this.valorEliminado = new string[9, 9];
            this.valorEliminado = vEliminado;
        }

        private string[,] valorInicio = new string[9, 9];

        public string[,] GetValorInicio()
        {
            return valorInicio;
        }

        public void SetValorInicio(string[,] vInicio)
        {
            this.valorInicio = new string[9, 9];
            this.valorInicio = vInicio;
        }

        private string[,] valorSolucion = new string[9, 9];

        public string[,] GetValorSolucion()
        {
            return valorSolucion;
        }

        public void SetValorSolucion(string[,] vSolucion)
        {
            this.valorSolucion = new string[9, 9];
            this.valorSolucion = vSolucion;
        }

        private string[,] numFiltro = new string[9, 9];

        public string[,] GetNumFiltro()
        {
            return numFiltro;
        }

        public void SetNumFiltro(string[,] vFiltro)
        {
            this.numFiltro = new string[9, 9];
            this.numFiltro = vFiltro;
        }

        //**********************************************************************************

        private bool objFrom  = false;

        public void SetObjFrom(bool from) { objFrom = from; }

        public bool GetObjFrom () { return objFrom; }

        private bool visiblidadACB = true ;

        public void SetVisibilidadACB (bool v) { visiblidadACB = v; }

        public bool GetVisibilidadACB() { return visiblidadACB; }

        //***********************************************************************************

        private string[] f1 = { "11", "12", "13", "14", "15", "16", "17", "18", "19" };
        private string[] f2 = { "21", "22", "23", "24", "25", "26", "27", "28", "29" };
        private string[] f3 = { "31", "32", "33", "34", "35", "36", "37", "38", "39" };
        private string[] f4 = { "41", "42", "43", "44", "45", "46", "47", "48", "49" };
        private string[] f5 = { "51", "52", "53", "54", "55", "56", "57", "58", "59" };
        private string[] f6 = { "61", "62", "63", "64", "65", "66", "67", "68", "69" };
        private string[] f7 = { "71", "72", "73", "74", "75", "76", "77", "78", "79" };
        private string[] f8 = { "81", "82", "83", "84", "85", "86", "87", "88", "89" };
        private string[] f9 = { "91", "92", "93", "94", "95", "96", "97", "98", "99" };

        public string [] GetNumeroFila (int f)
        {
            string[] fila = new string[9];
            switch (f)
            {
                case (0):
                    fila = f1;
                    break;
                case (1):
                    fila = f2;
                    break;
                case (2):
                    fila = f3;
                    break;
                case (3):
                    fila = f4;
                    break;
                case (4):
                    fila = f5;
                    break;
                case (5):
                    fila = f6;
                    break;
                case (6):
                    fila = f7;
                    break;
                case (7):
                    fila = f8;
                    break;
                case (8):
                    fila = f9;
                    break;
            }
            return fila;
        }

        private string[] c1 = { "11", "21", "31", "41", "51", "61", "71", "81", "91" };
        private string[] c2 = { "12", "22", "32", "42", "52", "62", "72", "82", "92" };
        private string[] c3 = { "13", "23", "33", "43", "53", "63", "73", "83", "93" };
        private string[] c4 = { "14", "24", "34", "44", "54", "64", "74", "84", "94" };
        private string[] c5 = { "15", "25", "35", "45", "55", "65", "75", "85", "95" };
        private string[] c6 = { "16", "26", "36", "46", "56", "66", "76", "86", "96" };
        private string[] c7 = { "17", "27", "37", "47", "57", "67", "77", "87", "97" };
        private string[] c8 = { "18", "28", "38", "48", "58", "68", "78", "88", "98" };
        private string[] c9 = { "19", "29", "39", "49", "59", "69", "79", "89", "99" };

        public string[] GetNumeroColumna(int c)
        {
            string[] columna = new string[9];
            switch (c)
            {
                case (0):
                    columna = c1;
                    break;
                case (1):
                    columna = c2;
                    break;
                case (2):
                    columna = c3;
                    break;
                case (3):
                    columna = c4;
                    break;
                case (4):
                    columna = c5;
                    break;
                case (5):
                    columna = c6;
                    break;
                case (6):
                    columna = c7;
                    break;
                case (7):
                    columna = c8;
                    break;
                case (8):
                    columna = c9;
                    break;
            }
            return columna;
        }

        private string[] r1 = { "11", "12", "13", "21", "22", "23", "31", "32", "33" };
        private string[] r2 = { "14", "15", "16", "24", "25", "26", "34", "35", "36" };
        private string[] r3 = { "17", "18", "19", "27", "28", "29", "37", "38", "39" };
        private string[] r4 = { "41", "42", "43", "51", "52", "53", "61", "62", "63" };
        private string[] r5 = { "44", "45", "46", "54", "55", "56", "64", "65", "66" };
        private string[] r6 = { "47", "48", "49", "57", "58", "59", "67", "68", "69" };
        private string[] r7 = { "71", "72", "73", "81", "82", "83", "91", "92", "93" };
        private string[] r8 = { "74", "75", "76", "84", "85", "86", "94", "95", "96" };
        private string[] r9 = { "77", "78", "79", "87", "88", "89", "97", "98", "99" };

        public string[] GetNumeroRecuadro(int r)
        {
            string[] recuadro = new string[9];
            switch (r)
            {
                case (0):
                    recuadro = r1;
                    break;
                case (1):
                    recuadro = r2;
                    break;
                case (2):
                    recuadro = r3;
                    break;
                case (3):
                    recuadro = r4;
                    break;
                case (4):
                    recuadro = r5;
                    break;
                case (5):
                    recuadro = r6;
                    break;
                case (6):
                    recuadro = r7;
                    break;
                case (7):
                    recuadro = r8;
                    break;
                case (8):
                    recuadro = r9;
                    break;
            }
            return recuadro;
        }

        public string  TituloEtiqueta1 (string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "INFORMACION DEL SISTEMA";
                    break;
                case ("Ingles"):
                    titulo = "SYSTEM INFORMATION";
                    break;
                case ("Portugues"):
                    titulo = "INFORMAÇÃO DO SISTEMA";
                    break;
                default:
                    titulo = "INFORMACION DEL SISTEMA";
                    break;
            }
            return titulo;
        }

        public string TituloEtiqueta2(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "¿ Desea Salir de la Aplicacion ?";
                    break;
                case ("Ingles"):
                    titulo = "Do you want to exit the application ?";
                    break;
                case ("Portugues"):
                    titulo = "Você quer sair do aplicativo ?";
                    break;
                default:
                    titulo = "¿ Desea Salir de la Aplicacion ?";
                    break;
            }
            return titulo;
        }

        public string TituloEtiquetaSave(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "¿ Quieres guardar los cambios que has hecho a este juego ?";
                    break;
                case ("Ingles"):
                    titulo = "Do you want to save the changes you made in this game ?";
                    break;
                case ("Portugues"):
                    titulo = "Você quer salvar as alterações que você fez neste jogo?";
                    break;
                default:
                    titulo = "¿ Quieres guardar los cambios que has hecho a este juego ?";
                    break;
            }
            return titulo;
        }

        public string TituloButton1(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "SI";
                    break;
                case ("Ingles"):
                    titulo = "YES";
                    break;
                case ("Portugues"):
                    titulo = "SIM";
                    break;
                default:
                    titulo = "SI";
                    break;
            }
            return titulo;
        }

        public string TituloButton2(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "NO";
                    break;
                case ("Ingles"):
                    titulo = "NO";
                    break;
                case ("Portugues"):
                    titulo = "NÃO";
                    break;
                default:
                    titulo = "NO";
                    break;
            }
            return titulo;
        }

        public string TituloButton3(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "CANCELAR";
                    break;
                case ("Ingles"):
                    titulo = "CANCEL";
                    break;
                case ("Portugues"):
                    titulo = "CANCELAR";
                    break;
                default:
                    titulo = "CANCELAR";
                    break;
            }
            return titulo;
        }

        public string TituloReiniciar(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Al reiniciar el juego se borrara todo lo jugado " + Environment.NewLine + "¿Es eso lo que desea?";
                    break;
                case ("Ingles"):
                    titulo = "When you restart the game, everything played will be delete " + Environment.NewLine + "Is that what you want?";
                    break;
                case ("Portugues"):
                    titulo = "Quando você reiniciar o jogo, tudo sera deletado " + Environment.NewLine + "É issoo que você quer?";
                    break;
                default:
                    titulo = "Al reiniciar el juego se borrara todo lo jugado " + Environment.NewLine + "¿Es eso lo que desea?";
                    break;
            }
            return titulo;
        }

        private bool continuar = false;

        public void SetContinuar (bool value) { continuar = value; }

        public bool GetContinuar() { return continuar; }

        private bool salir = false;

        public void SetSalirJuego(bool value) { salir = value; }

        public bool GetSalirJuego () { return salir; }

        public string Fila(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Fila";
                    break;
                case ("Ingles"):
                    titulo = "Row";
                    break;
                case ("Portugues"):
                    titulo = "Linha";
                    break;
                default:
                    titulo = "Fila";
                    break;
            }
            return titulo;
        }

        public string Columna(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Columna";
                    break;
                case ("Ingles"):
                    titulo = "Column";
                    break;
                case ("Portugues"):
                    titulo = "Coluna";
                    break;
                default:
                    titulo = "Columna";
                    break;
            }
            return titulo;
        }

        public string Recuadro(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Recuadro";
                    break;
                case ("Ingles"):
                    titulo = "Box";
                    break;
                case ("Portugues"):
                    titulo = "Quadrante";
                    break;
                default:
                    titulo = "Recuadro";
                    break;
            }
            return titulo;
        }

        public string FilaColumnaRecuadro(string lenguaje,string tipo)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español" ):
                    if (tipo == "FILA")
                        titulo = "Fila";
                    else if (tipo == "COLUMNA")
                        titulo = "Columna";
                    else if (tipo == "RECUADRO")
                        titulo = "Recuadro";
                        break;
                case ("Ingles"):
                    if (tipo == "FILA")
                        titulo = "Row";
                    else if (tipo == "COLUMNA")
                        titulo = "Column";
                    else if (tipo == "RECUADRO")
                        titulo = "Box";
                    break;
                case ("Portugues"):
                    if (tipo == "FILA")
                        titulo = "Linha";
                    else if (tipo == "COLUMNA")
                        titulo = "Coluna";
                    else if (tipo == "RECUADRO")
                        titulo = "Quadrante";
                    break;
                default:
                    if (tipo == "FILA")
                        titulo = "Fila";
                    else if (tipo == "COLUMNA")
                        titulo = "Columna";
                    else if (tipo == "RECUADRO")
                        titulo = "Recuadro";
                    break;
            }
            return titulo;
        }

        private bool selectIdioma = false ;
        
        public  bool GetSelectIdioma() { return selectIdioma; }

        public void  SetSelectIdioma (bool v) { selectIdioma = v; }

    }
}
