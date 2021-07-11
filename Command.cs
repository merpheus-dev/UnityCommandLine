using System;

namespace Subtegral.CommandLine
{
    [Serializable]
    public abstract class Command
    {
        /// <summary>
        /// Do not implement any logic here!! This gets executed on scan process.
        /// </summary>
        public Command(string command)
        {
        }

        public abstract string Key { get; protected set; }
        public abstract string UserInput { get; set; }
        public abstract void Execute();
    }
}