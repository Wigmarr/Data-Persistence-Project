using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int bestScore = 0;
    public string playerName = "Bob";
    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Deserialize();
    }

    public void SetName(string name)
    {
        playerName = name;
    }

    public void SetBestScore(int score)
    {
        bestScore = score;
    }

    public void Serialize()
    {
        SaveData saveData = new SaveData();
        saveData.playerName = playerName;
        saveData.score = bestScore;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Deserialize()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            bestScore = data.score;
        }
    }

    private void OnApplicationQuit()
    {
        Serialize();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }
}
