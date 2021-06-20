using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Shift;
using RockVR.Video;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomMenu : MonoBehaviour
{
    public GameObject AvatarController;
    public GameObject rightHand;
    public GameObject cursor;
    public GameObject cursorColor;
    public Camera headCam;

    public GameObject RandomMode;
    public GameObject RandomHL;
    private Rect RandomRect;
    public GameObject StaticSender;
    //public EventSystem eventSys;

    private AvatarController ava;

    private bool randomOn = false;
    private bool customOn = false;

    public GameObject scoreCount;

    private bool handActivate=false;
    
    public GameObject Switcher;
    private Rect SwitcherRect;
    private SwitchManager swi;

    public GameObject recorder;

    
    // Start is called before the first frame update
    void Start()
    {
        ava = AvatarController.GetComponent<AvatarController>();
        RandomRect = RandomMode.GetComponent<RectTransform>().rect;
       
        RandomRect.position = new Vector2(RandomMode.GetComponent<RectTransform>().anchoredPosition.x, 
            RandomMode.GetComponent<RectTransform>().anchoredPosition.y);

        SwitcherRect = Switcher.GetComponent<RectTransform>().rect;
       
        SwitcherRect.position = new Vector2(Switcher.GetComponent<RectTransform>().anchoredPosition.x, 
            Switcher.GetComponent<RectTransform>().anchoredPosition.y);
     
        
        RandomHL.GetComponent<CanvasGroup>().alpha = 0;
        swi = Switcher.GetComponent<SwitchManager>();
      

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
        cursor.GetComponent<Transform>().localPosition = new Vector3(rightHandScreenPos.x-1000, rightHandScreenPos.y-500, 
            cursor.GetComponent<Transform>().localPosition.z); 
            
        if (!ava.leftHandOpen&& handActivate)
        {
            cursorColor.GetComponent<Image>().color= Color.red;
            if (isInRect(RandomRect,rightHandScreenPos))
            {
                StaticSender.SetActive(true);
                scoreCount.SetActive(true);
                gameObject.SetActive(false);
            }
            
            if (isInRect(SwitcherRect,rightHandScreenPos)&& !swi.isOn )
            {
                
                swi.AnimateSwitch();
                recorder.GetComponent<VideoCaptureCtrl>().StartCapture();
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

        if (swi.isOn)
        {
            
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
