using System.Collections.Generic;
using System.Linq;
using Editor.SceneViewEditor.Source.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Editor.SceneViewEditor.Source.Customs
{
    public class CustomWindowHandler
    {
        private readonly Camera _camera;
        private readonly List<IWindow> _customWindows = new List<IWindow>();
        private readonly Vector2 _defaultWindowSize = new Vector2(150, 160);
        private readonly CustomStyles _customStyles;

        public CustomWindowHandler(Camera camera, CustomStyles customStyles)
        {
            _camera = camera;
            _customStyles = customStyles;
        }

        public Vector2 WorldToScreenPoint(Vector2 position)
        {
            return _camera.WorldToScreenPoint(position);
        }

        public IWindow GetNextHandleWindow(IWindow ignoreWindow)
        {
            return _customWindows.FindLast(window => window != ignoreWindow &&
                                                     window.IsActive &&
                                                     !window.IsDataEmpty);
        }

        public void OnSceneSelectedItem()
        {
            var transforms = Selection.GetTransforms(SelectionMode.Unfiltered);
            for (var i = 0; i < transforms.Length; i++)
            {
                var id = transforms[i].GetInstanceID();
                var target = _customWindows.Find(window => window.Id == id);

                if (target == null)
                {
                    CreateCustomWindow(transforms[i]);
                    continue;
                }

                target.IsActive = true;
            }

            Selection.SetActiveObjectWithContext(transforms.LastOrDefault(), null);
        }

        private void CreateCustomWindow(Transform transform)
        {
            var position = WorldToScreenPoint(transform.position);
            var windowSize = new Rect(position, _defaultWindowSize);
            var initScrollPosition = int.MaxValue * Vector2.up;
            var customWindowSettings = new CustomWindow.Settings(true,
                windowSize, initScrollPosition, transform);

            var customWindow = new CustomWindow(this, customWindowSettings, _customStyles);
            _customWindows.Add(customWindow);
        }

        public void OnSceneGUIUpdate()
        {
            if (Event.current.type == EventType.Layout)
            {
                for (var i = 0; i < _customWindows.Count; i++)
                {
                    if (_customWindows[i].IsDataEmpty)
                    {
                        _customWindows.RemoveAt(i);
                        continue;
                    }

                    _customWindows[i].Display();
                }
            }
        }
    }
}