using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataSave : MonoBehaviour
{
    [Header("file storage config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataSave> dataSaveObjects;
    private FileDataHandler dataHandler;

    public static DataSave instance { get; private set; }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataSaveObjects = FindAllDataSaveObjects();
        LoadGame();
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError(">1 save serial");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("no data for load");
            NewGame();
        }

        foreach(IDataSave dataSaveObj in dataSaveObjects)
        {
            dataSaveObj.LoadData(gameData);
        }

        Debug.Log("load money:" + gameData.money);
        Debug.Log("load scene:" + gameData.sceneNumber);
        Debug.Log("load currentSkin:" + gameData.currentSkin);
        for(int i =0; i < 3; i++)
        {
            Debug.Log("load Skins:" + gameData.unlockedSkins[i]);
        }
    }

    public void SaveGame()
    {
        foreach (IDataSave dataSaveObj in dataSaveObjects)
        {
            dataSaveObj.SaveData(ref gameData);
        }
        Debug.Log("save money:" + gameData.money);
        Debug.Log("save scene:" + gameData.sceneNumber);
        Debug.Log("save currentSkin:" + gameData.currentSkin);
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("save Skins:" + gameData.unlockedSkins[i]);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataSave> FindAllDataSaveObjects()
    {
        IEnumerable<IDataSave> dataSaveObjects = 
            FindObjectsOfType<MonoBehaviour>().OfType<IDataSave>();

        return new List<IDataSave>(dataSaveObjects);
    }
}

[System.Serializable]
public class GameData
{
    public int sceneNumber;
    public int money;
    public int currentSkin;
    public Dictionary<int, bool> unlockedSkins;

    public GameData()
    {
        this.currentSkin = 0; 
        this.sceneNumber = 0;
        this.money = 0;

        this.unlockedSkins = new Dictionary<int, bool>();
        this.unlockedSkins.Add(currentSkin, true);
        this.unlockedSkins.Add(1, false);
        this.unlockedSkins.Add(2, false);
    }
}

