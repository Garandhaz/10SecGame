using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonMashHandler : MonoBehaviour
{
    AudioSource audioSource;
    public AudioHandler audioHandler;
    float time = 0;
    float total_time;
    int score;
    int max_score;
    
    public Transform charge_meter;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(time>0)
        {
            time-=Time.deltaTime;
            Check_Mash();
        }
    }

    public void Start_Timer(float time, int max_mash)
    {
        this.time = time;
        this.total_time = time;
        score = 0;
        max_score = max_mash;
        Update_Display();
    }

    public bool Is_Complete()
    {
        return time<=0;
    }

    public int Get_Score()
    {
        return score;
    }

    void Check_Mash()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            score++;
            audioSource.clip = audioHandler.punch;
            audioSource.Play();
            Update_Display();
        }
    }

    void Update_Display()
    {
            Vector3 new_scale = charge_meter.localScale;
            new_scale.x = Mathf.Clamp((float)score/max_score,0f,1f);
            charge_meter.localScale = new_scale;
    }
}

