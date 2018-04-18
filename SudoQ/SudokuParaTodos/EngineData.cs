using System;
using System.Collections.Generic;
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

    }
}
