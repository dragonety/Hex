using System;
using System.IO;
using System.Text;
using System.Threading;
using NPOI.HSSF.UserModel;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class FileOpenUtils {
    [OnOpenAsset(0)]
    private static bool OpenAssetStep1(int instanceId, int line) {
        var path = AssetDatabase.GetAssetPath(instanceId);
        if (ConfigFileOpenUtils.VerifyConfigFile(path)) {
            ConfigFileOpenUtils.StartOpenConfigFile(path);
            return true;
        }
        return false;
    }
}