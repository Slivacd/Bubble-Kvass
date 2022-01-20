using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace AppodealAppTracking.Unity.Editor
{
    public class IOSPostprocessUtils : MonoBehaviour
    {
        [PostProcessBuildAttribute(41)]
        private static void UpdatePod(BuildTarget target, string buildPath)
        {
            if (target != BuildTarget.iOS) return;
            if (string.IsNullOrEmpty(PlayerSettings.iOS.targetOSVersionString)) return;
            
                var plistPath = buildPath + "/Info.plist";
                var plist = new PlistDocument();
                plist.ReadFromString(File.ReadAllText(plistPath));
                var rootDict = plist.root;
                const string buildKey = "NSUserTrackingUsageDescription";
                rootDict.SetString(buildKey, "This identifier will be used to deliver personalized ads to you");
                File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}