using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CameraScriptTest : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    public float xRotation;
    public float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
        
        //move freely
        Cursor.visible = true;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, 135f, 225f);
        //xRotation -= Mathf.Clamp(mouseY,-90f,+90f);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,0f,+20f);
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        
    }


    
    
    
}
