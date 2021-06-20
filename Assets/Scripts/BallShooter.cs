using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DefaultNamespace;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PingPong;
    public float startSpeed = 10.0f;
    public float onceReflectPeriod = 0.9f;
    public Vector3 onceReflectSpeed = new Vector3(1.4f, 1f, 1f);
    public GameObject direction;
    public float FireRate = 20.0f;
    

    private float lastfired =0.0f;

    public float y_offset = 0.5f;
    public float forward_offset = 1f;
    
    public Button saveJsonBtn;
    public Button resetJsonBtn;
    public InputField inputForPos;

    private string jsonPath = "C:\\Users\\Ray\\Downloads\\tiyuchang\\Assets\\Data\\autodata.json";
    private string allData;
    private autoDataList dataList;
    public float moveSpeed = 2.0f;

    public int ballCount = 0;
    
    void Start()
    {
        allData = File.ReadAllText(jsonPath);
        dataList=JsonUtility.FromJson<autoDataList>(allData);
        Button btn = saveJsonBtn.GetComponent<Button>();
        btn.onClick.AddListener(save2Json);
        Button btn2 = resetJsonBtn.GetComponent<Button>();
        btn2.onClick.AddListener(resetJson);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("up")) 
        {direction.transform.position += moveSpeed * new Vector3(0, 1, 0);}
        if (Input.GetKey("right")) 
        {direction.transform.position += moveSpeed * new Vector3(0, 0, 1);}
        if (Input.GetKey("down")) 
        {direction.transform.position -= moveSpeed * new Vector3(0, 1, 0);}
        if (Input.GetKey("left")) 
        {direction.transform.position -= moveSpeed * new Vector3(0, 0, 1);}

        if (Time.time - lastfired > FireRate)
        {
            lastfired = Time.time;
            Vector3 ball_position = direction.transform.position;
            ball_position.y += y_offset;
            ball_position += direction.transform.forward * Time.deltaTime * forward_offset;
            GameObject clone;
            clone = Instantiate(PingPong, ball_position, direction.transform.rotation);
            ballCount += 1;
            clone.GetComponent<Rigidbody>().velocity = clone.transform.TransformDirection(Vector3.forward * startSpeed);
            clone.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(BallReflect(clone));
            Destroy(clone, 3.0f);
        }

    }

    void save2Json()
    {
        autoData onceData = new autoData()
        {
            forPos = inputForPos.text,
            shooterPos = direction.transform.position,
            shooterAngle = direction.transform.rotation,
            sendSpeed = startSpeed,
        };
        dataList.dataList.Add(onceData);
        var serializer = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(jsonPath, serializer);

    }

    void resetJson()
    {
        autoData onceData = new autoData()
        {
            forPos = inputForPos.text,
            shooterPos = direction.transform.position,
            shooterAngle = direction.transform.rotation,
            sendSpeed = startSpeed,
        };
        
        dataList.dataList.Clear(); 
        dataList.dataList.Add(onceData);
        var serializer = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(jsonPath, serializer);

    }

    IEnumerator BallReflect(GameObject Ball)
    {
        yield return new WaitForSeconds(onceReflectPeriod);
        Ball.GetComponent<Rigidbody>().velocity = Vector3.Scale(-Ball.GetComponent<Rigidbody>().velocity , onceReflectSpeed);
    }
    

}
