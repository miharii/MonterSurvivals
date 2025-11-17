Vampire-style META Save System (Meta-only)
Compatible with Unity 6000.2.1f1 (and earlier Unity 2021/2022/2023)

Included files (relative to project root):
- Assets/Scripts/SaveSystem/MetaData.cs
- Assets/Scripts/SaveSystem/SaveData.cs
- Assets/Scripts/SaveSystem/SaveSystem.cs
- Assets/Scripts/Managers/GameManager.cs

How to use:
1) Import this package folder into your Unity project (copy Assets/... into your project's Assets/).
2) In the Editor, create an empty GameObject in your initial scene and name it "SaveManager".
   Attach the GameManager.cs script to it.
   (Or create a prefab from it to persist between scenes.)
3) Configure autoSaveInterval on the GameManager component if you want periodic auto-saving.
   Otherwise call GameManager.Instance.SaveMeta() after important changes (e.g., after purchases, unlocks).
4) Access and modify meta data via GameManager.Instance.saveData.meta (gold, unlocked_characters, unlocked_items, settings).
   Example:
       GameManager.Instance.AddGold(500);
       GameManager.Instance.UnlockCharacter("Imelda");
       GameManager.Instance.SetMusicVolume(0.7f);
5) The save file is stored at:
   Application.persistentDataPath/vampire_meta_save.json
6) The save file content is Base64-encoded JSON (obfuscation, not secure encryption).

Notes:
- This package only contains scripts. Unity-specific assets like .prefab or .meta files are not included.
- If you want a .unitypackage instead of this zip, import the scripts into a project and use Assets->Export Package... in the Editor.

Enjoy — tell me if you want a UI sample (buttons) included next.
