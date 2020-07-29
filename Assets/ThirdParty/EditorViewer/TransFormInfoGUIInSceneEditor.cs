using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor.DebugMode
{
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class TransFormInfoGUIInSceneEditor : TransformEditor
    {
        private static bool _initFlag;
        private static readonly List<GameObject> GameObjects = new List<GameObject>();
        private static readonly Dictionary<int, WindowData> Actives = new Dictionary<int, WindowData>();

        public override void OnEnable()
        {
            base.OnEnable();

            OnSceneCompleted();

#if UNITY_2019_1_OR_NEWER
            SceneView.onSceneGUIDelegate += OnSceneGUIRefresh;
            Selection.selectionChanged = OnSceneSelectedItem;
#else
            SceneView.onSceneGUIDelegate += OnSceneGUIRefresh;
            Selection.selectionChanged = OnSceneSelectedItem;
#endif
        }

        private void OnDisable()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.onSceneGUIDelegate -= OnSceneGUIRefresh;
            Selection.selectionChanged = null;
#else
            SceneView.onSceneGUIDelegate -= OnSceneGUIRefresh;
            Selection.selectionChanged = null;
#endif
        }

        private void OnSceneCompleted()
        {
            if (!_initFlag)
            {
                return;
            }

            _initFlag = true;
            SceneManager.GetActiveScene()
                .GetRootGameObjects(GameObjects);
        }

        private static void OnSceneSelectedItem()
        {
            if (!Selection.activeTransform)
            {
                return;
            }

            TransformsBfsSearch();
        }

        private static void TransformsBfsSearch()
        {
            foreach (var topLayerTransform in Selection.transforms)
            {
                var queue = new Queue<Transform>();
                queue.Enqueue(topLayerTransform);

                while (queue.Count > 0)
                {
                    var child = queue.Dequeue();

                    TransformDataPreparation(child);

                    foreach (Transform transform in child)
                    {
                        queue.Enqueue(transform);
                    }
                }
            }
        }

        private static void TransformDataPreparation(Transform transform)
        {
            var id = transform.GetInstanceID();
            if (Actives.ContainsKey(id))
            {
                Actives[id].Active = true;
            }

            var gameObject = transform.gameObject;
            if (!GameObjects.Contains(gameObject))
            {
                GameObjects.Add(gameObject);
            }
        }

        private static void OnSceneGUIRefresh(SceneView view)
        {
            if (Selection.activeTransform == null)
            {
                return;
            }

            var pool = GameObjects;
            for (var i = 0; i < pool.Count; ++i)
            {
                if (pool[i] == null)
                {
                    GameObjects.Remove(pool[i]);
                    continue;
                }

                var transform = pool[i].transform;
                DisplayGameObjectNames(transform);
            }
        }

        private static void DisplayGameObjectNames(Transform transform)
        {
            var transformId = transform.GetInstanceID();

            // Gather game object's name
            var gameObjectNames = new Stack<string>();
            var transformTemp = transform;
            while (transformTemp.parent != null)
            {
                gameObjectNames.Push(transformTemp.name);
                transformTemp = transformTemp.parent;
            }

            gameObjectNames.Push(transformTemp.name);

            // Render the names by order
            var guiPosition = HandleUtility.WorldToGUIPoint(transform.position);
            var height = Mathf.Min(50 + gameObjectNames.Count * 25, 160);
            var windowSize = new Rect(guiPosition.x, guiPosition.y, 150, height);

            if (!Actives.ContainsKey(transformId))
            {
                Actives.Add(transformId,
                    new WindowData(Vector2.up * int.MaxValue, true, windowSize));
            }

            // Display GUI
            if (Actives[transformId].Active)
            {
                Actives[transformId].WindowSize = GUI.Window(transformId, Actives[transformId].WindowSize, id =>
                {
                    if (gameObjectNames.Count > 5)
                    {
                        using (var scrollViewScope = new GUILayout.ScrollViewScope(
                            Actives[transformId].ScrollPosition,
                            GUILayout.Width(140),
                            GUILayout.Height(100)))
                        {
                            Actives[transformId].ScrollPosition = scrollViewScope.scrollPosition;
                            DisplayGameObjectNames(gameObjectNames);
                        }
                    }
                    else
                    {
                        DisplayGameObjectNames(gameObjectNames);
                    }

                    GUILayout.Space(5);

                    DisplayCloseButton(transform, windowSize);

                    GUI.DragWindow();
                }, transform.name);
            }
        }

        private static void DisplayGameObjectNames(IEnumerable<string> gameObjectNames)
        {
            var guiStyle = new GUIStyle(GUI.skin.textField);
            guiStyle.alignment = TextAnchor.MiddleCenter;

            var cnt = 0;
            var maxCount = gameObjectNames.Count();
            foreach (var gameObjectName in gameObjectNames)
            {
                if (cnt == maxCount - 1)
                {
                    guiStyle.normal.textColor = Color.red;
                }

                if (GUILayout.Button(gameObjectName, guiStyle))
                {
                    CopyToClipboard(gameObjectName);
                }

                cnt++;
            }
        }

        private static void CopyToClipboard(string text)
        {
            var textEditor = new TextEditor();
            textEditor.text = text;
            textEditor.SelectAll();
            textEditor.Copy();
        }

        private static void DisplayCloseButton(Transform transform, Rect windowSize)
        {
            if (GUILayout.Button("Close"))
            {
                Actives[transform.GetInstanceID()].Active = false;
                Actives[transform.GetInstanceID()].WindowSize = windowSize;

                GameObjects.Remove(transform.gameObject);

                // Debug.Log("close");

                if (Selection.transforms.Contains(transform))
                {
                    // Debug.Log("close current");
                    if (GameObjects.Count.Equals(0))
                    {
                        Selection.SetActiveObjectWithContext(null, null);
                        return;
                    }

                    var gameObject = GameObjects[0];
                    Selection.SetActiveObjectWithContext(gameObject, gameObject);
                }
            }
        }

        private class WindowData
        {
            public bool Active { get; set; }
            public Vector2 ScrollPosition { get; set; }
            public Rect WindowSize { get; set; }

            public WindowData(Vector2 scrollPosition, bool active, Rect windowSize)
            {
                ScrollPosition = scrollPosition;
                Active = active;
                WindowSize = windowSize;
            }
        }
    }
}