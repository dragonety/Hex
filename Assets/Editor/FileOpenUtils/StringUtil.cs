using UnityEngine;
using System.Text;
using System;

public static class StringUtil {
    public static readonly string[] splitSeparator = {"|"};
    private static StringBuilder stringBuilder = new StringBuilder();
    private static object lockObject = new object();

    public static string Concat(params object[] objects) {
        if (objects == null) {
            return string.Empty;
        }

        lock (lockObject) {
            stringBuilder.Length = 0;
            foreach (var item in objects) {
                if (item != null) {
                    stringBuilder.Append(item);
                }
            }

            return stringBuilder.ToString();
        }
    }

    public static bool CustomEndsWith(this string a, string b) {
        int ap = a.Length - 1;
        int bp = b.Length - 1;

        if (ap < bp) {
            return false;
        }
        
        while (ap >= 0 && bp >= 0 && a[ap] == b[bp]) {
            ap--;
            bp--;
        }

        return (bp < 0 && a.Length >= b.Length) || (ap < 0 && b.Length >= a.Length);
    }

    public static bool CustomStartsWith(this string a, string b) {
        int aLen = a.Length;
        int bLen = b.Length;

        if (aLen < bLen) {
            return false;
        }

        int ap = 0;
        int bp = 0;


        while (ap < aLen && bp < bLen && a[ap] == b[bp]) {
            ap++;
            bp++;
        }

        return (bp == bLen && aLen >= bLen) || (ap == aLen && bLen >= aLen);
    }
    public static string Substring(string input, int startIndex, int length) {
        if (string.IsNullOrEmpty(input)) {
            throw new NullReferenceException("空字符串无法执行这项操作");
        }

        return input.Substring(startIndex, length);
    }

    public static void RenameGameObject(GameObject gameObject, params object[] objects) {
#if UNITY_EDITOR
        gameObject.name = Concat(objects);
#endif
    }
}