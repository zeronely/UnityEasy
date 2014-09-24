using System;
using System.IO;
using System.Text;
using UnityEngine;


public class Logger
{
    /// <summary>
    /// 文件日志目录
    /// </summary>
    private const string OUTPUT_DIRECTORY_NAME = "Logs";
    /// <summary>
    /// 日志文件扩展名
    /// </summary>
    private const string OUTPUT_FILE_EXTENSION = "log";

    // 日志者名字
    private string _name;
    private StreamWriter _writer;
    private bool _writerInitialized;

    public Logger(string name)
    {
        _name = name;
    }

    public bool CanPrint(eLogLevel level, eLogTarget target)
    {
        LogInfo logInfo = Log.Get().GetLogInfo(_name);
        if (logInfo == null)
        {
            return false;
        }
        if (level < logInfo.m_level)
        {
            return false;
        }
        return (target != eLogTarget.Invalid) && (logInfo.m_target == target);
    }

    public bool CanPrint()
    {
        LogInfo logInfo = Log.Get().GetLogInfo(_name);
        return (logInfo != null) && (logInfo.m_target != eLogTarget.Invalid);
    }

    public void Print(string format, params object[] args)
    {
        string message = string.Format(format, args);
        Print(eLogLevel.Debug, message);
    }
    public void Print(eLogLevel level, string message)
    {
        FilePrint(level, message);
        ConsolePrint(level, message);
        ScreenPrint(level, message);
    }
    public void FilePrint(string format, params object[] args)
    {
        string message = string.Format(format, args);
        FilePrint(eLogLevel.Debug, message);
    }
    public void FilePrint(eLogLevel level, string message)
    {
        if (!CanPrint(level, eLogTarget.File))
        {
            return;
        }
        InitFileWriter();
        if (_writer == null)
        {
            return;
        }
        StringBuilder stringBuilder = new StringBuilder();
        switch (level)
        {
            case eLogLevel.Debug:
                stringBuilder.Append("D ");
                break;
            case eLogLevel.Info:
                stringBuilder.Append("I ");
                break;
            case eLogLevel.Warning:
                stringBuilder.Append("W ");
                break;
            case eLogLevel.Error:
                stringBuilder.Append("E ");
                break;
        }
        stringBuilder.Append(DateTime.Now.TimeOfDay.ToString());
        stringBuilder.Append(" ");
        stringBuilder.Append(message);
        _writer.WriteLine(stringBuilder.ToString());
        _writer.Flush();
    }

    public void ConsolePrint(string format, params object[] args)
    {
        string message = string.Format(format, args);
        ConsolePrint(eLogLevel.Debug, message);
    }

    public void ConsolePrint(eLogLevel level, string message)
    {
        if (!CanPrint(level, eLogTarget.Console))
        {
            return;
        }
        string message2 = string.Format("[{0}] {1}", _name, message);
        switch (level)
        {
            case eLogLevel.Debug:
            case eLogLevel.Info:
                Debug.Log(message2);
                break;
            case eLogLevel.Warning:
                Debug.LogWarning(message2);
                break;
            case eLogLevel.Error:
                Debug.LogError(message2);
                break;
        }
    }

    public void ScreenPrint(string format, params object[] args)
    {
        string message = string.Format(format, args);
        ScreenPrint(eLogLevel.Debug, message);
    }

    public void ScreenPrint(eLogLevel level, string message)
    {
        if (!CanPrint(level, eLogTarget.Screen))
        {
            return;
        }
        // TODO:实现平屏幕打印
        //if (SceneDebugger.Get() == null)
        //{
        //    return;
        //}
        //string message2 = string.Format("[{0}] {1}", m_name, message);
        //SceneDebugger.Get().AddMessage(message2);
    }

    private void InitFileWriter()
    {
        if (_writerInitialized) return;
        _writer = null;
        string text = "Logs";
        if (!Directory.Exists(text))
        {
            try
            {
                Directory.CreateDirectory(text);
            }
            catch (Exception)
            {
            }
        }
        string path = string.Format("{0}/{1}.{2}", text, _name, "log");
        try
        {
            _writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite));
        }
        catch (Exception)
        {
        }
        _writerInitialized = true;
    }
}
