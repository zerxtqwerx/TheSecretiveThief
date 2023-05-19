using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class SaveSerial : MonoBehaviour
{
    string file;
    SaveData saveData = new SaveData();

    int sceneNumberToSave;
    Money moneyToSave;

    void Start()
    {
        file = Application.persistentDataPath + "/SaveData.json";
        LoadGame();
    }

    void Awake()
    {
        file = Application.persistentDataPath + "/SaveData.json";
    }

    public void SaveGame()
    {
        /*BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(file);
        SaveData data = new SaveData();
        data.money = moneyToSave;
        data.sceneNumber = sceneNumberToSave;
        bf.Serialize(file, data);
        file.Close();*/

        saveData = new SaveData();
        saveData.sceneNumber = SceneManager.GetActiveScene().buildIndex;
        try
        {
            moneyToSave = GameObject.FindObjectOfType<Money>();
            if (moneyToSave == null)
            {
                saveData.money = moneyToSave.GetMoney();
            }
        }
        catch (Exception e)
        {
            Debug.Log("SaveGame/money " + e);
        }

        File.WriteAllText(file, JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));

        /*string jsonString = JsonUtility.ToJson(saveData);
        File.WriteAllText(file, jsonString);*/
        Debug.Log("save game");
    }

    public void LoadGame()
    {
        if (File.Exists(file))
        {
            /*BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(file, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            sceneNumberToSave = data.sceneNumber;
            moneyToSave = data.money;*/

            saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(file));
            if (SceneManager.GetActiveScene().buildIndex == saveData.sceneNumber)
            {
                try
                {
                    moneyToSave = GameObject.FindObjectOfType<Money>();
                    if (moneyToSave != null)
                    {
                        moneyToSave.SetMoney(saveData.money);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("LoadGame/money " + e);
                }
            }
            //string fileContents = File.ReadAllText(file);
            //data = JsonUtility.FromJson<SaveData>(fileContents);

            Debug.Log("game data loaded");
        }
        else
            Debug.LogError("no save data");
    }
}

[System.Serializable]
class SaveData
{
    public int sceneNumber;
    public int money;
    //public int currentPlayerSkin;
    //public int currentNPCSkin;
    //public int[] unlockedPlayerSkin;
    //public int[] unlockedNPCSkin;
}

