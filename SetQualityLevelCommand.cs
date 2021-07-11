using UnityEngine;

namespace Subtegral.CommandLine
{
    public class SetQualityLevelCommand : Command
    {
        public SetQualityLevelCommand(string command) : base(command)
        {
            UserInput = command;
        }

        public override string Key { get; protected set; } = "SetQuality";
        public override string UserInput { get; set; }
        private int index;

        public override void Execute()
        {
            if (!UserInput.Contains(" "))
            {
                UserInput = "Specify Quality Level";
                return;
            }

            var splitResult = UserInput.Split(' ');
            if (!int.TryParse(splitResult[1], out index))
            {
                UserInput = "Invalid Quality Level:" + UserInput.Split(' ')[1];
            }

            QualitySettings.SetQualityLevel(index);
        }
    }
}