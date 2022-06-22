using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSaver : MonoBehaviour
{
    [System.Serializable]
    class SaveData{
        public int HighScore;
        public string PlayerName;
    }

    public static DataSaver Instance;

    public int HighScore;
    public string PlayerName;

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void SavingData(){
        SaveData data = new SaveData();
        data.HighScore = HighScore;
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData(){
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            PlayerName = data.PlayerName;
        }
    }
}
