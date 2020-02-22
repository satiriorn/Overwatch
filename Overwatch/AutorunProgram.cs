namespace Overwatch
{
    class AutorunProgram
    {
        public string RegName { get; set; } // The name of the item in the registry key

        public string RegValue { get; set; }// The value of the item in the registry key may contain the path to the file and startup arguments

        public string RunFilePath { get; set; }//The path to the file

        public string RunArguments { get; set; }// File Launch Arguments

        public bool IsFileExists { get; set; } = false;// Is there a file

        public bool IsActiveProcess { get { return cProcesses.IsFileActiveProcess(RunFilePath); } }// Is file process running

        public RegistryLocation RegLocation { get; set; }// The base registry key in which this item resides

        public enum RegistryLocation { LocalMachine, CurrentUser };
    }
}
