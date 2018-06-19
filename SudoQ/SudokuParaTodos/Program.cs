using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuParaTodos
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EngineData Valor = EngineData.Instance();
            if (args.Length == 0)
            {
                /*  Valor.SetOpenFrom(EngineData.Exe);
                Form1 f = new Form1();
                Application.Run(f);*/

                  Valor.SetOpenFrom(EngineData.File);
                  Valor.SetPathArchivo(@"D:\AAA.jll");
                  Application.Run(new Formularios.AzulUno());
            }
            else
            {
                Valor.SetOpenFrom(EngineData.File);
                Valor.SetPathArchivo(args[0]);
                Formularios.AzulUno  f = new Formularios.AzulUno();
                Application.Run(f);
            }
        }
    }
}
