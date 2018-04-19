using Microsoft.Win32;
using System;

namespace SudokuParaTodos
{
    public class EngineSudoku
    {
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


    }
}
