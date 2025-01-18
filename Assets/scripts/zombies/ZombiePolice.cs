using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePolice : MonoBehaviour
{
    private float _zombieSpeed = 1.5f;
    Animator _anim;
    private float[] turnPos = { -379.18f, -373.19f, -369.06f };
    int rand;
    public int healthPoint;
    float moveSpeed;
    public static int killForDamage;
    public static int kill;
    public bool isDead;

    private int healthPointUpgrade;
    private bool hpUpgrade;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        rand = Random.Range(0, turnPos.Length);
        healthPoint = 100;
        killForDamage = 0;
        kill = 0;
        isDead = false;

    }
    void Update()
    {
        //print("police hp: " + healthPoint);
        healthPointUpgrade = fixAnim.healthPointUpgrade;
        hpUpgrade = fixAnim.hpUpgrade;
        healthPoint = Mathf.Clamp(healthPoint, 0, 1000000000);
        if (hpUpgrade)
        {
            healthPoint *= healthPointUpgrade;
            
        }
        

        moveSpeed = _zombieSpeed * Time.deltaTime;
        if (!isDead)
        {
            Vector3 zombieMove = new Vector3(0, 0, moveSpeed);
            transform.Translate(zombieMove);
        }
        else 
        {
            Vector3 zombieMove = new Vector3(0, 0, 0);
            transform.Translate(zombieMove);
        }
        

        float zombPos = transform.position.z;
        Vector3 vectorTurn = new Vector3(0, 0, turnPos[rand]);
        float vectorTurnZ = vectorTurn.z;

        if (Mathf.Abs(zombPos - vectorTurnZ) < 0.1f)
        {
            _anim.SetBool("turn", true);

        }


        if (healthPoint <= 0)
        {
            _anim.SetBool("dead", true);
        }

        

    }

    public void AnimComplited()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
        _anim.SetBool("walkNext", true);
    }

    public void DeadAnim()
    {
        Destroy(gameObject);
    }

    public void DeadAnimStart()
    {
        //kill++;
        //killForDamage++;
        isDead = true;
        gameManager.killCount++;
    }

    
}
