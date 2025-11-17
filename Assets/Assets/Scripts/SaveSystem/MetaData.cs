using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingData
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public bool fullscreen = true;
}

[Serializable]
public class MetaData
{
    public int gold = 0;
    public List<string> unlocked_characters = new List<string>();
    public List<string> unlocked_items = new List<string>();
    public List<string> achievements = new List<string>();
    public SettingData settings = new SettingData();
}
