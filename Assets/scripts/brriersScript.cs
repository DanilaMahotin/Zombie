using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class brriersScript : MonoBehaviour
{
    public static int heart = 0;


    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadSaveHeart();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject childObject = other.gameObject;
        GameObject parentObject = childObject.transform.root.gameObject;

        if (parentObject.gameObject.layer == 7) 
        {
            
            Destroy(parentObject);
            heart++;
            SaveHeartScore();
        }
    }

    public void SaveHeartScore()
    {
        YandexGame.savesData.heartScore = heart;
        YandexGame.SaveProgress();
    }
    public void LoadSaveHeart()
    {
        heart = YandexGame.savesData.heartScore;
    }
}
