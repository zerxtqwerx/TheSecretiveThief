using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RageFill : MonoBehaviour
{
    //public Image image;
    private Image rageFillImage;

    [SerializeField] private float secondsToGrab;
    public float RageFillSeconds;
    private bool isRage;

    void Awake()
    {
        //image = Resources.Load<Image>("Assets/2d/RageFillImage");
        //enemy = GameObject.FindGameObjectWithTag("enemy");
        //Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity, enemy.transform);
    }

    void Start()
    {
        rageFillImage = GetComponent<Image>();
        if( rageFillImage != null ) { Debug.Log("rageFill image"); }
    }

   /* void Update()
    {
        RageController();
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
        rageFillImage.fillAmount = RageFillSeconds / secondsToGrab;
        //if (RageFillSeconds >= secondsToGrab) SceneManager.LoadScene(0);
        

    }*/
}
