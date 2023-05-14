using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ManagerTimer : MonoBehaviour
{
    [SerializeField] private Image timer;
    [SerializeField] Image[] itemSlots;
    public float timeToTake;
    public float timeToOut;
    public GameObject winPanel;
    public GameObject selectLevel;
    public Money money;


    private float time;
    private bool isTaking;
    private bool isOutLevel;
    private Image timerObj;
    private GameObject _object;
    private Camera cam;
    private PlayerMovement player;

    static public int itemsAmount = 0;

    [Space]
    [SerializeField] private Image RageFillImage;
    [SerializeField] private float secondsToGrab;
    public float RageFillSeconds;
    private bool isRage;

    private void Start()
    {
        cam = Camera.main;   
        player = FindObjectOfType<PlayerMovement>();
    }
    public void RageAddition()
    {
        RageFillSeconds += Time.deltaTime * 2;
        isRage = true;
    }

    private void RageController()
    {
        RageFillSeconds -= Time.deltaTime;

        if (RageFillSeconds < 0) RageFillSeconds = 0;
        RageFillImage.fillAmount = RageFillSeconds / secondsToGrab;
        if (RageFillSeconds >= secondsToGrab) SceneManager.LoadScene(0);

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


    private void OutLevel()
    {
        player.isMove = false;
        time -= Time.deltaTime;
        timerObj.fillAmount = time / timeToOut;

        if (time <= 0)
        {
            player.isMove = true;
            itemsAmount = 0;

            money.AddMoneyOnFinishLevel();
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
                break;
            }
        }
    }
}
