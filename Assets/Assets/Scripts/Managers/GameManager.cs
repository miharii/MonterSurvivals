using UnityEngine;
using System.Collections;

// Simple GameManager focused on meta-only save (gold, unlocks, settings).
// Drop this on a GameObject called "SaveManager" and mark it DontDestroyOnLoad.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SaveData saveData = new SaveData();

    // autosave interval for meta changes (optional)
    [Tooltip("Interval in seconds to autosave meta if changes happen. Set 0 to disable periodic auto-save.")]
    public float autoSaveInterval = 0f;

    private bool dirty = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMeta();
            if (autoSaveInterval > 0f) StartCoroutine(AutoSaveLoop());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkDirty()
    {
        dirty = true;
    }

    public void SaveMeta()
    {
        SaveSystem.SaveMeta(saveData);
        dirty = false;
    }

    public void LoadMeta()
    {
        saveData = SaveSystem.LoadMeta();
        Debug.Log("📂 Meta loaded. Gold: " + saveData.meta.gold);
    }

    IEnumerator AutoSaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            if (dirty)
            {
                SaveMeta();
                Debug.Log("🕒 Auto-saved meta (dirty)");
            }
        }
    }

    // Convenience API
    public void AddGold(int amount)
    {
        saveData.meta.gold += amount;
        MarkDirty();
    }

    public bool UnlockCharacter(string id)
    {
        if (!saveData.meta.unlocked_characters.Contains(id))
        {
            saveData.meta.unlocked_characters.Add(id);
            MarkDirty();
            return true;
        }
        return false;
    }

    public bool UnlockItem(string id)
    {
        if (!saveData.meta.unlocked_items.Contains(id))
        {
            saveData.meta.unlocked_items.Add(id);
            MarkDirty();
            return true;
        }
        return false;
    }

    public void SetMusicVolume(float v)
    {
        saveData.meta.settings.musicVolume = Mathf.Clamp01(v);
        MarkDirty();
    }

    public void SetSfxVolume(float v)
    {
        saveData.meta.settings.sfxVolume = Mathf.Clamp01(v);
        MarkDirty();
    }
}
