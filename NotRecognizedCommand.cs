using System;

namespace Subtegral.CommandLine
{
    [Serializable]
    public class NotRecognizedCommand : Command
    {
        public override string Key { get; protected set; } = "askfamliqwjwnkew";
        public override string UserInput { get; set; } = "[!] Command Not Recognized: ";
        public override void Execute()
        {
        }

        public NotRecognizedCommand(string command) : base(command)
        {
            UserInput += command;
        }
    }
}