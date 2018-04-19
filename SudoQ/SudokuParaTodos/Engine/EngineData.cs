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


        private string idioma = string.Empty;
       public void SetIdioma (string v) { idioma = v; }
       public string GetIdioma(string v) { return idioma; }

    }
}
