using System.Linq;
using Editor.SceneViewEditor.Source.Customs;
using UnityEditor;

namespace Editor.SceneViewEditor.Source
{
    public class SceneViewEditor
    {
        public enum State
        {
            OnEnable,
            OnDisable,
            None
        }

        public State CurrentState { get; private set; } = State.None;
        private CustomWindowHandler _customWindowHandler;

        public void OnEnable()
        {
            CurrentState = State.OnEnable;

            Selection.selectionChanged = OnSceneSelectedItem;
            SceneView.onSceneGUIDelegate += OnGUIInitialize;
            SceneView.onSceneGUIDelegate += OnSceneGUIUpdate;
            SceneView.RepaintAll();
        }

        public void OnDisable()
        {
            CurrentState = State.OnDisable;

            Selection.selectionChanged = null;
            SceneView.onSceneGUIDelegate -= OnGUIInitialize;
            SceneView.onSceneGUIDelegate -= OnSceneGUIUpdate;
            SceneView.RepaintAll();
        }

        private void OnGUIInitialize(SceneView _)
        {
            var camera = SceneView.GetAllSceneCameras().FirstOrDefault();
            // TODO: CustomStyles Factory
            var customStyles = new CustomStyles();
            _customWindowHandler = new CustomWindowHandler(camera, customStyles);

            SceneView.onSceneGUIDelegate -= OnGUIInitialize;
        }

        private void OnSceneSelectedItem()
        {
            _customWindowHandler.OnSceneSelectedItem();
        }

        private void OnSceneGUIUpdate(SceneView _)
        {
            _customWindowHandler.OnSceneGUIUpdate();
        }
    }
}