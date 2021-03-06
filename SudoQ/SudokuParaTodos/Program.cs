﻿using System;
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
                Valor.SetOpenFrom(EngineData.Exe);
                 Application.Run(new Form1());

                /*Valor.SetOpenFrom(EngineData.File);
                Valor.SetPathArchivo(@"C:\Users\ASUS\Downloads\Sudoku\AAA.jll");
                Application.Run(new Form1());*/
            }
            else
            {
                Valor.SetOpenFrom(EngineData.File);
                Valor.SetPathArchivo(args[0]);
                Application.Run(new Form1());
            }
        }
    }
}
