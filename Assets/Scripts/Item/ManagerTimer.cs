using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Advertisements;

public class ManagerTimer : MonoBehaviour
{
    [SerializeField] private Image timer;
    [SerializeField] Image[] itemSlots;
    int countLives;

    public float timeToTake;
    public float timeToOut;
    public GameObject winPanel;
    public GameObject gameOver;
    public GameObject selectLevel;
    public Money money;
    private HP hp;


    private float time;
    private float gameTime = 0.0f;
    public float GameTime() { return gameTime; }

    private bool isTaking;
    private bool isOutLevel;
    private Image timerObj;
    private GameObject _object;
    private Camera cam;
    private PlayerMovement player;
    private Vector3 playerStartPosition;


    [SerializeField] private int died = 0;
    public int Died() { return died; }

    static public int itemsAmount = 0;

    [Space]
    [SerializeField] Image RageFillImage;
    [SerializeField] private float secondsToGrab;
    public float RageFillSeconds;
    private bool isRage;

    private void Start()
    {
        cam = Camera.main;   
        player = FindObjectOfType<PlayerMovement>();
        hp = GameObject.FindWithTag("lives").GetComponent<HP>();
        Time.timeScale = 0;
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("5444950", false);
        }
    }

    public void TimeScale(float n)
    {
        Time.timeScale = n;
    }
    public void RageAddition()
    {
        RageFillSeconds += Time.deltaTime * 3;
        isRage = true;
    }

    private void RageController()
    {
        RageFillSeconds -= Time.deltaTime;

        if (RageFillSeconds < 0) RageFillSeconds = 0;
        RageFillImage.fillAmount = RageFillSeconds / secondsToGrab;
        if (RageFillSeconds >= secondsToGrab)
        {
            RageFillImage.fillAmount = 0;
            hp.MinusHP();

            if (died == 2)
            { 
                gameOver.SetActive(true);
                selectLevel.SetActive(true);
                if (Advertisement.isInitialized)
                    Advertisement.Show("Interstitial_Android");
            }
            else
            {
                RageFillSeconds = 0;
                player.MovePlayerToStartLevel();
                died += 1;
            }
        }
    }

    public void SetTime(GameObject obj)
    {
        if (obj.GetComponent<Finish>() && itemsAmount >= 1)
        {
            isOutLevel = true;
            time = timeToOut;
            timerObj = Instantiate(timer);
            timerObj.transform.SetParent(transform);
            timerObj.transform.position = cam.WorldToScreenPoint(obj.transform.position);
        }
        
        if (obj.GetComponent<Item>())
        {
            _object = obj;
            player.isMove = false;

            timerObj = Instantiate(timer);
            timerObj.transform.SetParent(transform);
            timerObj.transform.position = cam.WorldToScreenPoint(obj.transform.position);
            time = timeToTake;

            isTaking = true;
        }

    }

    private void Update()
    {
        RageController();

        if (isOutLevel) OutLevel();
        if (isTaking) TakeItem();

    }

    private void CountTime()
    {
        gameTime += Time.deltaTime;
    }

    private void OutLevel()
    {
        RageFillImage.fillAmount = 0;
        player.isMove = false;
        time -= Time.deltaTime;
        timerObj.fillAmount = time / timeToOut;

        if (time <= 0)
        {
            player.isMove = true;
            itemsAmount = 0;

            money.AddMoneyOnFinishLevel();
            Time.timeScale = 0;
            ShowWinPanel();
            ShowSelectLevel();

        }
    }

    private void ShowSelectLevel()
    {
        selectLevel.gameObject.SetActive(true);
    }

    private void ShowWinPanel()
    {
        winPanel.gameObject.SetActive(true);
    }

    private void TakeItem()
    {

        time -= Time.deltaTime;
        timerObj.fillAmount = time / timeToTake;
        
        if (time <= 0)
        {
            itemsAmount += 1;
            _object.GetComponent<ItemPointer>().Destroy();
            money.CollectMoneyOnLevel(_object.GetComponent<Item>().price);
            Destroy(timerObj);
            player.isMove = true;
            isTaking = false;
            PickItem();
        }
    }



    private void PickItem()
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].GetComponent<ItemSlot>().icon == null)
            {
                Item item = _object.GetComponent<Item>();
                itemSlots[i].GetComponent<ItemSlot>().SetItem(item.icon, item.index);

                var tempColor = itemSlots[i].color;
                tempColor.a = 1f;

                itemSlots[i].color = tempColor;
                break;
            }
        }
    }
}
