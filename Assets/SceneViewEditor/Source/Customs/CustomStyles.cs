using UnityEngine;

namespace Editor.SceneViewEditor.Source.Customs
{
    public class CustomStyles
    {
        public readonly GUIStyle CloseButtonGUIStyle;
        public readonly GUIStyle EditableTextGUIStyle;
        public readonly GUIStyle LabelTextGUIStyle;
        public readonly GUIStyle WindowBoxGUIStyle;
        public readonly GUIStyle VerticalScrollbarGUIStyle;
        public readonly GUIStyle HorizontalScrollbarGUIStyle;
        public readonly GUIStyle ScrollViewBackGroundGUIStyle;

        public CustomStyles()
        {
            Debug.Log("Custom GUI Style Setup.");
            
            CloseButtonGUIStyle = new GUIStyle(GUI.skin.button);
            CloseButtonGUIStyleSetup();

            EditableTextGUIStyle = new GUIStyle(GUI.skin.textField);
            EditableTextGUIStyleSetup();

            LabelTextGUIStyle = new GUIStyle(GUI.skin.textField);
            LabelGUIStyleSetup();

            WindowBoxGUIStyle = new GUIStyle(GUI.skin.window);
            WindowBoxGUIStyleSetup();

            VerticalScrollbarGUIStyle = new GUIStyle(GUI.skin.verticalScrollbar);
            HorizontalScrollbarGUIStyle = new GUIStyle(GUI.skin.horizontalScrollbar);
            ScrollViewBackGroundGUIStyle = new GUIStyle(GUI.skin.scrollView);
            ScrollViewGUIStyleSetup();
        }
        
        private void ScrollViewGUIStyleSetup()
        {
            // Warning: This will change the global scrollbar GUI style in the editor.
            GUI.skin.verticalScrollbarUpButton = GUIStyle.none;
            GUI.skin.verticalScrollbarDownButton = GUIStyle.none;
            GUI.skin.horizontalScrollbarLeftButton = GUIStyle.none;
            GUI.skin.horizontalScrollbarRightButton = GUIStyle.none;

            VerticalScrollbarGUIStyle.margin = new RectOffset(5, 5, 0, 0);
            VerticalScrollbarGUIStyle.padding = new RectOffset(0, 0, 0, 0);
            VerticalScrollbarGUIStyle.stretchHeight = true;
            VerticalScrollbarGUIStyle.fixedWidth = 10;
            VerticalScrollbarGUIStyle.normal.background = MakeTex(1, 1, new Color32(25, 38, 50, 255));

            HorizontalScrollbarGUIStyle.margin = new RectOffset(0, 0, 5, 5);
            HorizontalScrollbarGUIStyle.padding = new RectOffset(0, 0, 0, 0);
            HorizontalScrollbarGUIStyle.stretchWidth = true;
            HorizontalScrollbarGUIStyle.fixedHeight = 10;
            HorizontalScrollbarGUIStyle.normal.background = MakeTex(1, 1, new Color32(25, 38, 50, 255));

            ScrollViewBackGroundGUIStyle.margin = new RectOffset(5, 5, 5, 5);
            ScrollViewBackGroundGUIStyle.stretchWidth = true;
            ScrollViewBackGroundGUIStyle.stretchHeight = true;
        }

        private void CloseButtonGUIStyleSetup()
        {
            CloseButtonGUIStyle.alignment = TextAnchor.MiddleCenter;
            CloseButtonGUIStyle.fontStyle = FontStyle.Bold;
            CloseButtonGUIStyle.normal = new GUIStyleState
            {
                textColor = new Color(255, 255, 255, 255),
                background = MakeTex(1, 1, new Color32(244, 76, 79, 255))
            };
            CloseButtonGUIStyle.hover = new GUIStyleState
            {
                textColor = new Color(255, 255, 255, 125),
                background = MakeTex(1, 1, new Color32(244, 96, 117, 255))
            };
        }

        private void EditableTextGUIStyleSetup()
        {
            EditableTextGUIStyle.alignment = TextAnchor.MiddleLeft;
            EditableTextGUIStyle.fontStyle = FontStyle.Bold;
            EditableTextGUIStyle.normal.background = MakeTex(1, 1, new Color32(242, 244, 244, 255));
            EditableTextGUIStyle.normal.textColor = new Color32(35, 27, 30, 255);
        }

        private void LabelGUIStyleSetup()
        {
            LabelTextGUIStyle.alignment = TextAnchor.MiddleLeft;
            LabelTextGUIStyle.fontStyle = FontStyle.Bold;
            LabelTextGUIStyle.normal.background = MakeTex(1, 1, new Color32(0, 0, 0, 125));
            LabelTextGUIStyle.normal.textColor = new Color32(161, 148, 149, 225);
        }

        private void WindowBoxGUIStyleSetup()
        {
            WindowBoxGUIStyle.onNormal.background = MakeTex(1, 1, new Color32(61, 88, 112, 255));
            WindowBoxGUIStyle.normal.background = MakeTex(1, 1, new Color32(31, 54, 68, 255));
        }

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            var pix = new Color[width * height];

            for (var i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }

            var result = new Texture2D(width, height);
            result.hideFlags = HideFlags.HideAndDontSave;
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }
}