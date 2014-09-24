using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class GameClient : MonoBehaviour
{
    public static GameClient Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("重复挂载GameClient");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
