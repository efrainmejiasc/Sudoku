using Microsoft.Win32;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace SudokuParaTodos
{
    public class EngineSudoku
    {
        private EngineData Valor = EngineData.Instance();
        private int recuadro = -1;

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
            return v;
        }

        public Button [] ColoresPincel2(Button[] v)
        {
            v[0].BackColor = Color.Silver;
            v[1].BackColor = Color.PaleGreen;
            v[2].BackColor = Color.Green;
            v[3].BackColor = Color.LightSalmon;
            v[4].BackColor = Color.Orange;
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

        public TextBox [,] SetearTextBoxJuego(TextBox[,] cajaTexto, string[,] vArray ,float fontBig = 0, float fontSmall = 0 )
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].Text = vArray[f,c];
                    if (vArray[f, c].Length == 1)
                    {
                        cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontBig);
                        cajaTexto[f, c].ForeColor = Color.Green;
                    }
                    else
                    {
                        cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontSmall);
                        cajaTexto[f, c].ForeColor = Color.Blue;
                    }
                    cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
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

        ///
        public string[,] CandidatosSinEliminados(string[,] valorEliminado, string[,] valorCandidato)
        {
            ListBox candidatosOrganizados = new ListBox();
            ListBox eliminarOrganizados = new ListBox();
            string[,] valorCandidatoSinEliminados = new string[9,9];
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorEliminado[f, c] != null && valorEliminado[f, c] != string.Empty)
                    {
                        candidatosOrganizados.Items.Clear();
                        eliminarOrganizados.Items.Clear();
                        candidatosOrganizados = OrganizarCandidatos(candidatosOrganizados, f, c, valorCandidato);
                        eliminarOrganizados = OrganizarLista(eliminarOrganizados, valorEliminado[f, c]);
                        candidatosOrganizados = QuitarEliminados(candidatosOrganizados, eliminarOrganizados);
                        valorCandidatoSinEliminados = EscribirCandidatosSinEliminados(candidatosOrganizados, f, c,valorCandidatoSinEliminados); 
                       
                    }
                }
            }
            return valorCandidatoSinEliminados;
        }

        private ListBox OrganizarCandidatos(ListBox lista, int f, int c, string[,] valorCandidato)
        {
            ListBox listaAux = new ListBox();
            string k = valorCandidato[f, c];
            k = k.Trim();
            string[] item = k.Split(' ');
            for (int i = 0; i <= item.Length - 1; i++) { listaAux.Items.Add(item[i].Trim()); }
            foreach (String n in listaAux.Items)
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

        private ListBox OrganizarLista(ListBox lista, string cadena)
        {
            ListBox listaAux = new ListBox();
            cadena = cadena.Trim();
            string[] item = cadena.Split(' ');
            for (int i = 0; i <= item.Length - 1; i++) { listaAux.Items.Add(item[i].Trim()); }
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

        private string[,] EscribirCandidatosSinEliminados(ListBox candidatosFinal, int f, int c,string[,] valorCandidatoSinEliminados)
        {
            if (candidatosFinal.Items.Count > 0)
            {
                string valor = string.Empty;
                int indic = 1;
                foreach (string v in candidatosFinal.Items)
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

    }
}
