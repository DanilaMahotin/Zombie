using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBuilder : MonoBehaviour
{
    private float _zombieSpeed = 4f;
    Animator _anim;
    private float[] turnPos = { -380.84f, -374.93f, -370.09f };
    int rand;
    public int healthPoint;
    float moveSpeed;
    public static int killForDamage = 0;
    public static int kill = 0;
    public bool isDead = false;

    private int healthPointUpgrade;
    private bool hpUpgrade;




    private void Start()
    {
        _anim = GetComponent<Animator>();
        rand = Random.Range(0, turnPos.Length);
        healthPoint = 40;


    }
    void Update()
    {
        //print("builder hp: " + healthPoint);
        healthPointUpgrade = fixAnim.healthPointUpgrade;
        hpUpgrade = fixAnim.hpUpgrade;
        healthPoint = Mathf.Clamp(healthPoint, 0, 1000000000);
        if (hpUpgrade) 
        {
            healthPoint *= healthPointUpgrade;
            fixAnim.hpUpgrade = false;
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
