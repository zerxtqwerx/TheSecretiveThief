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
        foreach (bool flag in gameData.skins)
        {
            Debug.Log(flag);
        }

    }

    public void SaveGame()
    {
        foreach (IDataSave dataSaveObj in dataSaveObjects)
        {
            dataSaveObj.SaveData(ref gameData);
        }
        Debug.Log("save money:" + gameData.money);
        foreach(bool flag in gameData.skins)
        {
            Debug.Log(flag);
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
    public List<bool> skins;
    

    public GameData()
    {
        this.money = 0;
        skins = new List<bool> { true, false, false };
        
    }
}

