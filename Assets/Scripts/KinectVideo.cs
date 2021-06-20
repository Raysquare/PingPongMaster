using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinectVideo : MonoBehaviour
{
    public GameObject kinectManager;
    private KinectManager ki;
    public GameObject imageView;
    public 
    // Start is called before the first frame update
    void Start()
    {
        ki = kinectManager.GetComponent<KinectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        imageView.GetComponent<RawImage>().texture = ki.GetUsersClrTex2D();
    }
}
