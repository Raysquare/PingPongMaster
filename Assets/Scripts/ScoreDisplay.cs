using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ScoreDisplay : MonoBehaviour
{
    public GameObject gotPoint;
    public GameObject totalShoot;
    private Text _gotPointText;
    private Text _totalShootText;
    private float point = 0;
    private float originalCount = 0;
    public bool isCustom = true;
    public GameObject getCount;
    private BallShooter bs;
    private RandomBallShooter rbs;
    
    // Start is called before the first frame update
    void Start()
    {
        _gotPointText = gotPoint.GetComponent<Text>();
        _totalShootText = totalShoot.GetComponent<Text>();

        if (isCustom)
        {
            bs = getCount.GetComponent<BallShooter>();
            originalCount=bs.ballCount;
        }
        else
        {
            rbs = getCount.GetComponent<RandomBallShooter>();
            originalCount=rbs.ballCount;
        }
        
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.name == "PingPongBall(Clone)")
        {
            point += 1;
        }

    }
    // Update is called once per frame
    void Update()
    {
        _gotPointText.text=point.ToString();
        if (isCustom)
        {
            _totalShootText.text = (bs.ballCount - originalCount).ToString();

        }
        else
        {
            _totalShootText.text = (rbs.ballCount - originalCount).ToString();

        }

    }
}
