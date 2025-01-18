using UnityEngine;
using UnityEngine.EventSystems;


public class playerMove : MonoBehaviour
{
    Rigidbody rb;
    public float  speed = 13f;

    public GameObject head;
    float sensX = 110f;
    float sensY = 110f;
    float mouseX = 0;
    float mouseY = 0;
    public GameObject rightHand;
    

    private Animator _anim;
    public static float moveInput;

    private bool isPc;
    private bool isMobile;
    public static float mobileMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        mobileMove = 0;
        
    }

    
    void Update()
    {
        isPc = checkPlatform.isPc;
        isMobile = checkPlatform.isMobile;
        if (isPc) 
        {
            mobileMove = 0;
            moveInput = Input.GetAxis("Horizontal");
        float posZ = transform.position.z - moveInput * speed * Time.deltaTime;
        if (posZ >= -357.64f)
        {
            posZ = -357.64f;
        }
        else if (posZ <= -394.12f) 
        {
            posZ = -394.12f;
        }
        
        Vector3 movement = new Vector3(-354.8f, 1, posZ);
        transform.position = movement;

        float valueY = Input.GetAxis("Mouse X") * Time.deltaTime * sensY;
        float valueX = Input.GetAxis("Mouse Y") * Time.deltaTime * sensX;
        mouseX += valueX;
        mouseY += valueY;
        mouseX = Mathf.Clamp(mouseX, -15, 15);
        mouseY = Mathf.Clamp(mouseY, -45, 45);
        head.transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);

        float fixRightHand = 82.9f + -mouseY * 1.1f;
        rightHand.transform.localEulerAngles = new Vector3(fixRightHand, 0, 0);
        }
        if (isMobile) 
        {
            moveInput = 0;
            float posZ = transform.position.z - mobileMove * speed * Time.deltaTime;
            if (posZ >= -357.64f)
            {
                posZ = -357.64f;
            }
            else if (posZ <= -394.12f)
            {
                posZ = -394.12f;
            }

            Vector3 movement = new Vector3(-354.8f, 1, posZ);
            transform.position = movement;


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    float touchDeltaX = touch.deltaPosition.x * 0.1f;
                    float touchDeltaY = touch.deltaPosition.y * 0.1f;

                    mouseX -= touchDeltaY;
                    mouseY -= touchDeltaX;

                    mouseX = Mathf.Clamp(mouseX, -15, 15);
                    mouseY = Mathf.Clamp(mouseY, -45, 45);

                    head.transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);

                    float fixRightHand = 82.9f + -mouseY * 1.1f;
                    rightHand.transform.localEulerAngles = new Vector3(fixRightHand, 0, 0);

                }
            }

        }


        
        _anim.SetBool("isWalkRight", moveInput > 0 || mobileMove > 0);
        _anim.SetBool("isWalkLeft", moveInput < 0 || mobileMove < 0);

    }

    public void MobileRight() 
    {
        mobileMove = 1;
    }

    public void MobileLeft()
    {
        mobileMove = -1;
    }

    public void MobileStop()
    {
        mobileMove = 0;
    }

    
    
}
