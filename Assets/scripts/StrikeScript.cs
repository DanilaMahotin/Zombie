using System.Collections;

using TMPro;


using UnityEngine;

public class StrikeScript : MonoBehaviour
{
    

    [Header("Ray")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Min(0)] private float _distance = Mathf.Infinity;
    [SerializeField, Min(0)] private int _shotCount = 1;

    private bool isShooting = false;


    public TMP_Text bulletsText;
    public TMP_Text bulletsFullText;
    private int bullets = 30;
    private int bulletsFull = 30;

    [SerializeField] private float reloadTime = 2f;
    public static bool isReloading;

    public GameObject reloadIndecator;

    private int killDefault;
    private int killPolice;
    private int killBuilder;
    private int _damage = 4;

    private GameObject handForReload;
    private Animator reloadAnim;
    private bool isReload = false;

    public GameObject flash;

    public  static int killCountForGranade;

    private bool isGameOver;
    private int killCount;
    private int LastKillCount;

    private AudioSource audioSource;
    public AudioClip shot;
    public AudioClip reload;
    public AudioClip empty;

    private bool isPc;
    private bool isMobile;

    private bool grenadeKick;
    private void Start()
    {
        reloadIndecator.SetActive(false);
        flash.SetActive(false);
        handForReload = GameObject.Find("Hip_bone");
        reloadAnim = handForReload.GetComponent<Animator>();
        LastKillCount = 0;
        audioSource = GetComponent<AudioSource>();
        isReloading = false;

        
    }

    

    private void PerformRaycast() 
    {
        
        var direction = transform.forward;
        var ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask)) 
        {
            
            
            var hitCollider = hitInfo.collider;
            if (hitCollider.gameObject.CompareTag("zombieDefault"))
            {

                var zombie = hitCollider.GetComponentInParent<ZombieMove>();
                zombie.healthPoint -= _damage;
            }
            else if (hitCollider.gameObject.CompareTag("zombiePolice"))
            {
                var zombie = hitCollider.GetComponentInParent<ZombiePolice>();
                zombie.healthPoint -= _damage;
            } else if (hitCollider.gameObject.CompareTag("zombieBuilder")) 
            {
                var zombie = hitCollider.GetComponentInParent<ZombieBuilder>();
                zombie.healthPoint -= _damage;
            } else if (hitCollider.gameObject.CompareTag("ZombieBoss"))
            {
                var zombie = hitCollider.GetComponentInParent<ZombieBoss>();
                zombie.healthPoint -= _damage;
            }
        }
    }

    private void Update()
    {
        isPc = checkPlatform.isPc;
        isMobile = checkPlatform.isMobile;
        isGameOver = gameManager.isGameOver;
        bulletsText.text = "" + bullets;
        bulletsFullText.text = "" + bulletsFull;

        grenadeKick = fixAnim.grenadeKick;

        int reloadRotateZ;
        if (isGameOver)
        {
            reloadRotateZ = 0;
            audioSource.Stop();
        }
        else 
        {
            reloadRotateZ = 2;    
        }
        if (isPc)
        {
            if (bullets <= 0 && Input.GetMouseButtonDown(0))
            {
                audioSource.clip = empty;
                audioSource.Play();
            }
            if (Input.GetMouseButton(0) && bullets > 0 && !isReloading && !grenadeKick && !isGameOver)
            {
                if (!isShooting)
                {

                    isShooting = true;
                    StartCoroutine(Shoot());
                }
            }
            else if (!Input.GetMouseButtonDown(0))
            {
                isShooting = false;
            }

            if (Input.GetKey(KeyCode.R) && !isReloading && !isShooting)
            {

                StartCoroutine(Reload());

            }
        }
        else if (isMobile) 
        {
            
        }
        

        var reloadRotate = reloadIndecator.GetComponent<Transform>();
        reloadRotate.Rotate(0, 0, reloadRotateZ);


        checkKill();
        
        

        if (!isShooting ) 
        {
            flash.SetActive(false);
        }
        reloadAnim.SetBool("reload", isReloading);
    }

    private void checkKill() 
    {
        killCount = gameManager.killCount;
        if (killCount / 3 > LastKillCount / 3) 
        {
            _damage += 4;
        }
        LastKillCount = killCount;
    }
    private IEnumerator Shoot() 
    {
        while (isShooting) 
        {
            bullets--;
            PerformRaycast();
            flash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            audioSource.clip = shot;
            audioSource.Play();
            if (bullets == 0) 
            {
                isShooting=false;
            }
        }
    }

    private IEnumerator Reload() 
    {
        if (isShooting) 
        {
            StopCoroutine(Shoot());
            isShooting = false;
        }
        
        audioSource.clip = reload;
        audioSource.Play();
        isReloading = true;
        reloadIndecator.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        //audioSource.Stop();
        bullets = bulletsFull;
        reloadIndecator.SetActive(false);
        isReloading = false;
    }

    public void ReloadMobile() 
    {
        if (!isReloading && !isShooting) 
        {
            StartCoroutine(Reload());
        }
    }

    public void MobileStrike() 
    {
        if (bullets <= 0)
        {
            audioSource.clip = empty;
            audioSource.Play();
        }
        if (bullets > 0 && !isReloading && !grenadeKick && !isGameOver)
        {
            if (!isShooting)
            {

                isShooting = true;
                StartCoroutine(Shoot());
            }
        } 
    }

    public void MobileDONTStrike() 
    {
        isShooting = false;
    }
    



}
