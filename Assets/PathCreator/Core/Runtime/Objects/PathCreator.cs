﻿using UnityEngine;
using ThirdParty.Extensions;

namespace PathCreation
{
    public class PathCreator : MonoBehaviour
    {

        /// This class stores data for the path editor, and provides accessors to get the current vertex and bezier path.
        /// Attach to a GameObject to create a new path editor.

        public event System.Action pathUpdated;

        [SerializeField, HideInInspector]
        PathCreatorData editorData;
        [SerializeField, HideInInspector]
        bool initialized;

        [SerializeField]
        private bool _isDrawPathWhenSelected = true;

        GlobalDisplaySettings globalEditorDisplaySettings;

        [SerializeField]
        private bool _isAllowShowPath = true;

        // Vertex path created from the current bezier path
        public VertexPath path
        {
            get
            {
                if (!initialized)
                {
                    InitializeEditorData(false);
                }
                return editorData.GetVertexPath(transform);
            }
        }

        // The bezier path created in the editor
        public BezierPath bezierPath
        {
            get
            {
                if (!initialized)
                {
                    InitializeEditorData(false);
                }
                return editorData.bezierPath;
            }
            set
            {
                if (!initialized)
                {
                    InitializeEditorData(false);
                }
                editorData.bezierPath = value;
            }
        }

        #region Internal methods

        /// Used by the path editor to initialise some data
        public void InitializeEditorData(bool in2DMode)
        {
            if (editorData == null)
            {
                editorData = new PathCreatorData();
            }
            editorData.bezierOrVertexPathModified -= TriggerPathUpdate;
            editorData.bezierOrVertexPathModified += TriggerPathUpdate;

            editorData.Initialize(in2DMode);
            initialized = true;
        }

        public PathCreatorData EditorData
        {
            get
            {
                return editorData;
            }

        }

        public void TriggerPathUpdate()
        {
            if (pathUpdated != null)
            {
                pathUpdated();
            }
        }

#if UNITY_EDITOR

        private void Awake()
        {
            if (globalEditorDisplaySettings == null)
            {
                globalEditorDisplaySettings = GlobalDisplaySettings.Load();
            }
        }

        // Draw the path when path objected is not selected (if enabled in settings)
        void OnDrawGizmos()
        {

            // Only draw path gizmo if the path object is not selected
            // (editor script is resposible for drawing when selected)

            if (!_isAllowShowPath)
            {
                return;
            }

            GameObject selectedObj = UnityEditor.Selection.activeGameObject;

            if (path != null)
            {
                path.UpdateTransform(transform);

                if (globalEditorDisplaySettings == null)
                {
                    globalEditorDisplaySettings = GlobalDisplaySettings.Load();
                }

                if (globalEditorDisplaySettings.visibleWhenNotSelected)
                {

                    Gizmos.color = globalEditorDisplaySettings.bezierPath;

                    for (int i = 0; i < path.NumPoints; i++)
                    {
                        int nextI = i + 1;
                        if (nextI >= path.NumPoints)
                        {
                            if (path.isClosedLoop)
                            {
                                nextI %= path.NumPoints;
                            }
                            else
                            {
                                break;
                            }
                        }

                        // Gizmos.DrawLine (path.GetPoint (i), path.GetPoint (nextI));
                        GizmosExtension.DrawLine(path.GetPoint(i), path.GetPoint(nextI),
                        globalEditorDisplaySettings.lineWidth);
                    }
                }
            }

        }


#endif

        #endregion
    }
}