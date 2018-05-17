using Microsoft.Win32;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Data;

namespace SudokuParaTodos
{
    public class EngineSudoku
    {
        private EngineData Valor = EngineData.Instance();
        private int recuadro = -1;
        private int[] pos = new int[2];

        private RegistryKey key = Registry.CurrentUser;

        public bool ExisteClaveRegWin()
        {
            bool respuesta = false;
            string[] subKeys = key.GetSubKeyNames();
            for (int i = 0; i <= subKeys.Length - 1; i++)
            {
                if (subKeys[i].ToString() == EngineData.ClaveRegWin) respuesta = true;
            }
            return respuesta;
        }

        public void AgregarClaveRegWin()
        {
            key = Registry.CurrentUser.CreateSubKey(EngineData.ClaveRegWin);
            key.SetValue(EngineData.FechaDeCreacion, DateTime.Now.ToShortDateString(), RegistryValueKind.String);
            key.SetValue(EngineData.Clave, EngineData.ClaveRegWin, RegistryValueKind.String);
            key.Close();
        }

        public void AsociarExtension()
        {
            if (GetProgIdFromExtension(EngineData.Extension) == null && GetProgIdFromExtension(EngineData.Extension) == string.Empty)
            {
                LinkExtension(EngineData.Extension, EngineData.ArchivoEjecutable, EngineData.ProgramaId, EngineData.Comando, EngineData.DescripcionPrograma);
            }
        }

        private string GetProgIdFromExtension(string extension)
        {
            string strProgramID = string.Empty;
            using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(extension))
            {
                if (registryKey?.GetValue(string.Empty) != null)
                {
                    strProgramID = registryKey.GetValue(string.Empty).ToString();
                    registryKey.Close();
                }
            }
            return strProgramID;
        }

        public void LinkExtension(string extension, string executableFileName, string programId, string command, string description)
        {
            string linkedProgramID;
            RegistryKey registryKey = null;
            RegistryKey registryKeyShell = null;

            if (string.IsNullOrEmpty(command))
                command = EngineData.Comando;
            if (string.IsNullOrEmpty(description))
                description = $"{extension} Descripción de {programId}";
            if (!extension.StartsWith("."))
                extension = "." + extension;

            linkedProgramID = GetProgIdFromExtension(extension);
            if (string.IsNullOrEmpty(linkedProgramID) || linkedProgramID.Length == 0)
            {
                registryKey = Registry.ClassesRoot.CreateSubKey(extension);
                registryKey?.SetValue(string.Empty, programId);
                registryKey = Registry.ClassesRoot.CreateSubKey(programId);
                registryKey?.SetValue(string.Empty, description);
                registryKeyShell = registryKey?.CreateSubKey($"shell\\{command}\\command");
            }
            else
            {
                registryKey = Registry.ClassesRoot.OpenSubKey(linkedProgramID, true);
                registryKeyShell = registryKey?.OpenSubKey($"shell\\{command}\\command", true);
                if (registryKeyShell == null)
                    registryKeyShell = registryKey?.CreateSubKey(programId);

            }
            if (registryKeyShell != null)
            {
                registryKeyShell.SetValue(string.Empty, $"\"{executableFileName}\" \"%1\"");
                registryKeyShell.Close();
            }
        }

        // METODOS DEL JUEGO////////////////////////////////////////////////////////////////////
        public Button [] ColoresPincel(Button [] v)
        {
            v[0].BackColor = Color.Silver;
            v[1].BackColor = Color.SkyBlue;
            v[2].BackColor = Color.CornflowerBlue;
            v[3].BackColor = Color.LightCoral;
            v[4].BackColor = Color.Crimson;

            v[5].BackColor = Color.PaleGreen;
            v[6].BackColor = Color.YellowGreen;
            v[7].BackColor = Color.LightSalmon;
            v[8].BackColor = Color.Orange;
            return v;
        }

        public string [,] CandidatosJuego(string[,] vIngresado , string [,] valorCandidato) 
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] == string.Empty || vIngresado[f, c] == null)
                        valorCandidato[f, c] = "1 2 3" + Environment.NewLine + "4 5 6" + Environment.NewLine + "7 8 9";
                    else
                        valorCandidato[f, c] = vIngresado[f, c];
                }
            }

            return valorCandidato;
        }

        public TextBox [,] SetearTextBoxJuego(TextBox[,] cajaTexto, string[,] vIngresado , string [,] vCandidato, string [,] vInicio, Color colorA, Color colorB, float fontBig = 20, float fontSmall = 8 ,string lado = EngineData.Right)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (lado == EngineData.Right)
                    {
                        if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vIngresado[f, c];
                            cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontBig);
                            cajaTexto[f, c].ForeColor = colorA;
                        }
                        else if (vIngresado[f, c] == null || vIngresado[f, c] == string.Empty)
                        {
                            cajaTexto[f, c].Text = vCandidato[f, c];
                            cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontSmall);
                            cajaTexto[f, c].ForeColor = colorB;
                        }
                    }
                    else
                    {
                        if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vIngresado[f, c];
                            cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontBig);
                            cajaTexto[f, c].ForeColor = colorA;
                            if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                            {
                                cajaTexto[f, c].ForeColor = Color.Black;
                            }

                        }
                    }
                   cajaTexto[f,c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox [,] SetearTextBoxJuegoInicio(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = vIngresado[f, c];
                        cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, 20);
                        cajaTexto[f, c].ForeColor = Color.Blue;
                        if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].ForeColor = Color.Black;
                        }

                    }
                    cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox [,] SetearTextBoxLimpio(TextBox[,] cajaTexto)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].Text = string.Empty;
                }           
            }
            return cajaTexto;
        }

        public string [,] LimpiarArreglo(string[,] arreglo)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    arreglo[f, c] = null;
                }
            }
            return arreglo;
        }

        public int ContadorIngresado(string[,] valorIngresado)
        {
            int contadorIngresado = 0;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado [f,c] != null && valorIngresado [f,c] != string.Empty) { contadorIngresado++; }
                }
            }
             return contadorIngresado;
        }

        public int [] Position(string sentido, int f, int c)
        {
            switch (sentido)
            {
                case "Up":
                    pos[0] = f - 1; pos[1] = c;
                    break;
                case "Down":
                    pos[0] = f + 1; pos[1] = c;
                    break;
                case "Right":
                    pos[0] = f; pos[1] = c + 1;
                    break;
                case "Left":
                    pos[0] = f; pos[1] = c - 1;
                    break;
            }
            return pos;
        }

        public TextBox [,] SetearTextColorInicio(TextBox[,] cajaTexto)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                }
            }
            return cajaTexto;
        }

        // LETRAS JUEGO 
        public class LetrasJuego
        {
            public double A { get; set; }
            public double B { get; set; }
            public double C { get; set; }
            public double F { get; set; }
            public double E { get; set; }
            public double G { get; set; }
        }

        public int ContadorCandidatos(string[,] valorIngresado, string[,] valorCandidatoSinEliminados)
        {
            int contadorCandidatos = 0;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(System.Environment.NewLine, "");
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                        contadorCandidatos = contadorCandidatos + valorCandidatoSinEliminados[f, c].Length;
                    }
                }
            }
            return contadorCandidatos;
        }

        public LetrasJuego SetLetrasJuego(int num, string[,] valorIngresado, string[,] valorCandidatoSinEliminados)
        {
            LetrasJuego letras = new LetrasJuego
            {
                F = num,
                E = 81 - num,
                G = ContadorCandidatos(valorIngresado, valorCandidatoSinEliminados)
            };
            return letras;
        }

        // METODOS NUMEROS + CANDIDATOS 
        public string [,] ElejiblesInstantaneos(string[,] valorIngresado, string[,] valorCandidato )
        {
            ListBox enterRecuadro = new ListBox();
            ListBox enterFila = new ListBox();
            ListBox enterColumna = new ListBox();
            ListBox elejiblesRecuadro = new ListBox();
            ListBox elejiblesFila = new ListBox();
            ListBox elejiblesColumna = new ListBox();
            ListBox elejiblesCelda = new ListBox();
            String valor = string.Empty;

            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        int[] posicion = new int[2];
                        enterRecuadro.Items.Clear(); elejiblesRecuadro.Items.Clear();
                        enterFila.Items.Clear(); elejiblesFila.Items.Clear();
                        enterColumna.Items.Clear(); elejiblesColumna.Items.Clear();
                        elejiblesCelda.Items.Clear();
                        posicion = GetRecuadro(f, c);
                        enterRecuadro = IngresadoRecuadro(posicion[0], posicion[1], enterRecuadro,valorIngresado);
                        elejiblesRecuadro = CandidatosRecuadro(enterRecuadro);
                        enterFila = IngresadoFila(f, enterFila, valorIngresado);
                        elejiblesFila = CandidatosFila(enterFila);
                        enterColumna = IngresadoColumna(c, enterColumna,valorIngresado);
                        elejiblesColumna = CandidatosColumna(enterColumna);
                        elejiblesCelda = ElejiblesDefinitivo(elejiblesRecuadro, elejiblesFila, elejiblesColumna);
                        valor = string.Empty; int indic = 1;
                        foreach (String v in elejiblesCelda.Items)
                        {
                            if (indic == 3 || indic == 6 || indic == 9)
                            {
                                valor = valor + " " + v + Environment.NewLine;
                            }
                            else
                            {
                                valor = valor + " " + v.Trim();
                            }
                            indic++;
                        }
                        valorCandidato[f, c] = valor;

                    }
                    else
                    {
                        valorCandidato[f, c] = valorIngresado[f, c];
                    }
                }
            }

            return valorCandidato;
        }

        private int[] GetRecuadro(int f, int c)
        {
            int[] p = new int[2];
            if ((f >= 0 && f <= 2) && (c >= 0 && c <= 2)) { p[0] = 0; p[1] = 0; recuadro = 0; }
            else if ((f >= 0 && f <= 2) && (c >= 3 && c <= 5)) { p[0] = 0; p[1] = 3; recuadro = 1; }
            else if ((f >= 0 && f <= 2) && (c >= 6 && c <= 8)) { p[0] = 0; p[1] = 6; recuadro = 2; }
            else if ((f >= 3 && f <= 5) && (c >= 0 && c <= 2)) { p[0] = 3; p[1] = 0; recuadro = 3; }
            else if ((f >= 3 && f <= 5) && (c >= 3 && c <= 5)) { p[0] = 3; p[1] = 3; recuadro = 4; }
            else if ((f >= 3 && f <= 5) && (c >= 6 && c <= 8)) { p[0] = 3; p[1] = 6; recuadro = 5; }
            else if ((f >= 6 && f <= 8) && (c >= 0 && c <= 2)) { p[0] = 6; p[1] = 0; recuadro = 6; }
            else if ((f >= 6 && f <= 8) && (c >= 3 && c <= 5)) { p[0] = 6; p[1] = 3; recuadro = 7; }
            else if ((f >= 6 && f <= 8) && (c >= 6 && c <= 8)) { p[0] = 6; p[1] = 6; recuadro = 8; }
            return p;
        }

        private ListBox IngresadoRecuadro(int fila, int columna, ListBox listaRecuadro,string[,] valorIngresado)
        {
            listaRecuadro = new ListBox();
            for (int f = fila; f <= fila + 2; f++)
            {
                for (int c = columna; c <= columna + 2; c++)
                {
                    if (valorIngresado[f, c] != null && valorIngresado[f, c] != "")
                    {
                        listaRecuadro.Items.Add(valorIngresado[f, c]);
                    }
                }
            }
            return listaRecuadro;
        }

        private ListBox CandidatosRecuadro(ListBox listaRecuadro)
        {
            ListBox listaRecuadroElejible = new ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (!listaRecuadro.Items.Contains(i.ToString()))
                {
                    listaRecuadroElejible.Items.Add(i.ToString());
                }
            }
            return listaRecuadroElejible;
        }

        private ListBox IngresadoFila(int fila, ListBox listaFila, string[,] valorIngresado)
        {
            listaFila = new ListBox();
            for (int columna = 0; columna <= 8; columna++)
            {
                if (valorIngresado[fila, columna] != null && valorIngresado[fila, columna] != string.Empty)
                {
                    listaFila.Items.Add(valorIngresado[fila, columna]);
                }
            }
            return listaFila;
        }

        private ListBox CandidatosFila(ListBox listaFila)
        {
            ListBox listaFilaElejible = new ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (!listaFila.Items.Contains(i.ToString()))
                {
                    listaFilaElejible.Items.Add(i.ToString());
                }
            }
            return listaFilaElejible;
        }

        private ListBox IngresadoColumna(int columna, ListBox listaColumna, string[,] valorIngresado)
        {
            listaColumna = new ListBox();
            for (int fila = 0; fila <= 8; fila++)
            {
                if (valorIngresado[fila, columna] != null && valorIngresado[fila, columna] != string.Empty)
                {
                    listaColumna.Items.Add(valorIngresado[fila, columna]);
                }
            }
            return listaColumna;
        }

        private ListBox CandidatosColumna(ListBox listaColumna)
        {
            ListBox listaColumnaElejible = new ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (!listaColumna.Items.Contains(i.ToString()))
                {
                    listaColumnaElejible.Items.Add(i.ToString());
                }
            }
            return listaColumnaElejible;
        }

        private ListBox ElejiblesDefinitivo(ListBox listaR, ListBox listaF, ListBox listaC)
        {
            ListBox listaDefinitiva = new ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (listaR.Items.Contains(i.ToString()) && listaF.Items.Contains(i.ToString()) && listaC.Items.Contains(i.ToString()))
                {
                    listaDefinitiva.Items.Add(i.ToString());
                }
            }
            return listaDefinitiva;
        }

        // SIN ELIMINADOS
        public string[,] CandidatosSinEliminados(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado)
        {
            ListBox candidatosOrganizados = new ListBox();
            ListBox eliminarOrganizados = new ListBox();
            string[,] valorCandidatoSinEliminados = new string[9, 9];
            string candidatosFC = string.Empty;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty) 
                    {
                        if (valorEliminado[f,c] != null && valorEliminado[f,c] != string.Empty)
                        {
                            candidatosOrganizados.Items.Clear();
                            eliminarOrganizados.Items.Clear();
                            candidatosFC = valorCandidato[f, c];
                            candidatosOrganizados = OrganizarCandidatos(candidatosOrganizados, candidatosFC);
                            eliminarOrganizados = OrganizarLista(eliminarOrganizados, valorEliminado[f, c]);
                            candidatosOrganizados = QuitarEliminados(candidatosOrganizados, eliminarOrganizados);
                            valorCandidatoSinEliminados = EstablecerCandidatosSinEliminados(candidatosOrganizados, f, c, valorCandidatoSinEliminados);
                        }
                        else
                        {
                          valorCandidatoSinEliminados[f, c] = valorCandidato[f,c];
                        }
                    }
                    else
                    {
                        valorCandidatoSinEliminados[f, c] = valorIngresado[f,c];
                    }
                }
            }
            return valorCandidatoSinEliminados;
        }

        private ListBox OrganizarCandidatos(ListBox lista, string candidatosFC)
        {
            ListBox listaAux = new ListBox();
            candidatosFC = candidatosFC.Trim();
            string[] item = candidatosFC.Split(' ');
            for (int i = 0; i <= item.Length - 1; i++)
            {
                listaAux.Items.Add(item[i].Trim());
            }
            foreach (string n in listaAux.Items)
            {
                if (n.Trim().Length == 1)
                {
                    lista.Items.Add(n.Trim());
                }
                if (n.Trim().Length > 1)
                {
                    lista.Items.Add(n.Substring(0, 1));
                    lista.Items.Add(n.Substring(2, 2));
                }
            }
            return lista;
        }

        private ListBox OrganizarLista(ListBox lista, string cadena)
        {
            ListBox listaAux = new ListBox();
            cadena = cadena.Trim();
            string[] item = cadena.Split(' ');
            for (int i = 0; i <= item.Length - 1; i++)
            {
                listaAux.Items.Add(item[i].Trim());
            }
            foreach (string n in listaAux.Items)
            {
                if (n.Trim().Length == 1) { lista.Items.Add(n.Trim()); }
                if (n.Trim().Length > 1)
                {
                    lista.Items.Add(n.Substring(0, 1));
                    lista.Items.Add(n.Substring(2, 2));
                }
            }
            return lista;
        }

        private ListBox QuitarEliminados(ListBox candidatosOrganizados, ListBox cadenaEliminado)
        {
            int index = -1;
            ListBox eliminados = new ListBox();
            eliminados.Items.Clear();
            foreach (string valor in cadenaEliminado.Items)
            {
                index = candidatosOrganizados.FindString(valor.Trim());
                if (index > -1)
                {
                    eliminados.Items.Add(valor.Trim());
                    candidatosOrganizados.Items.RemoveAt(index);
                }
            }
            return candidatosOrganizados;
        }

        private string[,] EstablecerCandidatosSinEliminados(ListBox candidatosFinal, int f, int c, string[,] valorCandidatoSinEliminados)
        {
            if (candidatosFinal.Items.Count > 0)
            {
                string valor = string.Empty; int indic = 1;
                foreach (String v in candidatosFinal.Items)
                {
                    String I = v.Trim();
                    if (indic == 3 || indic == 6 || indic == 9)
                    {
                        valor = valor + " " + I + Environment.NewLine;
                    }
                    else
                    {
                        valor = valor + " " + I;
                    }
                    indic++;
                }
                valorCandidatoSinEliminados[f, c] = valor;
            }
            else
            {
                valorCandidatoSinEliminados[f, c] = string.Empty;
            }

            return valorCandidatoSinEliminados;
        }

        public TextBox[,] SetearTextBoxJuegoSinEliminados(TextBox[,] cajaTexto, string[,] valorCandidatoSinEliminados)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorCandidatoSinEliminados != null && valorCandidatoSinEliminados[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = valorCandidatoSinEliminados[f, c];
                        cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                    }
                }
            }
            return cajaTexto;
        }

        // GUARDAR ARCHIVO
        public bool ExisteValorIngresado(string[,] plantilla)
        {
            bool existeValor = false;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (plantilla[f, c] != null && plantilla[f, c] != string.Empty)
                    {
                        existeValor = true;
                        return existeValor;
                    }
                }
            }
            return existeValor;
        }

        public void GuardarValoresIngresados(string pathArchivo, string[,] valorIngresado)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo))
                {
                    string vIngresado = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorIngresado[f, c] != null && valorIngresado[f, c] != string.Empty)
                            {
                                vIngresado = valorIngresado[f, c].Trim();
                            }
                            else
                            {
                                vIngresado = EngineData.Zero;
                            }
                            if (c == 0) vLinea = vIngresado + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vIngresado + "-";
                            else if (c == 8) vLinea = vLinea + vIngresado;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }
                }
            }
        }

        public void GuardarValoresEliminados(string pathArchivo, string[,] valorEliminado)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vEliminado = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorEliminado[f, c] != null && valorEliminado[f, c] != string.Empty)
                            {
                                vEliminado = valorEliminado[f, c].Trim();
                            }
                            else
                            {
                                vEliminado = EngineData.Zero;
                            }
                            if (c == 0) vLinea = vEliminado + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vEliminado + "-";
                            else if (c == 8) vLinea = vLinea + vEliminado;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }

        }

        public void GuardarValoresInicio(string pathArchivo, string[,] valorInicio)
        {
            if (pathArchivo != null && pathArchivo != string.Empty)
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vInicio = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorInicio[f, c] != null && valorInicio[f, c] != string.Empty)
                            {
                                vInicio = valorInicio[f, c].Trim();
                            }
                            else
                            {
                                vInicio = EngineData.Zero;
                            }
                            if (c == 0) vLinea = vInicio + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vInicio + "-";
                            else if (c == 8) vLinea = vLinea + vInicio;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }
        }

        public void GuardarValoresSolucion(string pathArchivo, string[,] valorSolucion)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vSolucion = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorSolucion[f, c] != null && valorSolucion[f, c] != string.Empty)
                            {
                                vSolucion = valorSolucion[f, c].Trim();
                            }
                            else
                            {
                                vSolucion = EngineData.Zero;
                            }
                            if (c == 0) vLinea = vSolucion + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vSolucion + "-";
                            else if (c == 8) vLinea = vLinea + vSolucion;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty ;
                    }

                }
            }
        }

        //ATRIBUTOS ARCHIVO
        public void ReadWriteTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores & ~FileAttributes.ReadOnly);
        }

        public void OnlyReadTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores | FileAttributes.ReadOnly);
        }

        public bool StatusOnlyReadTxt(string pathArchivo)
        {
            bool r = false;
            FileAttributes atributos = File.GetAttributes(pathArchivo);
            if ((atributos & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
               r = true;
            }
            return r;
        }

        public bool ExiteArchivo(string pathArchivo)
        {
            bool resultado = false;
            if (File.Exists(pathArchivo))
            {
                resultado = true;
            }
            return resultado;
        }

        //ABRIR ARCHIVO
        public ArrayList AbrirValoresArchivo(string pathArchivo)
        {
            ArrayList arrText = new ArrayList();
            String sLine = string.Empty;
            try
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(pathArchivo);
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null) arrText.Add(sLine);
                }
                objReader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            return arrText;
        }

        public string [,] SetValorIngresado(ArrayList arrText , string [,] valorIngresado)
        {
            valorIngresado = new string[9,9];
            for (int f = 0; f <= 8; f++)
            {
                string[] lineaVector = arrText[f].ToString().Split('-');

                if (f >= 0 && f <= 8)
                {
                    if (lineaVector.Length != 9) return valorIngresado;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineData.Zero)
                        {
                            valorIngresado[f, columna] = lineaVector[columna];
                        }
                    }
                }

            }
            return valorIngresado;
        }

        public string[,] SetValorEliminado (ArrayList arrText, string[,] valorEliminado)
        {
            valorEliminado = new string[9, 9];
            for (int f = 0; f <= 17; f++)
            {
                string[] lineaVector = arrText[f].ToString().Split('-');

                if (f >= 9 && f <= 17)
                {
                    if (lineaVector.Length != 9) return valorEliminado;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineData.Zero)
                        {
                            valorEliminado[f - 9, columna] = lineaVector[columna];
                        }
                    }
                }

            }
            return valorEliminado;
        }

        public string[,] SetValorInicio(ArrayList arrText, string [,] valorInicio)
        {
            valorInicio = new string[9, 9];
            int fila = 0;
            for (int f = 0; f <= 35; f++)
            {
                if (f >= 18 && f <= 26)
                {
                    string[] lineaVector = arrText[f].ToString().Split('-');
                    if (lineaVector.Length != 9) return valorInicio;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineData.Zero)
                        {
                            valorInicio[fila, columna] = lineaVector[columna];
                        }
                    }
                    fila++;
                }
            }
            return valorInicio;
        }

        public string[,] SetValorSolucion(ArrayList arrText, string[,] valorSolucion)
        {
            int fila = 0;
            valorSolucion = new string[9, 9];
            for (int f = 0; f <= 35; f++)
            {
                if (f >= 27 && f <= 35)
                {
                    string[] lineaVector = arrText[f].ToString().Split('-');
                    if (lineaVector.Length != 9) return valorSolucion;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineData.Zero)
                        {
                            valorSolucion[fila, columna] = lineaVector[columna];
                        }
                    }
                    fila++;
                }
            }
            return valorSolucion;
        }

        //FILAS COLUMNAS RECUADROS
        public string[,] ObtenerSetearValoresFila(string[,] valorIngresado, string [,] valorCandidato, string [,] valorEliminado, int fila)//MANEJA PLANTILLA FILAS
        {
            string[,] valorPlantilla = new string[9, 9]; 
            ListBox candidatos = new ListBox();
            ListBox eliminadosOrganizados = new ListBox();
            for (int c = 0; c <= 8; c++)
            {
                if (valorIngresado[fila, c] == null || valorIngresado[fila, c] == string.Empty)
                {
                    int col = 0;
                    candidatos.Items.Clear();
                    candidatos = OrganizarLista(candidatos, valorCandidato[fila, c]);
                    if (valorEliminado[fila, c] != null && valorEliminado[fila, c] != string.Empty)//Si Existe Eliminado -> Eliminarlo
                    {
                        eliminadosOrganizados.Items.Clear();
                        eliminadosOrganizados = OrganizarLista(eliminadosOrganizados, valorEliminado[fila, c]);
                        candidatos = QuitarEliminados(candidatos, eliminadosOrganizados);
                    }
                    foreach (string candidato in candidatos.Items)
                    {
                        col = Convert.ToInt32(candidato)- 1;
                        valorPlantilla[c, col] = candidato;
                    }
                    candidatos.Items.Clear();
                }


            }
            return valorPlantilla;
        }

        public string[,] ObtenerSetearValoresColumna(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado, int columna) //MANEJA PLANTILLAS COLUMNA
        {
            string[,] valorPlantilla = new string[9, 9];
            ListBox candidatos = new ListBox();
            ListBox eliminadosOrganizados = new ListBox();
            for (int f = 0; f <= 8; f++)
            {
                if (valorIngresado[f, columna] == null || valorIngresado[f, columna] == string.Empty)
                {
                    int col = 0;
                    candidatos.Items.Clear();
                    candidatos = OrganizarLista(candidatos, valorCandidato[f, columna]);
                    if (valorEliminado[f, columna] != null && valorEliminado[f, columna] != string.Empty)//Si Existe Eliminado -> Eliminarlo
                    {
                        eliminadosOrganizados.Items.Clear();
                        eliminadosOrganizados = OrganizarLista(eliminadosOrganizados, valorEliminado[f, columna]);
                        candidatos = QuitarEliminados(candidatos, eliminadosOrganizados);
                    }
                    foreach (String candidato in candidatos.Items)
                    {
                        col = Convert.ToInt32(candidato) - 1;
                        valorPlantilla[f, col] = candidato;
                    }
                    candidatos.Items.Clear();
                }
            }
            return valorPlantilla;
        }

        public string[,] ObtenerSetearValoresRecuadro(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado, int fila ,int columna)//MANEJA PLANTILLAS RECUADRO
        {
            string[,] valorPlantilla = new string[9, 9];
            ListBox candidatos = new ListBox();
            ListBox eliminadosOrganizados = new ListBox();
            int i = 0;
            for (int f = fila; f <= fila + 2; f++)
            {
                for (int c = columna; c <= columna + 2; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        int col = 0;
                        candidatos.Items.Clear();
                        candidatos = OrganizarLista(candidatos, valorCandidato[f, c]);
                        if (valorEliminado[f, c] != null && valorEliminado[f, c] != string.Empty)//Si Existe Eliminado -> Eliminarlo
                        {
                            eliminadosOrganizados.Items.Clear();
                            eliminadosOrganizados = OrganizarLista(eliminadosOrganizados, valorEliminado[f, c]);
                            candidatos = QuitarEliminados(candidatos, eliminadosOrganizados);
                        }
                        foreach (String candidato in candidatos.Items)
                        {
                            col = Convert.ToInt32(candidato) - 1;
                            valorPlantilla[i, col] = candidato;
                        }
                        candidatos.Items.Clear();
                    }
                    i++;
                }
            }
            return valorPlantilla;
        }

        //ESTADOS SOLO DEL JUEGO FILAS COLUMNAS 
        public CandidatoUnicoCelda ExisteCandidatoUnico(string[,] valorIngresado, string[,] valorCandidatoSinEliminados,int f,int c)
        {
           CandidatoUnicoCelda Unico = new CandidatoUnicoCelda();
           int count = 0;
           if (valorIngresado[f, c] == string.Empty || valorIngresado[f, c] == null)
           {
             valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(Environment.NewLine, "");
             valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
             valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Trim();
              if (valorCandidatoSinEliminados[f, c].Length == 1)
              {
                    count = count + 1;
                    Unico.Fila = f;
                    Unico.Columna = c;
                    Unico.Valor = valorCandidatoSinEliminados[f, c];
                    Unico.Contador = count;
              }
           }
           return Unico;
        }

        public bool FilaCandidatoUnico(string[,] valorIngresado, int f , int c)
        {
            bool resultado = EngineData.Verdadero ;
            for (int col = 0; col <= 8; col++)
            {
                if (col != c)
                {
                    if (valorIngresado[f, col] == null || valorIngresado[f, col] == string.Empty)
                    {
                        resultado = EngineData.Falso;
                    }
                }
            }
            return resultado;
        }

        public int NumeroRecuadro(int f, int c)
        {
            int recuadro = -1;

            if ((f >= 0 && f <= 2) && (c >= 0 && c <= 2)) { recuadro = 0; }
            else if ((f >= 0 && f <= 2) && (c >= 3 && c <= 5)) { recuadro = 1; }
            else if ((f >= 0 && f <= 2) && (c >= 6 && c <= 8)) { recuadro = 2; }

            else if ((f >= 3 && f <= 5) && (c >= 0 && c <= 2)) { recuadro = 3; }
            else if ((f >= 3 && f <= 5) && (c >= 3 && c <= 5)) { recuadro = 4; }
            else if ((f >= 3 && f <= 5) && (c >= 6 && c <= 8)) { recuadro = 5; }

            else if ((f >= 6 && f <= 8) && (c >= 0 && c <= 2)) { recuadro = 6; }
            else if ((f >= 6 && f <= 8) && (c >= 3 && c <= 5)) { recuadro = 7; }
            else if ((f >= 6 && f <= 8) && (c >= 6 && c <= 8)) { recuadro = 8; }

            return recuadro;
        }

        public bool ColumnaCandidatoUnico(string[,] valorIngresado, int f, int c)
        {
            bool resultado = EngineData.Verdadero;
            for (int fil = 0; fil <= 8; fil++)
            {
                if (fil != f)
                {
                    if (valorIngresado[fil, c] == null || valorIngresado[fil, c] == string.Empty)
                    {
                        resultado = EngineData.Falso;
                    }
                }
            }
            return resultado;
        }

        public class CandidatoUnicoCelda
        {
            public int Fila { get; set; }
            public int Columna { get; set; }
            public int Contador { get; set; }
            public string Valor { get; set; }
        }

        //CREAR TABLAS 
        public DataTable CrearTabla1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("POS.");
            dt.Columns.Add("G.");
            dt.Columns.Add("Nº");
            dt.Columns.Add("c.v.");
            AgregarFilas(dt, 27,"TABLA1");
            return dt;
        }

        public DataTable CrearTabla2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("POS.");
            dt.Columns.Add("G.");
            dt.Columns.Add("Nº");
            dt.Columns.Add("SOLO");
            dt.Columns.Add("OCULTO");
            AgregarFilas(dt, 27,"TABLA2");
            return dt;
        }

        private  DataTable AgregarFilas(DataTable dt, int nF , string tabla)
        {
            for (int i = 1; i <= nF; i++)
            {
                if (tabla == "TABLA2")
                {
                    if (i >= 1 && i<= 9)
                        dt.Rows.Add(i, "F", i , "");
                    else if (i >= 10 && i <= 18)
                        dt.Rows.Add(i, "C", i - 9, "");
                    else if (i >= 19 && i <= 27)
                        dt.Rows.Add(i, "R", i - 18, "");
                }
                else
                {
                    dt.Rows.Add(i, "", "", "");
                }
            }
            return dt;
        }

        // FORMATO DATAGRIDVIEW
        public DataGridView FormatoDataGridView1(DataGridView dgv)
        {
            //dataGridView1.AutoGenerateColumns = EngineData.Falso;
            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 50;
            dgv.Columns[3].Width = 50;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ClearSelection();
            return dgv;
        }

        public DataGridView FormatoDataGridView2(DataGridView dgv)
        {
            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 50;
            dgv.Columns[3].Width = 50;
            dgv.Columns[4].Width = 80;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ClearSelection();
            return dgv;
        }






    }
}
