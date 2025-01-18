using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;
using TMPro;
using System.Runtime.InteropServices;
using YG.Example;
using UnityEditor;
using Unity.VisualScripting;

public class gameManager : MonoBehaviour
{
    private int heartScore;
    private int LastHeartScore;
    public static bool isGameOver ;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public GameObject menu;

    public static int killCount;

    private AudioSource audioSource;
    private AudioSource backgroundAudio;
    public AudioClip killAudio;
    public AudioClip heartLow;
    public AudioClip backgroundSound;
    int lastKill;

    public GameObject YG;

    private bool SetGrenade = false;

    private bool isPc;
    private bool isMobile;
    //public GameObject menuPlatform;
    public GameObject mobileController;
    public GameObject grenadeMobileButton;
    public GameObject reloadMobilleButton;
    public GameObject strikeMobilleButton;
    public GameObject changePlatfrom;
    public Button changePlatfromButton;
    public GameObject keyCImage;
    public GameObject keySpaceImage;
    public RectTransform grenade;
    public GameObject adButton;
    

    private bool timeStop;

    private bool isPaused;

    private float timerAd;  
    private float timerTest = 0;

    public TMP_Text score;
    private void Awake()
    {
        DontDestroyOnLoad(YG);
    }
    void Start()
    {
        AudioListener.volume = 0.3f;
        menu.SetActive(false);
        isGameOver = false;
        killCount = 0;
        lastKill = 0;
        LastHeartScore = 0;
        audioSource = GetComponent<AudioSource>();  
        backgroundAudio = gameObject.AddComponent<AudioSource>();
        backgroundAudio.loop = true;
        backgroundAudio.clip = backgroundSound;
        backgroundAudio.Play();

        heart1.gameObject.SetActive(false);
        heart2.gameObject.SetActive(false);
        heart3.gameObject.SetActive(false);

        
        mobileController.SetActive(false);
        grenadeMobileButton.SetActive(false);
        reloadMobilleButton.SetActive(false);
        strikeMobilleButton.SetActive(false);
        changePlatfrom.SetActive(true);
        keyCImage.SetActive(false);
        keySpaceImage.SetActive(false);
        grenade.gameObject.SetActive(false);

        timeStop = false;

        isPaused = false;
       
        
    }

    
    void Update()
    {
        score.text = "Счёт: " + killCount;
        timerTest += Time.unscaledDeltaTime;
        print(timerTest);
        timerAd = YandexGame.timerShowAd;
        if (isPaused)
        {
            Time.timeScale = 0;
            
        }
        else 
        {
            Time.timeScale = 1;
            
            if (changePlatfrom.activeSelf) 
            {
                Time.timeScale = 0;
            }
        }
        

        isPc = checkPlatform.isPc;
        isMobile = checkPlatform.isMobile;

        if (isPc || isMobile)
        {
        changePlatfrom.SetActive(false);
        
        }

       

        if (isMobile)
        {
            mobileController.SetActive(true);
            grenadeMobileButton.SetActive(true);
            reloadMobilleButton.SetActive(true);
            strikeMobilleButton.SetActive(true);
            keyCImage.SetActive(false);
            keySpaceImage.SetActive(false);
            grenade.anchoredPosition = new Vector2(-112.8049f, 214f);
            grenade.gameObject.SetActive(true);
        }
        else if (isPc) 
        {
            Cursor.visible = false;
            keyCImage.SetActive(true);
            keySpaceImage.SetActive(true);
            mobileController.SetActive(false);
            grenadeMobileButton.SetActive(false);
            reloadMobilleButton.SetActive(false);
            strikeMobilleButton.SetActive(false);
            grenade.anchoredPosition = new Vector2(-61.805f, 218.19f);
            grenade.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.C)) 
            {
                ChangePlatfomrButton();
            }
        }
        
       
        
        
        heartScore = brriersScript.heart;
        
        switch (heartScore) 
        {
            case 0:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 1:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(true);
                break;
            case >=3:
                heart3.gameObject.SetActive(false);
                SaveGrenade();
                isGameOver = true;
                break;
        }
        
        CheckKillAudio();
        CheckHeartAudio();
        if (isGameOver)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            menu.SetActive(true);
            AudioListener.volume = 0;
            if (!SetGrenade) 
            {
                //fixAnim.grenadeInvenoty = 1;
                SetGrenade = true;
            }

            
        }
        else 
        {
            //Cursor.visible = false;
            menu.SetActive(false);
        }
        
    }

    public void ButtonRestart() 
    {
        
        isGameOver=false;
        SceneManager.LoadScene("main");
        brriersScript.heart = 0;
        SaveHeartScore();
    }

    

    
    public void CheckKillAudio() 
    {
        
        if (killCount - lastKill >= 1) 
        {
            audioSource.clip = killAudio;
            audioSource.Play();
            
        }
        lastKill = killCount;
    }


    public void CheckHeartAudio() 
    {
        if (heartScore - LastHeartScore == 1) 
        {
            audioSource.clip = heartLow;
            audioSource.Play();
        }
        LastHeartScore = heartScore;
    }

    public void AddGrenade() 
    {
        fixAnim.grenadeInvenoty += 5;
        SaveGrenade();
    }
    public void Resume() 
    {
        brriersScript.heart = 0;
        SaveHeartScore();
        isGameOver = false;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        SetGrenade = false;
        AudioListener.volume = 0.3f;

    }
    private void OnEnable()
    {
        //YandexGame.GetDataEvent += LoadSaveGrenade;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        //YandexGame.GetDataEvent -= LoadSaveGrenade;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void Rewarded(int id)
    {

        if (id == 1)
            Resume();

        else if (id == 2)
            AddGrenade();
    }


    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
        
    }

    public void SaveHeartScore() 
    {
        YandexGame.savesData.heartScore = brriersScript.heart;
        YandexGame.SaveProgress();
    }
    /*public void LoadSaveHeart() 
    {
        brriersScript.heart = YandexGame.savesData.heartScore;
    }*/
    public void SaveGrenade() 
    {
        YandexGame.savesData.grenadeInvenoty = fixAnim.grenadeInvenoty;
        YandexGame.SaveProgress();
    }

    public void ChangePlatfomrButton() 
    {
        Cursor.visible = true;
        checkPlatform.isPc = false;
        checkPlatform.isMobile = false;
        changePlatfrom.SetActive(true);
    }

    public void AdButton() 
    {
        if (timerTest < 60)
        {
            ButtonRestart();

        }
        else 
        {
            YandexGame.FullscreenShow();
            timerTest = 0;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        
        isPaused = !hasFocus;
        Silence(isPaused);
    }

    void OnApplicationPause(bool isPaused)
    {
        
        this.isPaused = isPaused;
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        // Или / И
        AudioListener.volume = silence ? 0 : 1;

    }

    
}
