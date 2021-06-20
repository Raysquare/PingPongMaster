using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using RockVR.Video;
using UnityEngine;
using UnityEngine.UI;

public class RandomBallShooter : MonoBehaviour
{
    public GameObject PingPong;
    public GameObject shooter;
    public string jsonPath = "C:\\Users\\Ray\\Downloads\\tiyuchang\\Assets\\Data\\autodata.json";
    private string allData;
    private autoDataList dataList;
    private int index = 0;
    private int indexLength;
    public Text hitNO;
    public Text status;
    public float duration=3.0f;
    public GameObject resultCanvas;
    public int ballCount = 0;

    public GameObject recorder;
   
    
    // Start is called before the first frame update
    void Start()
    {
        allData = File.ReadAllText(jsonPath);
        dataList=JsonUtility.FromJson<autoDataList>(allData);
        indexLength = dataList.dataList.Count;
        StartCoroutine(beginWait());
    }

    IEnumerator beginWait()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(Waiter());
    }
    
    
    IEnumerator Waiter()
    {
        TryAShoot(dataList.dataList[index]);
        yield return new WaitForSeconds(duration);
        if (index+1 < indexLength)
        {
            index += 1;
            StartCoroutine(Waiter());
        }
        else
        {
            resultCanvas.SetActive(true);
            recorder.GetComponent<VideoCaptureCtrl>().StopCapture();
        }
    }

    IEnumerator TryAShootPls(autoData onceData)
    {
        shooter.transform.position = Vector3.MoveTowards(shooter.transform.position, onceData.shooterPos, 0.1f);
        yield return null;
        Vector3 ball_position = shooter.transform.position;
        ball_position.y += 0.62f;
        ball_position += shooter.transform.forward * Time.deltaTime * 5.1f;
        GameObject clone;
        clone = Instantiate(PingPong, ball_position, shooter.transform.rotation);
        clone.GetComponent<Rigidbody>().velocity = clone.transform.TransformDirection(Vector3.forward * onceData.sendSpeed);
        clone.GetComponent<Rigidbody>().useGravity = true;
        Destroy(clone, 3.0f);
    }
    void TryAShoot(autoData onceData)
    {
        StartCoroutine(MoveSender(onceData));
    }

    IEnumerator MoveSender(autoData onceData)
    {
        shooter.transform.position = Vector3.MoveTowards(shooter.transform.position, onceData.shooterPos, 
            0.1f);
        shooter.transform.rotation = onceData.shooterAngle;

        yield return null;

        hitNO.text= onceData.forPos;
        status.text = (index+1).ToString();
        if(Vector3.Distance(shooter.transform.position, onceData.shooterPos) < 0.1f){
            Vector3 ball_position = shooter.transform.position;
            ball_position.y += 0.62f;
            ball_position += shooter.transform.forward * Time.deltaTime * 5.1f;
            var clone = Instantiate(PingPong, ball_position, shooter.transform.rotation);
            ballCount += 1;
            clone.GetComponent<Rigidbody>().velocity = clone.transform.TransformDirection(Vector3.forward * onceData.sendSpeed);
            clone.GetComponent<Rigidbody>().useGravity = true;
            Destroy(clone, 3.0f);
        }
        else
        {
            TryAShoot(onceData);
        } 

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
