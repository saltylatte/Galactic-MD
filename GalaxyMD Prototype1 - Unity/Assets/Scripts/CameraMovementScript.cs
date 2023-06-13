using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private bool Rotate = false;
    private Quaternion targetRotation = new Quaternion();
    private Vector3 targetPosition = new Vector3();
    private bool targetPosReached = true;
    private bool targetRotReached = true;
    
    private Vector3 MiddleRot = new Vector3(0f,180f,0f);
    private Vector3 LeftRot = new Vector3(0f,135f,0f);
    private Vector3 RightRot = new Vector3(0f,225f,0f);

    private Vector3 MiddlePos = new Vector3(0, 2.35f, -3f);
    private Vector3 OuterPos = new Vector3(0, 2.25f, 2f);

    public GameObject finishButton;
    public GameObject tube;


    public GameObject Acid_Input;
    private GlobalVariables GV;
    
    // Start is called before the first frame update
    void Start()
    { 
        transform.rotation = Quaternion.Euler(MiddleRot);
        GV = GameObject.FindWithTag("GlobalVariables").GetComponent<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {

        //Zoom
        if (transform.rotation == Quaternion.Euler(MiddleRot)&& transform.position.z <= MiddlePos.z+0.1f&&transform.position.z >= -5.1f)
        {
            float scroll = Input.GetAxis ("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * 1, Space.Self);
            if (transform.position.z >= -3)
            {
                transform.position = MiddlePos;
            }
            if (transform.position.z <= -5)
            {
                transform.position = new Vector3(MiddlePos.x,MiddlePos.y,MiddlePos.z-2);
            }
        }
        
                    
        //Check if drag to rotate
        if (Input.GetMouseButtonDown(0))
        {
            Rotate = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Rotate = false;
        }
        
        CameraMove();
        
    }

    private void CameraMove()
    {
        if (targetRotReached == false)
        {
            if (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.05f);
            } 

            else
            {
                targetRotReached = true;
            }
            
        }
        if (targetPosReached == false)
        {
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
            }
            else
            {
                targetPosReached = true;
            }
        }

    }
    
    
    public void ButtonLeft()
    {
        print(Rotate);
        if (Rotate == false)
        {
            if (transform.rotation == Quaternion.Euler(MiddleRot))
            {
                finishButton.SetActive(false);
                
                targetRotation = Quaternion.Euler(LeftRot);
                targetRotReached = false;

                targetPosition = OuterPos;
                targetPosReached = false;
                
                Acid_Input.SetActive(false);

                    

            }
            else if (transform.rotation == Quaternion.Euler(RightRot))
            {
                finishButton.SetActive(true);
                
                targetRotation = Quaternion.Euler(MiddleRot);
                targetRotReached = false;

                targetPosition = MiddlePos;
                targetPosReached = false;
                if (GV.acid_bought == true)
                {
                    Acid_Input.SetActive(true);
                }
                
            }
        }
    }

    public void ButtonRight()
    {
        if (Rotate == false)
        {
            if (transform.rotation == Quaternion.Euler(MiddleRot))
            {
                finishButton.SetActive(false);
                
                targetRotation = Quaternion.Euler(RightRot);
                targetRotReached = false;

                targetPosition = OuterPos;
                targetPosReached = false;
                Acid_Input.SetActive(false);

            }
            else if (transform.rotation == Quaternion.Euler(LeftRot))
            {
                finishButton.SetActive(true);
                
                targetRotation = Quaternion.Euler(MiddleRot);
                targetRotReached = false;
                
                targetPosition = MiddlePos;
                targetPosReached = false;
                
                if (GV.acid_bought == true)
                {
                    Acid_Input.SetActive(true);
                }
            }
        }
    }
    
    public void MonsterMode()
    {
        targetPosition =  new Vector3(0f,2.35f, -3f);
        targetPosReached = false;
        tube.SetActive(false);
    }
    
    
}
