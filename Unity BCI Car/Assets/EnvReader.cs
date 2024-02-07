using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class EnvReader
{
    public static string GetVariable(string variableName)
    {
        string path = Application.dataPath + "/.env";

        if (!File.Exists(path))
        {
            Debug.LogError("Env file not found.");
            return null;
        }

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2 && parts[0].Trim() == variableName)
            {
                return parts[1].Trim();
            }
        }

        Debug.LogError("Variable not found in the .env file: " + variableName);
        return null;
    }
}

