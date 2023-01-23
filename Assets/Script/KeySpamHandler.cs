using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeySpamHandler : MonoBehaviour
{
    AudioSource audioSource;
    public AudioHandler audioHandler;
    float time = 0;
    
    float total_time;
    
    int score;

    List<KeyCode> current_prompt = new List<KeyCode>();

    public List<TMP_Text> key_display = new List<TMP_Text>();

    int prompt_index;

    public int prompt_size = 5;

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
            Check_Prompt_Input();
        }
    }

    public void Start_Timer(float time)
    {
        this.time = time;
        this.total_time = time;
        score = 0;
        prompt_index = 0;
        Generate_Prompt();
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

    void Generate_Prompt()
    {
        current_prompt.Clear();
    for(int i = 0; i < prompt_size; i++)
    {
        current_prompt.Add((KeyCode)Random.Range((int)KeyCode.A, (int)KeyCode.Z));
    }
    }

    void Check_Prompt_Input()
    {
        if(Input.GetKeyDown(current_prompt[prompt_index]))
        {
            //TODO Feedback
            prompt_index++;
            audioSource.clip = audioHandler.punch;
            audioSource.Play();
        }
        if(prompt_index == current_prompt.Count)
        {
            prompt_index = 0;
            score++;
            Generate_Prompt();
            Update_Display();
        }
    }
    
    void Update_Display()
    {
        for(int i = 0; i < prompt_size; i++)
        {
            key_display[i].text = current_prompt[i].ToString();
        }
    }
}
