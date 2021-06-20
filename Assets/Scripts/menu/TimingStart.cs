using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TimingStart : MonoBehaviour
{
    public GameObject splashCanvas;
    public GameObject mainCanvas;
    public GameObject pingPongPlayer;

    private CanvasGroup splashGroup;
    private CanvasGroup mainGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        
        splashGroup = splashCanvas.GetComponent<CanvasGroup>();
        mainGroup = mainCanvas.GetComponent<CanvasGroup>();
        pingPongPlayer.SetActive(false);
        splashGroup.alpha = 1;
        mainGroup.alpha = 0;
        StartCoroutine("timestart");
    }

    IEnumerator timestart()
    {
        yield return new WaitForSeconds(4f);
        splashGroup.alpha = 0;
        mainGroup.alpha = 1;
        pingPongPlayer.SetActive(true);
           }

    // Update is called once per frame
    void Update()
    {
        
    }
}
