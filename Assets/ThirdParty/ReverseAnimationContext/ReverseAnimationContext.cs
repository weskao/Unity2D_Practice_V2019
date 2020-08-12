using UnityEditor;
using UnityEngine;
using System.IO;

namespace ThirdParty
{
    public static class ReverseAnimationContext
    {
        [MenuItem("Assets/Create Reversed Clip", false, 14)]
        private static void ReverseClip()
        {
            string directoryPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeObject));
            string fileName = Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject));
            string fileExtension = Path.GetExtension(AssetDatabase.GetAssetPath(Selection.activeObject));
            fileName = fileName.Split('.')[0];
            string copiedFilePath =
                directoryPath + Path.DirectorySeparatorChar + fileName + "_Reversed" + fileExtension;
            var clip = GetSelectedClip();

            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(Selection.activeObject), copiedFilePath);

            clip = (AnimationClip)AssetDatabase.LoadAssetAtPath(copiedFilePath, typeof(AnimationClip));

            if (clip == null)
            {
                return;
            }

            float clipLength = clip.length;

            // var curves = AnimationUtility.GetAllCurves(clip, true);
            var curves = AnimationUtility.GetObjectReferenceCurveBindings(clip);

            clip.ClearCurves();

            // foreach (var curve in curves)
            // {
            //     var keys = curve.curve.keys;
            //     int keyCount = keys.Length;
            //     var curvePostWrapMode = curve.curve.postWrapMode;
            //     curve.curve.postWrapMode = curve.curve.preWrapMode;
            //     curve.curve.preWrapMode = curvePostWrapMode;
            //     for (int i = 0; i < keyCount; i++)
            //     {
            //         Keyframe K = keys[i];
            //         K.time = clipLength - K.time;
            //         var tmp = -K.inTangent;
            //         K.inTangent = -K.outTangent;
            //         K.outTangent = tmp;
            //         keys[i] = K;
            //     }
            //
            //     curve.curve.keys = keys;
            //     clip.SetCurve(curve.path, curve.type, curve.propertyName, curve.curve);
            // }

            var events = AnimationUtility.GetAnimationEvents(clip);
            if (events.Length > 0)
            {
                foreach (var animationEvent in events)
                {
                    animationEvent.time = clipLength - animationEvent.time;
                }

                AnimationUtility.SetAnimationEvents(clip, events);
            }

            Debug.Log("Animation reversed!");
        }

        [MenuItem("Assets/Create Reversed Clip", true)]
        private static bool ReverseClipValidation()
        {
            return Selection.activeObject is AnimationClip;
        }

        public static AnimationClip GetSelectedClip()
        {
            var clips = Selection.GetFiltered(typeof(AnimationClip), SelectionMode.Assets);
            if (clips.Length > 0)
            {
                return clips[0] as AnimationClip;
            }

            return null;
        }
    }
}