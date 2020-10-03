using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem {

    public static void SavePlayer (GameProgress game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/AdeptVelocity.fast";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(game);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("New save " + "/gamejame.game");
    }

    public static PlayerData LoadPLayer ()
    {
        string path = Application.persistentDataPath + "/gamejame.game";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;

            
        }else
        {
            Debug.LogError("No Save file found in " + path);
            return null;
        }
    }
}
