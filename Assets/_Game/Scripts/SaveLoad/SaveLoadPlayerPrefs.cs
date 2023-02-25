using UnityEngine;

public static class SaveLoadPlayerPrefs
{
    public const string SOUNDS_PREF = "SOUNDS_PREF";
    
    public const string MUSIC_PREF = "MUSIC_PREF";

    public static void SaveSoundsPref(bool isOn)
    {
        PlayerPrefs.SetInt(SOUNDS_PREF, isOn ? 1 : 0);
    }

    public static bool LoadSoundsPref()
    {
        if(!PlayerPrefs.HasKey(SOUNDS_PREF)) 
        {
            SaveSoundsPref(true);
        }
        return PlayerPrefs.GetInt(SOUNDS_PREF) != 0;
    }

    public static void SaveMusicPref(bool isOn)
    {
        PlayerPrefs.SetInt(MUSIC_PREF, isOn ? 1 : 0);
    }

    public static bool LoadMusicPref()
    {
        if (!PlayerPrefs.HasKey(MUSIC_PREF))
        {
            SaveMusicPref(true);
        }
        return PlayerPrefs.GetInt(MUSIC_PREF) != 0;
    }
}
