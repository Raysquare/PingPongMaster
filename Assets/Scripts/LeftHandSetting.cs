using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Shift;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class LeftHandSetting : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject AvatarController;
    public GameObject ShooterScript;
    public GameObject NumberCanvas;
    public GameObject PingPongBat;
    
    public GameObject cursor;
    public GameObject cursorColor;

    public GameObject Switcher;
    public GameObject SliderHandle1;
    public GameObject SliderHandle2;
    public GameObject SaveNStart;

    public GameObject SwitchLightOn;
    public GameObject Slider1LightOn;
    public GameObject Slider2LightOn;
    public GameObject StartLightOn;

    public Slider slider1;
    public Slider slider2;
    
    public Camera fov;

    private bool doOnce=false;
    private bool doOnceSS = false;
    
    private float timeRecord;
    private float timeRecordSS;
    private float timeDuration = 1.0f;
    private bool sliderControlling1= false;
    private float tempSlider1;
    
    private bool sliderControlling2= false;
    private float tempSlider2;
    private AvatarController _ava;

    public GameObject scoreCount;

    private bool handActivate=false;

    // Start is called before the first frame update
    void Start()
    {
        _ava = AvatarController.GetComponent<AvatarController>();
        ShooterScript.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_ava.leftHandOpen)
        {
            handActivate = true;
        }
        
        switchDetect();
        startBtnDetect();
        slider1Detect();
        slider2Detect();
        
        ShooterScript.GetComponent<BallShooter>().FireRate = 4f * slider1.value + 0.1f;
        ShooterScript.GetComponent<BallShooter>().startSpeed = 8f * slider2.value + 0.5f;
    }

    private void switchDetect()
    {
        SwitchManager swi = Switcher.GetComponent<SwitchManager>();
        
        Vector3 leftHandPos = leftHand.transform.position;
        Vector3 leftHandScreenPos = fov.WorldToScreenPoint(leftHandPos);
        cursor.GetComponent<Transform>().localPosition = new Vector3(leftHandScreenPos.x-1000, leftHandScreenPos.y-500, 0); 
        if (!_ava.leftHandOpen&&handActivate)
        {
            cursorColor.GetComponent<Image>().color= Color.red;
        }
        else
        {
            cursorColor.GetComponent<Image>().color= new Color(99, 198, 255);;
        }

        RectTransform SwitchRect = SwitchLightOn.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(SwitchRect);
        Vector2 realSwitchPos = new Vector2(SwitchRect.anchoredPosition.x-SwitchRect.rect.size.x*0.5f,
            SwitchRect.anchoredPosition.y-SwitchRect.rect.size.y*0.5f);
        Rect rectbox = new Rect(new Rect(realSwitchPos, SwitchRect.rect.size));
        if (isInRect(rectbox,cursor.GetComponent<RectTransform>().anchoredPosition))
        {
            SwitchLightOn.GetComponent<CanvasGroup>().alpha = 1f;
            if (!_ava.leftHandOpen&& !doOnce&&handActivate)
            {
                swi.AnimateSwitch();
                doOnce = true;
                timeRecord = Time.time;
            }
        }
        else
        {
            SwitchLightOn.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        
        if (timeRecord + timeDuration < Time.time)
        {
            doOnce = false;
      
        }

        if (swi.isOn)
        {
            NumberCanvas.SetActive(true);
        }
        else
        {
            NumberCanvas.SetActive(false);
        }
    }

    private void startBtnDetect()
    {
        RectTransform StartRect = StartLightOn.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(StartRect);
        Vector2 realStartPos = new Vector2(StartRect.anchoredPosition.x-StartRect.rect.size.x*0.5f,
            StartRect.anchoredPosition.y-StartRect.rect.size.y*0.5f);
        Rect rectboxStart = new Rect(new Rect(realStartPos, StartRect.rect.size));
        if (isInRect(rectboxStart,cursor.GetComponent<RectTransform>().anchoredPosition))
        {
            StartLightOn.GetComponent<CanvasGroup>().alpha = 1f;
            if (!_ava.leftHandOpen&& !doOnceSS&&handActivate)
            {
                doOnceSS = true;
                timeRecordSS = Time.time;
                _ava.initialPosition = new Vector3(-30.72f, -0.2f, -11.53f);
                _ava.bodyRootPosition = new Vector3(-17.72f, -0.2f, -13f);
                _ava.initialRotation = Quaternion.Euler(0f,90f,0f);
                PingPongBat.SetActive(true);
                scoreCount.SetActive(true);
                NumberCanvas.SetActive(false);
                this.transform.gameObject.SetActive(false);
            }
        }
        else
        {
            StartLightOn.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
     
        if (timeRecordSS + timeDuration < Time.time)
        {
            doOnceSS = false;
        }
    }
    
    private void slider1Detect()
    {
        float sliderValue1 = slider1.value;
        RectTransform Slider1Rect = Slider1LightOn.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(Slider1Rect);
        Vector2 realSlider1Pos = new Vector2(Slider1Rect.anchoredPosition.x-Slider1Rect.rect.size.x*0.5f,
            Slider1Rect.anchoredPosition.y-Slider1Rect.rect.size.y*0.5f);
        Rect rectboxSlider1 = new Rect(new Rect(realSlider1Pos, Slider1Rect.rect.size));
        if (isInRect(rectboxSlider1,cursor.GetComponent<RectTransform>().anchoredPosition))
        {
            Slider1LightOn.GetComponent<CanvasGroup>().alpha = 1f;
            if (_ava.leftHandOpen == false&&handActivate)
            {
                if (!sliderControlling1)
                {
                    tempSlider1 = cursor.GetComponent<RectTransform>().anchoredPosition.x;
                    sliderControlling1 = true;
                    //Debug.Log("update once");
                } 
                float yOffset1 = tempSlider1 - cursor.GetComponent<RectTransform>().anchoredPosition.x;
                float newValue1 = sliderValue1 - yOffset1 * 0.00005f;
                if (newValue1 > 0 && newValue1 < 1)
                {
                    slider1.value = newValue1;
                }
            }
            else
            {
                sliderControlling1 = false;
            }
        }
        else
        {
            Slider1LightOn.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }

    private void slider2Detect ()
    {
        float sliderValue2 = slider2.value;
        RectTransform Slider2Rect = Slider2LightOn.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(Slider2Rect);
        Vector2 realSlider2Pos = new Vector2(Slider2Rect.anchoredPosition.x-Slider2Rect.rect.size.x*0.5f,
            Slider2Rect.anchoredPosition.y-Slider2Rect.rect.size.y*0.5f);
        Rect rectboxSlider2 = new Rect(new Rect(realSlider2Pos, Slider2Rect.rect.size));
        if (isInRect(rectboxSlider2,cursor.GetComponent<RectTransform>().anchoredPosition))
        {
            Slider2LightOn.GetComponent<CanvasGroup>().alpha = 1f;
            if (_ava.leftHandOpen == false&&handActivate)
            {
                if (!sliderControlling2)
                {
                    tempSlider2 = cursor.GetComponent<RectTransform>().anchoredPosition.x;
                    sliderControlling2 = true;
                } 
                float yOffset2 = tempSlider2 - cursor.GetComponent<RectTransform>().anchoredPosition.x;
                float newValue2 = sliderValue2 - yOffset2 * 0.00005f;
                if (newValue2 > 0 && newValue2 < 1)
                {
                    slider2.value = newValue2;
                }
            }
            else
            {
                sliderControlling2 = false;
            }
        }
        else
        {
            Slider2LightOn.GetComponent<CanvasGroup>().alpha = 0.5f;
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
