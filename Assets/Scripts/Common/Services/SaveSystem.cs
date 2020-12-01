using UnityEngine;
using System.IO;
using Assets.Scripts.Common.PlayerCommon;

//saving method that uses serialization/deserialzation while reading and writing to JSON

namespace Assets.Scripts.Common.Services
{
    //static save system class, should only ever be one of this
    public static class SaveSystem
    {
        //simple call to write to player.sav
        public static void SavePlayer(PlayerData data)
        {
            string path = Application.dataPath + "/player.sav";
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public static PlayerData LoadPlayer()
        {
            //simple call to read player.sav
            string path = Application.dataPath + "/player.sav";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<PlayerData>(json);
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}