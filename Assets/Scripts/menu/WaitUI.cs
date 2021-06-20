using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUI : MonoBehaviour
{
    public GameObject nextCanvas;
    public GameObject turnOffCanvas;
    public float waitTime = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitNGo());
    }

    IEnumerator waitNGo()
    {
        if (turnOffCanvas != null)
        {
            turnOffCanvas.SetActive(false);
        }
        yield return new WaitForSeconds(waitTime);
       
        nextCanvas.SetActive(true);
        
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
