using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Subtegral.CommandLine
{
    public class UnityCommandLine : EditorWindow
    {
        private Vector2 scrollViewPos;
        private string currentCommand;
        private static Dictionary<string, Command> commandDict;
        private List<Command> commands;
        private static UnityCommandLine Instance;

        [MenuItem("Window/Command Line")]
        public static void ShowWindow()
        {
            if (Instance != null)
            {
                Instance.Close();
                Instance = null;
            }
            Instance = GetWindow<UnityCommandLine>("Command Line");
            ScanCommandScripts();
            Instance.Show();
        }

        private static void ScanCommandScripts()
        {
            commandDict = new Dictionary<string, Command>();
            var types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(Command));
            for (int i = 0; i < types.Length; i++)
            {
                var targetType = types[i];
                var ctor = targetType.GetConstructors()[0];
                var instance = ctor.Invoke(new object[] {string.Empty});
                var commandKey = targetType.GetProperty("Key").GetValue(instance);
                commandDict.Add((string) commandKey, (Command) instance);
            }
        }

        public static void ClearCommandLine()
        {
            Instance.commands.Clear();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            if (Instance == null) Instance = this;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginScrollView(scrollViewPos);
            if (commands == null) commands = new List<Command>();
            for (int i = 0; i < commands.Count; i++)
            {
                //var style = BackgroundStyle.Get(i % 2 == 0 ? Color.white : Color.gray);
                EditorGUILayout.LabelField(commands[i].UserInput);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.BeginHorizontal();
            currentCommand = EditorGUILayout.TextField("CMD:", currentCommand);
            var e = Event.current;
            if (!string.IsNullOrWhiteSpace(currentCommand) && e.type == EventType.Used)
            {
                switch (e.keyCode)
                {
                    // Escape pressed
                    case KeyCode.Escape:
                        currentCommand = string.Empty;
                        break;

                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                        var resolvedCommand = ResolveAndInstantiateCommand(currentCommand);
                        commands.Add(resolvedCommand);
                        resolvedCommand.Execute();
                        currentCommand = string.Empty;
                        break;
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private Command ResolveAndInstantiateCommand(string commandInput)
        {
            if (commandDict == null) ScanCommandScripts();
            foreach (var commandDictKey in commandDict.Keys)
            {
                if (commandInput.Contains(commandDictKey))
                {
                    var ctor = commandDict[commandDictKey].GetType().GetConstructors()[0];
                    var instance = ctor.Invoke(new object[] {commandInput});
                    return (Command) instance;
                }
            }

            return new NotRecognizedCommand(commandInput);
        }
    }
}