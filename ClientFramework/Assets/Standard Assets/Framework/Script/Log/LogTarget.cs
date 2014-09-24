using System;

/// <summary>
/// 日志输出的目标
/// </summary>
public enum eLogTarget
{
    /// <summary>
    /// 关闭日志
    /// </summary>
    Invalid,
    /// <summary>
    /// 输出到控制台
    /// </summary>
    Console,
    /// <summary>
    /// 输出到屏幕
    /// </summary>
    Screen,
    /// <summary>
    /// 输出到文件
    /// </summary>
    File
}
