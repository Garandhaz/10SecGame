using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Game States to keep track of the step of the game we're on.
    enum gameState {
        Start,
        CurtainOpen,
        KeySpam,
        ButtonMash,
        End,
        CurtainCloseWin,
        CurtainCloseLose,
        }

    gameState current_State;
    bool new_State = false;

    public GameObject instructions;
    public GameObject win;
    public GameObject lose;
     public GameObject end;
     public GameObject enemy;
     public GameObject particle;
      public GameObject particle2;
    public CurtainController curtain;
    public KeySpamHandler keyspammer;
    public ButtonMashHandler buttonmasher;
    public ClockHandler clock;
    public PlayerHandler player;
    public LazerHandler lazer;
    public Vector2 flip;
    AudioSource audioSource;
    public float audioVolume = 0f;
    public AudioHandler audioHandler;

    float keyspam_time = 5;
    float buttonmash_time = 5;
    int buttonmash_max_mash = 10;


    // Start is called before the first frame update
    void Start()
    {
        Change_State(gameState.Start);
        audioSource = GetComponent<AudioSource>();
        //lazer.transform.rotation = Quaternion.identity;
    }
    
    // Update is called once per frame
    void Update()
    {
        Do_State();
        if (Input.GetKey("escape")) // quit game
            {
                Application.Quit();
            }
    }

    // Start of game, Confirm Player input

    void Do_State(bool start=false)
    {
                switch(current_State)
        {
            case gameState.Start:
                State_Start(start);
                break;
            case gameState.CurtainOpen:
                State_CurtainOpen(start);
                break;
            case gameState.KeySpam:
                State_KeySpam(start);
                break;
            case gameState.ButtonMash:
                State_ButtonMash(start);
                break;
            case gameState.End:
                State_End(start);
                break;
            case gameState.CurtainCloseWin:
                State_CurtainCloseWin(start);
                break;
            case gameState.CurtainCloseLose:
                State_CurtainCloseLose(start);
                break;
        }
        new_State = false;
    }

    void Change_State(gameState state)
    {
        current_State = state;
        Do_State(true);
    }

    void State_Start(bool start=false)
    {
        if(start)
        {
            instructions.gameObject.SetActive(true);
            Debug.Log("Game Start!");
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Return))
            {
                instructions.gameObject.SetActive(false);
                Change_State(gameState.CurtainOpen);
            }
        }
    }
void State_CurtainOpen(bool start=false)
    {
        if(start)
        {
            Debug.Log("Rising");
            curtain.SetAnimation("CurtainOpen");
            audioSource.clip = audioHandler.chains;
            audioSource.Play();
        }
        else
        {
            if(!curtain.anim.isPlaying)
            {
                audioSource.Stop();
                Change_State(gameState.KeySpam);
            }
        }
    }

        // Start of game, Confirm Player input
    void State_KeySpam(bool start=false)
    {
        if(start)
        {
            Debug.Log("Start Spamming");
            keyspammer.gameObject.SetActive(true);
            keyspammer.Start_Timer(keyspam_time);
            clock.Start_Timer(keyspam_time + buttonmash_time);
            player.SetAnimation("Power1");
            particle.gameObject.SetActive(true);
            audioSource.clip = audioHandler.poweringUp;
            audioSource.volume = audioVolume;
            audioSource.Play();
        }
        else
        {
            if(keyspammer.Is_Complete())
            {
                particle.gameObject.SetActive(false);
                keyspammer.gameObject.SetActive(false);
                Change_State(gameState.ButtonMash);
            }
        }
    }

    void State_ButtonMash(bool start=false)
    {
        if(start)
        {
            Debug.Log("Start Mashing");
            buttonmasher.gameObject.SetActive(true);
            buttonmasher.Start_Timer(buttonmash_time, buttonmash_max_mash);
            player.SetAnimation("Power2");
            particle2.gameObject.SetActive(true);
            audioSource.clip = audioHandler.poweringUp;
            audioSource.volume = audioVolume;
            audioSource.Play();
        }
        else
        {
            if(buttonmasher.Is_Complete())
            {
                buttonmasher.gameObject.SetActive(false);
                Change_State(gameState.End);
            }
        }
    }

    void State_End(bool start=false)
    {
        if(start)
        {
        Debug.Log("Game End!");
        Debug.Log("keyspam score: "+keyspammer.Get_Score());
        Debug.Log("buttonmash score: "+buttonmasher.Get_Score());
        
        }
        else if(keyspammer.Get_Score() > 0 && buttonmasher.Get_Score() >= 10)
        {
            
            {
                Debug.Log("You Win!");
                Change_State(gameState.CurtainCloseWin);
            }
        }
        else
        {
            Debug.Log("You lose!");
            particle2.gameObject.SetActive(false);
            Change_State(gameState.CurtainCloseLose);
        }
    }
    
    void State_CurtainCloseWin(bool start=false)
    {
        if(start)
        {
            Debug.Log("Closing");
            Debug.Log("Lazertime");
            audioSource.clip = audioHandler.power;
            audioSource.Play();
            lazer.gameObject.SetActive(true);
            lazer.transform.rotation = Quaternion.Euler(0, 0, 0);
            lazer.SetAnimation("Lazer");
            curtain.SetAnimation("CurtainClose");
            audioSource.clip = audioHandler.BGM;
            audioSource.Stop();
            audioSource.clip = audioHandler.win;
            audioSource.volume = 1;
            audioSource.Play();
            win.gameObject.SetActive(true);
            end.gameObject.SetActive(true);
            enemy.gameObject.SetActive(false);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.R))
            {
                particle2.gameObject.SetActive(false);
                lazer.gameObject.SetActive(false);
                win.gameObject.SetActive(false);
                end.gameObject.SetActive(false);
                audioSource.clip = audioHandler.win;
                audioSource.volume = 1;
                audioSource.Stop();
                player.SetAnimation("Power1");
                enemy.gameObject.SetActive(true);
                Change_State(gameState.Start);
            }

        }
    }
    void State_CurtainCloseLose(bool start=false)
    {
        if(start)
        {
            Debug.Log("Lazertime");
            audioSource.clip = audioHandler.power;
            audioSource.Play();
            lazer.gameObject.SetActive(true);
            lazer.transform.rotation = Quaternion.Euler(0, 180, 0);
            lazer.SetAnimation("Lazer");
            Debug.Log("Closing");
            curtain.SetAnimation("CurtainClose");
            audioSource.clip = audioHandler.lose;
            audioSource.volume = 1;
            audioSource.Play();
            lose.gameObject.SetActive(true);
            end.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.R))
            {
                audioSource.clip = audioHandler.lose;
                audioSource.volume = 1;
                lazer.gameObject.SetActive(false);
                lose.gameObject.SetActive(false);
                end.gameObject.SetActive(false);
                player.gameObject.SetActive(true);
                player.SetAnimation("Power1");
                Change_State(gameState.Start);
            }
            
        }
    }
}