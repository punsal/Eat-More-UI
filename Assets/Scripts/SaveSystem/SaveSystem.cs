using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {

    public static void SavePlayer(PlayerData player) {
        string path = Application.persistentDataPath + "/player.happy";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PersistentPlayerData data = new PersistentPlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PersistentPlayerData LoadPlayer() {
        string path = Application.persistentDataPath + "/player.happy";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PersistentPlayerData data = formatter.Deserialize(stream) as PersistentPlayerData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
