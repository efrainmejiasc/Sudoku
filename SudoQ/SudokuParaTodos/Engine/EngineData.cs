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
       public const string DataFileSave = "Sudoku Para Todos | *.jll";
       public const string DataFileSaveHow = "Guardar Sudoku Como";
       public const string ExtensionFile = "jll";

       public const string portu = "portu";
       public const string english = "english";
       public const string IDIOMAS= "IDIOMAS";
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


    }
}
