using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class checkPlatform : MonoBehaviour
{

    public static bool isPc = false;
    public static bool isMobile = false;


    public void ButtonPc() 
    {
        isPc = true;
        isMobile = false;
    }
    public void ButtonMobile() 
    {
        isMobile = true;
        isPc = false;
    }

    
}
