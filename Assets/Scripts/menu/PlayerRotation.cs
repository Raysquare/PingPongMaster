using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform _trans;
    private Transform _childTrans;
    
    Quaternion rotation;
    
    public GameObject player;
    void Start()
    {
        _trans = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _trans.Rotate(new Vector3(0,0,1) * (50 * Time.deltaTime), Space.Self);
    }
    void Awake()
    {

    }
    void LateUpdate()
    {
        
    }
}
