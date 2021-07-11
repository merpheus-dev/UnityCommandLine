using System.Linq;
using UnityEditor.SceneManagement;

namespace Subtegral.CommandLine
{
    public class OpenNewSceneCommand : Command
    {
        public OpenNewSceneCommand(string command) : base(command)
        {
            UserInput = command;
        }

        public override string Key { get; protected set; } = "new scene";
        public override string UserInput { get; set; }

        public override void Execute()
        {
            var spaceCount = UserInput.Count(x => x == ' ');
            if (spaceCount < 2)
            {
                UserInput = "Invalid Parameter";
                return;
            }

            var splitResult = UserInput.Split(' ');
            NewSceneSetup sceneSetup = NewSceneSetup.EmptyScene;
            NewSceneMode sceneMode = NewSceneMode.Single;
            if (spaceCount == 2)
            {
                if (splitResult[2].Contains("empty"))
                {
                    sceneSetup = NewSceneSetup.EmptyScene;
                }
                else if (splitResult[2].Contains("default"))
                {
                    sceneSetup = NewSceneSetup.DefaultGameObjects;
                }
                else
                {
                    PrintInvalidSyntax(1);
                    return;
                }
            }
            else if (spaceCount == 3)
            {
                if (splitResult[2].Contains("empty"))
                {
                    sceneSetup = NewSceneSetup.EmptyScene;
                }
                else if (splitResult[2].Contains("default"))
                {
                    sceneSetup = NewSceneSetup.DefaultGameObjects;
                }
                else
                {
                    PrintInvalidSyntax(2);
                    return;
                }
            
                if (splitResult[3].Contains("single"))
                {
                    sceneMode = NewSceneMode.Single;
                }
                else if (splitResult[3].Contains("additive"))
                {
                    sceneMode = NewSceneMode.Additive;
                }
                else
                {
                    PrintInvalidSyntax(2);
                    return;
                }

            }
            else
            {
                PrintInvalidSyntax(2);
                return;
            }

            EditorSceneManager.NewScene(sceneSetup, sceneMode);
        }

        private void PrintInvalidSyntax(int parameterCount)
        {
            UserInput =
                "[!] Invalid Syntax, Try 'new scene [sceneSetup(empty/default)]";
            if(parameterCount==2) UserInput+="[sceneMode(single/additive)], second parameter is optional";
        }
    }
}