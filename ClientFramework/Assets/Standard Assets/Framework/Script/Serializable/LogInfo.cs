using UnityEngine;


public class LogInfo : ScriptableObject
{
    [HideInInspector]
    [SerializeField]
    public string m_name;
    [HideInInspector]
    [SerializeField]
    public eLogTarget m_target = eLogTarget.Invalid;
    [HideInInspector]
    [SerializeField]
    public eLogLevel m_level = eLogLevel.Debug;
}
