using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour//, IDataSave
{
    public void changeScene(int numberOfSceneNext)
    {
        SceneManager.LoadScene(numberOfSceneNext);
    }

    /*public void LoadData(GameData data)
    {
        SceneManager.LoadScene(data.sceneNumber);
    }


    public void SaveData(ref GameData data)
    {
        data.sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }*/
}
