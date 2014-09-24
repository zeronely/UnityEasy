using System;
using System.IO;
using System.Collections.Generic;

public class Log
{
    private const string CONFIG_FILE_NAME = "log.config";

    public static Logger SYS = new Logger("sys");

    private readonly LogInfo[] DEFAULT_LOG_INFOS = new LogInfo[0];
    private static Log _inst;
    private Dictionary<string, LogInfo> m_logInfos = new Dictionary<string, LogInfo>();

    public static Log Get()
    {
        if (Log._inst == null)
        {
            Log._inst = new Log();
            Log._inst.Initialize();
        }
        return Log._inst;
    }

    public void Load()
    {
        string configPath = string.Format("{0}/{1}", FileUtils.PersistentDataPath, "log.config");
        if (File.Exists(configPath))
        {
            m_logInfos.Clear();
            // FileUtils.ParseConfigFile(configPath, new FileUtils.ConfigFileEntryParseCallback(this.OnConfigFileEntryParsed));
        }
        LogInfo[] dEFAULT_LOG_INFOS = this.DEFAULT_LOG_INFOS;
        for (int i = 0; i < dEFAULT_LOG_INFOS.Length; i++)
        {
            LogInfo logInfo = dEFAULT_LOG_INFOS[i];
            if (!this.m_logInfos.ContainsKey(logInfo.m_name))
            {
                this.m_logInfos.Add(logInfo.m_name, logInfo);
            }
        }
        Log.SYS.Print("log.config location: " + configPath, new object[0]);
    }

    public LogInfo GetLogInfo(string name)
    {
        LogInfo result = null;
        this.m_logInfos.TryGetValue(name, out result);
        return result;
    }

    private void Initialize()
    {
        Load();
    }


    private void OnConfigFileEntryParsed(string name, string targetStr, string levelStr)
    {
        LogInfo logInfo = null;
        if (!m_logInfos.TryGetValue(name, out logInfo))
        {
            logInfo = new LogInfo
            {
                m_name = name
            };
            m_logInfos.Add(logInfo.m_name, logInfo);
        }
        // 解析Target
        eLogTarget target = EnumUtils.GetEnum<eLogTarget>(targetStr, StringComparison.OrdinalIgnoreCase);
        logInfo.m_target = target;
        // 解析Level
        eLogLevel level = EnumUtils.GetEnum<eLogLevel>(levelStr, StringComparison.OrdinalIgnoreCase);
        logInfo.m_level = level;
    }
}
