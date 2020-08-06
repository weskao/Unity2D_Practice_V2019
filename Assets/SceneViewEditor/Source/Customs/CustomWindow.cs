using Editor.SceneViewEditor.Source.Extensions;
using Editor.SceneViewEditor.Source.Interfaces;
using UnityEditor;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Editor.SceneViewEditor.Source.Customs
{
    public class CustomWindow : IWindow
    {
        public int Id => _settings.Id;
        public bool IsDataEmpty => Transform == null;
        public Transform Transform => _settings.Transform;
        public bool IsActive
        {
            get
            {
                return _settings.IsActive;
            }
            set
            {
                _settings.IsActive = value;
            }
        }

        private static bool _isCloseWindowExecuted;
        private static bool _isEscapeKeyPressed;
        private readonly CustomWindowHandler _handler;
        private readonly Settings _settings;
        private readonly CustomStyles _customStyles;

        public CustomWindow(CustomWindowHandler handler, Settings settings, CustomStyles customStyles)
        {
            _handler = handler;
            _settings = settings;
            _customStyles = customStyles;
        }

        public void Display()
        {
            if (!_settings.IsActive)
            {
                return;
            }

            _settings.WindowSize = GUI.Window(_settings.Id,
                _settings.WindowSize,
                WindowCallBackFunction,
                "",
                _customStyles.WindowBoxGUIStyle);
        }

        public void Close()
        {
            var transform = _settings.Transform;
            var position = _handler.WorldToScreenPoint(transform.position);

            _settings.IsActive = false;
            _settings.WindowSize = new Rect(position, _settings.WindowSize.size);

            var window = _handler.GetNextHandleWindow(this);
            Selection.SetActiveObjectWithContext(window?.Transform, null);
        }

        private void WindowCallBackFunction(int transformId)
        {
            // The execute order is important.

            // Handle Window Event
            HandleWindowEvents();
            HandleFocusedWindowEvents(transformId);

            // Layout
            WindowGUILayout();

            // Key pressed Update
            UpdateEscapeKey();
        }

        private void HandleWindowEvents()
        {
            // When the window is clicked, the selection would be set to the gameObject's transform.
            if (Event.current.type == EventType.MouseDown)
            {
                Selection.SetActiveObjectWithContext(_settings.Transform, null);
            }
        }

        private void HandleFocusedWindowEvents(int transformId)
        {
            var isFocused = Selection.activeTransform != null &&
                            Selection.activeTransform.GetInstanceID() == transformId;
            if (!isFocused)
            {
                return;
            }

            GUI.FocusWindow(transformId);
            GUI.BringWindowToFront(transformId);

            UseEscapeKeyCloseWindow();
        }

        private void UseEscapeKeyCloseWindow()
        {
            if (!_isEscapeKeyPressed || _isCloseWindowExecuted)
            {
                if (!_isEscapeKeyPressed)
                {
                    _isCloseWindowExecuted = false;
                }

                return;
            }

            _isCloseWindowExecuted = true;

            Close();
        }

        private void WindowGUILayout()
        {
            using (new GUILayout.AreaScope(new Rect(130, 0, 20, 20)))
            {
                DisplayCloseButton();
            }

            GUILayout.Space(5);

            using (var scrollViewScope = new GUILayout.ScrollViewScope(
                _settings.ScrollPosition,
                false,
                false,
                _customStyles.HorizontalScrollbarGUIStyle,
                _customStyles.VerticalScrollbarGUIStyle,
                _customStyles.ScrollViewBackGroundGUIStyle))
            {
                _settings.ScrollPosition = scrollViewScope.scrollPosition;
                DisplayScrollViewContent();
            }

            GUI.DragWindow();
        }

        private void DisplayScrollViewContent()
        {
            var transforms = _settings.Transform.GetAllParentsAndSelf();
            for (var i = transforms.Count - 1; i >= 0; i--)
            {
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("✄", GUILayout.Width(20)))
                {
                    transforms[i].name.CopyToClipboard();
                    Selection.SetActiveObjectWithContext(transforms[i], null);
                    GUI.FocusWindow(transforms[i].GetInstanceID());

                    Debug.Log($"[#{transforms[i].name}] copy success!");
                }

                if (i == 0)
                {
                    transforms[i].name =
                        GUILayout.TextField(transforms[i].name, _customStyles.EditableTextGUIStyle);
                }
                else
                {
                    GUILayout.Label(transforms[i].name, _customStyles.LabelTextGUIStyle);
                }

                GUILayout.EndHorizontal();
            }
        }

        private void DisplayCloseButton()
        {
            if (GUILayout.Button("X", _customStyles.CloseButtonGUIStyle))
            {
                Close();
            }
        }

        private static void UpdateEscapeKey()
        {
#if ENABLE_INPUT_SYSTEM
            _isEscapeKeyPressed = Keyboard.current.escapeKey.isPressed;
#else
            _isEscapeKeyPressed = Input.GetKey(KeyCode.Escape);
#endif
        }


        public class Settings
        {
            public int Id => Transform.GetInstanceID();
            public bool IsActive { get; set; }
            public Vector2 ScrollPosition { get; set; }
            public Rect WindowSize { get; set; }
            public Transform Transform { get; }

            public Settings(bool isActive, Rect windowSize, Vector2 scrollPosition, Transform transform)
            {
                IsActive = isActive;
                WindowSize = windowSize;
                ScrollPosition = scrollPosition;
                Transform = transform;
            }
        }
    }
}