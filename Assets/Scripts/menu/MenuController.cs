using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject AvatarController;
    public GameObject rightHand;
    public GameObject cursor;
    public GameObject cursorColor;
    public Camera headCam;

    public GameObject RandomMode;
    public GameObject RandomHL;
    private Rect RandomRect;
    public GameObject CustomMode;
    public GameObject CustomHL;
    private Rect CustomRect;
    private AvatarController ava;

    private bool randomOn = false;
    private bool customOn = false;
    private bool handActivate=false;
    



    public GameObject kinectController;
    // Start is called before the first frame update
    void Start()
    {
        ava = AvatarController.GetComponent<AvatarController>();
        RandomRect = RandomMode.GetComponent<RectTransform>().rect;
        CustomRect = CustomMode.GetComponent<RectTransform>().rect;
       
        RandomRect.position = new Vector2(RandomMode.GetComponent<RectTransform>().anchoredPosition.x, 
            RandomMode.GetComponent<RectTransform>().anchoredPosition.y);

        CustomRect.position = new Vector2(CustomMode.GetComponent<RectTransform>().anchoredPosition.x, 
            CustomMode.GetComponent<RectTransform>().anchoredPosition.y);
        
        RandomHL.GetComponent<CanvasGroup>().alpha = 0;
        CustomHL.GetComponent<CanvasGroup>().alpha = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (ava.leftHandOpen)
        {
            handActivate = true;
        }
        
        Vector3 rightHandPos = rightHand.transform.position;
        Vector3 rightHandScreenPos = headCam.WorldToScreenPoint(rightHandPos);
        Vector2 rightHandScreen2D = new Vector2(rightHandScreenPos.x, rightHandScreenPos.y);
        cursor.GetComponent<Transform>().localPosition = new Vector3(rightHandScreenPos.x-1000, rightHandScreenPos.y-500, 
            cursor.GetComponent<Transform>().localPosition.z); 
        if (!ava.leftHandOpen&&handActivate)
        {
            cursorColor.GetComponent<Image>().color= Color.red;
            if (isInRect(RandomRect,rightHandScreenPos))
            {
                print("get random scene");
                Destroy(kinectController);
                SceneManager.LoadScene("RandomShooter",LoadSceneMode.Single);
            }
            
            if(isInRect(CustomRect,rightHandScreenPos))
            {
                print("get custom scene");
                Destroy(kinectController);
                SceneManager.LoadScene("ShooterScene", LoadSceneMode.Single);
            }
        }
        else
        {
            cursorColor.GetComponent<Image>().color= new Color(99, 198, 255);;
        }

        if (isInRect(RandomRect,rightHandScreenPos) && !randomOn)
        {
            RandomHL.GetComponent<CanvasGroup>().alpha = 1;
            randomOn = true;
        }
        else if(!isInRect(RandomRect,rightHandScreenPos) && randomOn)
        {
            RandomHL.GetComponent<CanvasGroup>().alpha = 0;
            randomOn = false;
        }
        
        if (isInRect(CustomRect,rightHandScreenPos) && !customOn)
        {
            CustomHL.GetComponent<CanvasGroup>().alpha = 0;
            customOn = true;
        }
        else if(!isInRect(CustomRect,rightHandScreenPos)  && customOn)
        {
            CustomHL.GetComponent<CanvasGroup>().alpha = 1;
            customOn = false;
        }

    }

    private static bool isInRect(Rect theRect, Vector3 pos)
    {
        if (pos.x < theRect.xMax && pos.x > theRect.xMin)
        {
            if (pos.y < theRect.yMax && pos.y > theRect.yMin)
            {
                return true;
            }
        }

        return false;
    }
}
