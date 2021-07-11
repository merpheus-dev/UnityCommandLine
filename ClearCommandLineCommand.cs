using System;

namespace Subtegral.CommandLine
{
    [Serializable]
    public class ClearCommandLineCommand : Command
    {
        public override string Key { get; protected set; } = "clear";
        public override string UserInput { get; set; }
        public override void Execute()
        {
            UnityCommandLine.ClearCommandLine();
        }

        public ClearCommandLineCommand(string command) : base(command)
        {
        
        }
    }
}