using System;
using System.IO;
using UnityEngine;
public class FileUtils
{
    public delegate void ConfigFileEntryParseCallback(string baseKey, string subKey, string val, object userData);

    /// <summary>
    /// 获取持久数据目录
    /// </summary>
    public static string PersistentDataPath
    {
        get
        {
            return string.Empty;
        }
    }

    public static string EditorDataPath
    {
        get
        {
            return string.Empty;
        }
    }
}
