using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    private const string FILE_NAME = "/gamesave1.sav";

    public static void SaveProgress(Save save)
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + FILE_NAME);
        binaryFormatter.Serialize(file, save);
        file.Close();
    }

    public static Save LoadProgress()
    {
        if (File.Exists(Application.persistentDataPath + FILE_NAME))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Open);
            Save save = (Save)binaryFormatter.Deserialize(file);
            file.Close();
            return save;
        }
        else
        {
            return GetEmptySave();
        }
    }

    private static Save GetEmptySave()
    {
        return new Save();
    }
}

