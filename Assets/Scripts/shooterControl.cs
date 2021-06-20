using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterControl : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject AvatarController;
    public GameObject headCamera;
    public float offsetY=-2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AvatarController ava = AvatarController.GetComponent<AvatarController>();
        
        float dist = Vector3.Distance(this.transform.position, rightHand.transform.position);
        var cubeRenderer = this.GetComponent<Renderer>();
        if (dist < 0.8 && !ava.rightHandOpen)
        {
            this.transform.position = new Vector3(rightHand.transform.position.x,rightHand.transform.position.y+offsetY,rightHand.transform.position.z);
            this.transform.rotation = headCamera.transform.rotation ;
            cubeRenderer.material.SetColor("_EmissionColor", Color.blue);
        }
        else
        {
            cubeRenderer.material.SetColor("_EmissionColor", Color.yellow);
        }
    }
}
