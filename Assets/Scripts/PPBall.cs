using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class PPBall : MonoBehaviour
{

    private AudioSource ballBource;
    private AudioSource batHit;

    // Start is called before the first frame update
    void Start()
    {
        ballBource = GetComponent<AudioSource>();
        batHit = transform.GetChild(0).GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision) 
    { 
        ballBource.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
