using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class camera : MonoBehaviour
{

    float mouseX = 0;
    float mouseY = 0;

    private bool isPc;
    private bool isMobile;
    void Update()
    {
        isPc = checkPlatform.isPc;
        isMobile = checkPlatform.isMobile;
        if (isPc)
        {
            float valueY = Input.GetAxis("Mouse X") * Time.deltaTime * 100;
            float valueX = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;
            mouseX += valueX;
            mouseY += valueY;
            mouseX = Mathf.Clamp(mouseX, -15, 15);
            mouseY = Mathf.Clamp(mouseY, -45, 45);
            transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);
        }
        else if (isMobile) 
        {
            if (Input.touchCount > 0) 
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(touch.fingerId)) 
                {
                    float touchDeltaX = touch.deltaPosition.x * 0.08f;
                    float touchDeltaY = touch.deltaPosition.y * 0.08f;

                    mouseX -= touchDeltaY;
                    mouseY -= touchDeltaX;

                    mouseX = Mathf.Clamp(mouseX, -15, 15);
                    mouseY = Mathf.Clamp(mouseY, -45, 45);

                    transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);
                }
            }
        }
        
    }
}
