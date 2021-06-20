using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RockVR.Video;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ResultMenu : MonoBehaviour
{
    public GameObject AvatarController;
    public GameObject leftHand;
    public GameObject cursor;
    public GameObject cursorColor;
    public Camera headCam;

    public GameObject BackMode;
    public GameObject BackHL;
    private Rect BackRect;

    public GameObject TarMode;
    public GameObject TarHL;
    private Rect TarRect;
    
    private AvatarController ava;

    private bool randomOn = false;
    private bool customOn = false;

    public GameObject kinectController;
    public GameObject targetedTraining;
    
    private List<string> videoFiles = new List<string>();

    public UnityEngine.Video.VideoPlayer video1;
    public UnityEngine.Video.VideoPlayer video2;


    // Start is called before the first frame update
    void Start()
    {
        ava = AvatarController.GetComponent<AvatarController>();
        
        BackRect = BackMode.GetComponent<RectTransform>().rect;
        BackRect.position = new Vector2(BackMode.GetComponent<RectTransform>().anchoredPosition.x, 
            BackMode.GetComponent<RectTransform>().anchoredPosition.y); 
        BackHL.GetComponent<CanvasGroup>().alpha = 0;
      
        TarRect = TarMode.GetComponent<RectTransform>().rect;
        TarRect.position = new Vector2(TarMode.GetComponent<RectTransform>().anchoredPosition.x, 
            TarMode.GetComponent<RectTransform>().anchoredPosition.y); 
        TarHL.GetComponent<CanvasGroup>().alpha = 0;

        print(PathConfig.saveFolder);
        if (Directory.Exists(PathConfig.SaveFolder))
        {
            DirectoryInfo direction = new DirectoryInfo(PathConfig.SaveFolder);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            videoFiles.Clear();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".mp4"))
                {
                    videoFiles.Add(PathConfig.SaveFolder + files[i].Name);
                    continue;
                }
            }
        }
        videoFiles = videoFiles.OrderBy(q => q).ToList();
        video1.url = "file://" + videoFiles[videoFiles.Count-1];
        print(videoFiles[videoFiles.Count-1]);
        video2.url = "file://" + videoFiles[videoFiles.Count-2];
        print(videoFiles[videoFiles.Count-2]);
        

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 rightHandPos = leftHand.transform.position;
        Vector3 rightHandScreenPos = headCam.WorldToScreenPoint(rightHandPos);
        cursor.GetComponent<Transform>().localPosition = new Vector3(rightHandScreenPos.x-1000, rightHandScreenPos.y-500, 
            cursor.GetComponent<Transform>().localPosition.z); 

        if (!ava.leftHandOpen)
        {
            cursorColor.GetComponent<Image>().color= Color.red;
            if (isInRect(BackRect,rightHandScreenPos))
            {
                print("get random scene");
                Destroy(kinectController);
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }
            
            if (isInRect(TarRect,rightHandScreenPos))
            {
                gameObject.SetActive(false);
                targetedTraining.SetActive(true);
            }
        }
        else
        {
            cursorColor.GetComponent<Image>().color= new Color(99, 198, 255);;
        }

        if (isInRect(BackRect,rightHandScreenPos) && !randomOn)
        {
            BackHL.GetComponent<CanvasGroup>().alpha = 1;
            randomOn = true;
        }
        else if(!isInRect(BackRect,rightHandScreenPos) && randomOn)
        {
            BackHL.GetComponent<CanvasGroup>().alpha = 0;
            randomOn = false;
        }
        
        if (isInRect(TarRect,rightHandScreenPos) && !customOn)
        {
            TarHL.GetComponent<CanvasGroup>().alpha = 1;
            customOn = true;
        }
        else if(!isInRect(TarRect,rightHandScreenPos) && customOn)
        {
            TarHL.GetComponent<CanvasGroup>().alpha = 0;
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
