using System;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace UnityCompileInBackground.Editor
{
    public static class UnityCompileInBackground
    {
        private const string ConsoleAppPath = @"UnityCompileInBackground/Editor/UnityCompileInBackground_Watch.exe";
        private static Process _process;
        private static bool _isRefresh;
        private static PlayModeStateChange _playModeState;

        [InitializeOnLoadMethod]
        private static void Init()
        {
#if UNITY_EDITOR_OSX
            return;
#endif
#if UNITY_EDITOR
            if (EditorApplication.isPlaying) return;

            InitialProcess();

            UnityEngine.Debug.Log("[UnityCompileInBackground] Start Watching");
            EditorApplication.update += OnUpdate;
            EditorApplication.playModeStateChanged += ModeStateChanged;
            CompilationPipeline.assemblyCompilationStarted += OnCompilationStarted;
#endif
        }

        private static void InitialProcess()
        {
            if (_process != null)
                return;
            var pid = Process.GetCurrentProcess().Id;
            var dataPath = Application.dataPath;
            var filename = dataPath + "/" + ConsoleAppPath;
            var path = Application.dataPath;
            var arguments = string.Format(@"-p ""{0}"" -w 0 -d ""{1}""", path, pid);
            var windowStyle = ProcessWindowStyle.Hidden;
            var info = new ProcessStartInfo
            {
                FileName = filename,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WindowStyle = windowStyle,
                Arguments = arguments,
            };

            _process = Process.Start(info);
            _process.OutputDataReceived += OnReceived;
            _process.BeginOutputReadLine();
        }

        private static void OnCompilationStarted(string _)
        {
            Dispose();
        }

        private static void Dispose()
        {
            if (_process == null) return;

            if (!_process.HasExited)
            {
                _process.Kill();
            }
            _process.Dispose();
            _process = null;

            UnityEngine.Debug.Log("[UnityCompileInBackground] Stop Watching");
        }

        private static void OnUpdate()
        {
            if (!_isRefresh) return;
            if (EditorApplication.isCompiling) return;
            if (EditorApplication.isUpdating) return;
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            UnityEngine.Debug.Log("[UnityCompileInBackground] Start Compiling");
            _isRefresh = false;
            AssetDatabase.Refresh();
        }

        private static void ModeStateChanged(PlayModeStateChange playmode)
        {
            _playModeState = playmode;
            if (playmode == PlayModeStateChange.ExitingEditMode || _playModeState == PlayModeStateChange.EnteredPlayMode)
            {
                _isRefresh = false;
                Dispose();
            }
            if (playmode == PlayModeStateChange.EnteredEditMode)
            {
                _isRefresh = false;
                InitialProcess();
            }
        }

        private static void OnReceived(object sender, DataReceivedEventArgs e)
        {
            var message = e.Data;
            if (message.Contains("OnChanged") || message.Contains("OnRenamed"))
                _isRefresh = true;
            if (message.Contains("ParentIsDie"))
            {
                _isRefresh = false;
                Dispose();
            }
        }
    }
}