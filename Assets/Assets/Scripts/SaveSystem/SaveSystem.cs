using UnityEngine;
using System.IO;
using System.Text;
using System;

public static class SaveSystem
{
    private static string fileName = "vampire_meta_save.json";
    private static string path => Path.Combine(Application.persistentDataPath, fileName);

    // Simple Base64 "obfuscation" - not secure encryption
    private static string Encode(string plain)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
    }

    private static string Decode(string encoded)
    {
        try
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        }
        catch
        {
            Debug.LogWarning("Save file decode failed, using raw content instead.");
            return encoded;
        }
    }

    public static void SaveMeta(SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            string encoded = Encode(json);
            File.WriteAllText(path, encoded);
            Debug.Log("💾 Meta saved to: " + path);
        }
        catch (Exception e)
        {
            Debug.LogError("SaveMeta failed: " + e);
        }
    }

    public static SaveData LoadMeta()
    {
        if (!File.Exists(path))
        {
            Debug.Log("⚠️ No meta save found — returning new SaveData");
            return new SaveData();
        }

        try
        {
            string encoded = File.ReadAllText(path);
            string json = Decode(encoded);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data == null) data = new SaveData();
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("LoadMeta failed: " + e);
            return new SaveData();
        }
    }

    public static void DeleteMeta()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("🗑 Meta save deleted: " + path);
        }
    }

    public static string GetSavePath()
    {
        return path;
    }
}
