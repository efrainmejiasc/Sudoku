using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public const string Titulo = "Sudoku Para Todos";
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
        public const string Zero = "0";
        public const string TipoLetra = "Microsoft Sans Serif";
        public const string BtnEspañol = "btnEspañol";
        public const string BtnIngles = "btnIngles";
        public const string BtnPortugues = "btnPortugues";

        public const string FiltroFile = " | *.jll";
        public const string ExtensionFile = "jll";

        public const string portu = "portu";
        public const string english = "english";
        public const string IDIOMAS = "IDIOMAS";
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

        public const string Up = "Up";
        public const string Down = "Down";
        public const string Right = "Right";
        public const string Left = "Left";

        public const string Exe = "Exe";
        public const string File = "File";

        public const int uno = 1;
        public const int dos = 2;

    

        public Color GetColorCeldaAct () { return Color.DarkGray; }

        private string openFrom = string.Empty;

        public void SetOpenFrom (string vOpen) { openFrom = vOpen; }

        public string GetOpenFrom () { return openFrom; }

        private string idioma = string.Empty;

        public void SetIdioma(string v) { idioma = v; }

        public string GetIdioma() { return idioma; }

        public string TituloForm(string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos - Crear Juego                                                                                                                                                                                                 Numeros y Candidatos";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku For All - Create Game                                                                                                                                                                                                  Numbers and Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos - Criar Jogo                                                                                                                                                                                                  Números e Candidatos";
                    break;
                default:
                    titulo = "Sudoku Para Todos - Crear Juego                                                                                                                                                                                                 Numeros y Candidatos";
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

        private ArrayList arrText = new ArrayList();

        public ArrayList GetArrText() { return arrText; }

        public void SetArrText(ArrayList vArrText) { arrText = vArrText; }

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


    }
}
