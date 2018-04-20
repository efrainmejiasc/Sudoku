using System;
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
       public const string Extension =".jll";
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
       public const string CulturaPortugues= "PT-PT";
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
      


       private string idioma = string.Empty;

       public void SetIdioma (string v) { idioma = v; }

       public string GetIdioma() { return idioma; }

       public string TituloForm (string lenguaje)
        {
            string titulo = string.Empty;
            switch (lenguaje)
            {
                case ("Español"):
                    titulo = "Sudoku Para Todos - Crear Juego                                                                                                                                                                                                  Numeros y Candidatos";
                    break;
                case ("Ingles"):
                    titulo = "Sudoku For All - Create Game                                                                                                                                                                                                  Numbers and Candidates";
                    break;
                case ("Portugues"):
                    titulo = "Sudoku Para Todos - Criar Jogo                                                                                                                                                                                                  Números e Candidatos";
                    break;
            }
            return titulo;
        }

        private int [] FilaA = { 0, 1, 2 };
        private int [] FilaB = { 3, 4, 5 };
        private int [] FilaC = { 6, 7, 8 };

        private int [] ColumnaA = { 0, 1, 2 };
        private int [] ColumnaB = { 3, 4, 5 };
        private int [] ColumnaC = { 6, 7, 8 };

        public int ObtenerRecuadro (int f , int c)
        {
            int recuadro = -1;
            if (FilaA.Contains(f))
            {
                if (ColumnaA.Contains(c)) recuadro = 1;
                else if (ColumnaB.Contains(c)) recuadro = 2;
                else if (ColumnaC.Contains(c)) recuadro = 3;
            }
            else if (FilaB.Contains(f))
            {
                if (ColumnaA.Contains(c)) recuadro = 4;
                else if (ColumnaB.Contains(c)) recuadro = 5;
                else if (ColumnaC.Contains(c)) recuadro = 6;
            }
            else if (FilaC.Contains(f))
            {
                if (ColumnaA.Contains(c)) recuadro = 7;
                else if (ColumnaB.Contains(c)) recuadro = 8;
                else if (ColumnaC.Contains(c)) recuadro = 9;
            }
            return recuadro;
        }

        public int [] IndiceFila (int recuadro)
        {
            int[] v = new int [3];
            switch (recuadro)  
            {
                case (1):
                    v = FilaA;
                    break;
                case (2):
                    v = FilaA;
                    break;
                case (3):
                    v = FilaA;
                    break;
                case (4):
                    v = FilaB;
                    break;
                case (5):
                    v = FilaB;
                    break;
                case (6):
                    v = FilaB;
                    break;
                case (7):
                    v = FilaC;
                    break;
                case (8):
                    v =  FilaC;
                    break;
                case (9):
                    v = FilaC;
                    break;
            }
            return v;
        }

        public int[] IndiceColumna(int recuadro)
        {
            int[] v = new int[3];
            switch (recuadro)
            {
                case (1):
                    v = ColumnaA;
                    break;
                case (2):
                    v = ColumnaA;
                    break;
                case (3):
                    v = ColumnaA;
                    break;
                case (4):
                    v = ColumnaB;
                    break;
                case (5):
                    v = ColumnaB;
                    break;
                case (6):
                    v = ColumnaB;
                    break;
                case (7):
                    v = ColumnaC;
                    break;
                case (8):
                    v = ColumnaC;
                    break;
                case (9):
                    v = ColumnaC;
                    break;
            }
            return v;
        }

    }
}
