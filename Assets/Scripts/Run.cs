using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System;

public class Run : MonoBehaviour
{
    private NetworkManager m_nm;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            Logger.Singleton.LogInfo($"Running the application in editor...");
            return;
        }

        m_nm = GetComponentInParent<NetworkManager>();
 
        if (GetArgs().TryGetValue("--role", out string role))
        {
            Logger.Singleton.LogInfo($"Running the application as {role}");
            switch (role)
            {
                case "server":
                    Logger.Singleton.LogInfo("Launching server...");
                    m_nm.StartServer();
                    break;
                case "host":
                    Logger.Singleton.LogInfo("Launching host (an honest client?)...");
                    m_nm.StartHost();
                    break;
                default:
                    Logger.Singleton.LogInfo("Launching client...");
                    m_nm.StartClient();
                    break;
            }
        }
    }

    private Dictionary<string, string> GetArgs()
    {
        var args_raw = Environment.GetCommandLineArgs();
        var args = new Dictionary<string, string>();
        for (int index = 1; index < args_raw.Length; index += 2)
        {
            args.Add(args_raw[index], args_raw[index + 1]);
        }
        return args;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
