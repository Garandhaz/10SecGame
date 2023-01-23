using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockHandler : MonoBehaviour
{
    public TMP_Text clockText;
    float time = 0;
    float total_time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time>0)
        {
            time-=Time.deltaTime;
            clockText.text = ((int)(time + .5)).ToString();
        }
    }

    public void Start_Timer(float time)
    {
        this.time = time;
        this.total_time = time;
    }

}
