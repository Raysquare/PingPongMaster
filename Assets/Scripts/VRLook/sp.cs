using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp : MonoBehaviour
{
    private bool startle = false;
    private float speed = 1f;

    public GameObject vr;

    public GameObject environment;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitdisp());
        
    }

    IEnumerator waitdisp()
    {

        yield return new WaitForSeconds(4f);
        vr.SetActive(true);
        environment.SetActive(false);
        StartCoroutine(goWork());
    }

    IEnumerator goWork()
    {
        yield return new WaitForSeconds(3f);
        startle = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (startle)
        {
            gameObject.transform.position += new Vector3(0,0,speed)* Time.deltaTime;
            speed += 0.01f;
        }
        
     
    }
}
