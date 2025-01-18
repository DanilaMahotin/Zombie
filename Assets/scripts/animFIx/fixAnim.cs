using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using YG;

public class fixAnim : MonoBehaviour
{
    float _moveInput;
    Animator _anim;

    public GameObject grenadePrefab;
    public GameObject handForGrenade;
    private Transform throwPoint;
    private float throwForce = 12;
    private float amplitude = 5f;

    public int killCount;
    public static int grenadeInvenoty;
    private int lastKillCount ;

    private bool reload;
    
    public static bool grenadeKick;

    public TMP_Text killGrenade;
    public TMP_Text grenadeCount;
    private int killGrenadeText;

    public static int healthPointUpgrade ;
    private int lastKillforHealth;
    public static bool hpUpgrade;

    private bool isGameOver;

    private AudioSource audioSource;
    public AudioClip grenadeKickAudio;
    public AudioClip explosionGrenade;

    private float mobileMove;

    private bool isPc;
    private bool isMobile;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        lastKillCount = 0;
        grenadeKick = false;
        killGrenadeText = 0;
        healthPointUpgrade = 1;
        lastKillforHealth = 0;
        hpUpgrade = false;
        audioSource = GetComponent<AudioSource>();
        isPc = checkPlatform.isPc;
        isMobile = checkPlatform.isMobile;
        
        if (YandexGame.SDKEnabled == true)
        {
            LoadSaveGrenade();
        }
    }
    void Update()
    {
        isGameOver = gameManager.isGameOver;
        killGrenade.text = "" + killGrenadeText;
        grenadeCount.text = "" + grenadeInvenoty;
        _moveInput = playerMove.moveInput;
        mobileMove = playerMove.mobileMove;
        _anim.SetBool("runFix", _moveInput > 0 || _moveInput < 0 || mobileMove < 0 || mobileMove > 0);

        
        killCount = gameManager.killCount;
        
        killGrenadeText = killCount % 10 ;
        if (killCount % 10 == 0) 
        {
            killGrenadeText *= 10;
        }
        reload = StrikeScript.isReloading;
       

        CheckGrenade();


        
            if (Input.GetKeyDown(KeyCode.Space) && grenadeInvenoty > 0 && !reload && !isGameOver)
            {
                grenadeKick = true;
                _anim.SetBool("granade", true);
                Invoke("ThrowGrenade", 0.9f);
                grenadeInvenoty--;
                SaveGrenade();
            }
        
        else if (isMobile) 
        {
            
        }
        

        CheckHealth();

    }

    private void CheckHealth() 
    {
        if (killCount / 10 > lastKillforHealth / 10)
        {
            hpUpgrade = true;
            healthPointUpgrade++;
        }
        lastKillforHealth = killCount;
    }
    
    private void CheckGrenade() 
    {
        if (killCount / 10 > lastKillCount / 10) 
        {
            grenadeInvenoty++;
            SaveGrenade();
        }
        lastKillCount = killCount;
    }
    
    private void ThrowGrenade() 
    {
        _anim.SetBool("granade", false);
        grenadeKick = false ;
        throwPoint = handForGrenade.GetComponent<Transform>();
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, Quaternion.identity);
        Transform grenadeTransform = grenade.transform;
        Transform bangTransform = grenadeTransform.Find("Explosion");
        GameObject bang = bangTransform.gameObject;
        bang.SetActive(false);

        Vector3 throwDirection = new Vector3(1.9f, 0, 0);

        StartCoroutine(MoveGrenade(grenade, bang, throwDirection));

        
    }

    private System.Collections.IEnumerator MoveGrenade(GameObject grenade, GameObject bang, Vector3 direction) 
    {
        
        float elapsedTime = 0f;
        Vector3 startPosition = grenade.transform.position;
        float travelTime = 1f;

        while (elapsedTime < travelTime) 
        {
            float t = elapsedTime / travelTime;
            grenade.transform.position = startPosition + direction * throwForce * t;

            grenade.transform.position += Vector3.up * (Mathf.Sin(t * Mathf.PI) * amplitude);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        grenade.GetComponent<MeshRenderer>().enabled = false;
        grenade.GetComponent<Collider>().isTrigger = true;
        bang.SetActive(true);
        audioSource.clip = explosionGrenade;
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        
        Destroy(grenade);
        
    }

    public void SaveGrenade()
    {
        YandexGame.savesData.grenadeInvenoty =grenadeInvenoty;
        YandexGame.SaveProgress();
    }
    public void LoadSaveGrenade()
    {
        grenadeInvenoty = YandexGame.savesData.grenadeInvenoty;
    }

    public void MobileGrenade()
    {
        if (grenadeInvenoty > 0 && !reload && !isGameOver) 
        {
            grenadeKick = true;
            _anim.SetBool("granade", true);
            Invoke("ThrowGrenade", 0.9f);
            grenadeInvenoty--;
            SaveGrenade();
        }
    }
}
