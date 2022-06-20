using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaTest : MonoBehaviour
{
    private LuaEnv luaEnv;
    
    private void Awake()
    {
        StartLua();
        CustomSet();
    }

    private void StartLua()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
        }

        luaEnv = new LuaEnv();
        luaEnv.AddLoader(LoadLua);
        luaEnv.DoString($"require('main').start()");
    }

    private void CustomSet()
    {
        var scriptEvn = luaEnv.NewTable();
        scriptEvn.Set("self", this);
        //luaEnv.DoString($"require('main').start()", "chunk",scriptEvn);
    }

    private static byte[] LoadLua(ref string filepath)
    {
        string root = Path.Combine(Application.dataPath, "Scripts/Lua");
        byte[] result = null;
        string[] pathes = {"", ""};
        pathes[0] = root;
        pathes[1] = filepath.Replace('.', Path.DirectorySeparatorChar) + ".lua";
        string realPath = Path.Combine(pathes);
        Debug.LogError(realPath);
        FileInfo info = new FileInfo(realPath);
        if (!info.Exists)
        {
            return null;
        }
        return File.ReadAllBytes(realPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
