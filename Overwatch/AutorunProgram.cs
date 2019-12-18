namespace Overwatch
{
    class AutorunProgram
    {
        public string RegName { get; set; } // Название элемента в разделе реестра

        public string RegValue { get; set; }// Значение элемента в разделе реестра, может содержать путь к файлу и аргументы запуска

        public string RunFilePath { get; set; }// Путь к файлу

        public string RunArguments { get; set; }// Аргументы запуска файла

        public bool IsFileExists { get; set; } = false;// Существует ли файл

        public bool IsActiveProcess { get { return cProcesses.IsFileActiveProcess(RunFilePath); } }// Запущен ли процесс файл

        public RegistryLocation RegLocation { get; set; }// Базовый раздел реестра в котором находится этот элемент

        public enum RegistryLocation { LocalMachine, CurrentUser };
    }
}
