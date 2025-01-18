
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject zombieDefault;
    public GameObject zombiePolice;
    public GameObject zombieBuilder;
    public GameObject zombieBoss;
    Vector3 SpawnPos;

    void Start()
    {
        SpawnPos = transform.position;
        SpawnPos.y = 1.01f;
        SpawnDefault();
        Invoke("SpawnPolice", 5f);
        SpawnBuilder();
        Invoke("SpawnBoss", 9f);

    }

    public void SpawnDefault() 
    {
        Instantiate(zombieDefault, SpawnPos, Quaternion.identity);
        Invoke("SpawnDefault", Random.Range(3, 6));
    }

    public void SpawnPolice()
    {
        Vector3 spawnPosPolice = SpawnPos;
        spawnPosPolice.x = -257.56f;
        Instantiate(zombiePolice, spawnPosPolice, Quaternion.identity);
        Invoke("SpawnPolice", Random.Range(7, 11));
    }

    public void SpawnBuilder() 
    {
        Vector3 spawnPosBuilder = SpawnPos;
        spawnPosBuilder.x = -254.51f;
        spawnPosBuilder.y = 1.5f;
        Instantiate(zombieBuilder, spawnPosBuilder, Quaternion.identity);
        Invoke("SpawnBuilder", Random.Range(5, 9));
    }
    public void SpawnBoss()
    {
        Vector3 spawnPosBoss = new Vector3(-192.23f, 1.01f, -356.8f);
        Instantiate(zombieBoss, spawnPosBoss, zombieBoss.transform.rotation);
        Invoke("SpawnBoss", Random.Range(25,32));
    }

}
