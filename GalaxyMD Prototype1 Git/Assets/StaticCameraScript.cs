using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StaticCameraScript : MonoBehaviour
{
    //references to Buttons
    public GameObject buttonL;
    public GameObject buttonR;
    public GameObject tube;
    public GameObject buttonBack;
    public Camera camera;
    public PhysicsRaycaster rayCast;
    public ParticleSystem BloodParticles;
    
    public Vector3 middle = new Vector3(0f,180f,0f);
    public Vector3 left = new Vector3(0f,135f,0f);
    public Vector3 right = new Vector3(0f,225f,0f);

    public Quaternion targetRotation = new Quaternion();
    public Vector3 targetPosition = new Vector3();
    public bool targetPosReached = true;
    public bool targetRotReached = true;

    public bool monsterMode = false;

    public GameObject[] Tools;

    //reference for array fuck those enums
    private const int Knife = 0;
    private const int Saw = 1;
    private const int Spritze = 2;
    private const int Salbe = 3;
    private const int Nadel = 4;
    
    public bool werf = false;
    public Vector3 ToolStartPos;
    public Vector3 ToolEndPos;
    public GameObject HandLeft;
    public GameObject HandRight;
    public GameObject Hit;

    public GameObject BodyOpen;
    public GameObject BodyClose;
    public GameObject activeTool;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(camera.name);
        buttonBack.SetActive(false);
        transform.rotation = Quaternion.Euler(middle);
        targetPosition = new Vector3(0f,2.25f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
        CameraMove();
        
        
        //Werfen
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -100f;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        Debug.DrawRay(transform.position, transform.position-mousePos, Color.black);
        
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }      
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse");
            TakeOrgan();
        }  
        
        
        //Boomerang
        if (ToolStartPos != Vector3.zero)
        {
            if (werf == true)
            {
                activeTool.transform.position = Vector3.Lerp(activeTool.transform.position, ToolEndPos, 50f*Time.deltaTime);
            }
            if (werf == true && activeTool.transform.position == ToolEndPos)
            {
                if (activeTool == Tools[Saw])
                {
                    Hit.GetComponent<Renderer>().enabled = false;
                }    
                    
                werf = false;
            }
            if (werf == false)
            {
                activeTool.transform.position = Vector3.Lerp(activeTool.transform.position, ToolStartPos, 50f*Time.deltaTime);
            }
            if (werf == false && activeTool.transform.position == ToolStartPos)
            {
                ToolStartPos = Vector3.zero;
            }
        }
    }


    void TakeOrgan()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (monsterMode == true)
            {
                if (hit.transform.name == "Organ1" || hit.transform.name == "Organ2" || hit.transform.name == "Organ3" || hit.transform.name == "Organ4")
                {

                    hit.transform.position = HandRight.transform.position;

                }
            }
        }
    }
    
    
    void CastRay()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (monsterMode == true)
            {
                if (hit.transform.name == "ArmL" || hit.transform.name == "ArmR"|| hit.transform.name == "ArmDownL"|| hit.transform.name == "ArmDownR"|| hit.transform.name == "AntennaL"|| hit.transform.name == "AntennaR"|| hit.transform.name == "AntennaM")
                {
                    //if (Tools[Tool.Saw].activeSelf == true)
                    if (Tools[Saw].activeSelf == true)
                    {
                        
                        //hit.transform.gameObject.GetComponent<Renderer>().enabled = false;
                        
                        //Particle Effect
                        ParticleSystem p = new ParticleSystem();
                        p = BloodParticles;
                        p.transform.position = hit.transform.GetChild(0).position;
                        p.transform.rotation = hit.transform.GetChild(0).rotation;
                        Instantiate(p);
                        p.Play();


                        activeTool = Tools[Saw];
                        ToolEndPos = hit.transform.position;
                        ToolStartPos = activeTool.transform.position;
                        werf = true;

                        Hit = hit.transform.gameObject;

                    }
                    
                }
                else if (Tools[Knife].activeSelf)
                {
                    if (hit.transform.name == "BodyClose")
                    {
                        BodyClose.SetActive(false);
                        BodyOpen.SetActive(true);
                        
                        activeTool = Tools[Knife];
                        ToolEndPos = hit.transform.position;
                        ToolStartPos = activeTool.transform.position;
                        

                        Hit = hit.transform.gameObject;
                        werf = true;
                    }
                    
                }
                else if (Tools[Nadel].activeSelf)
                {
                    if (hit.transform.name == "BodyOpen" || hit.transform.name == "Organ1"|| hit.transform.name == "Organ2"|| hit.transform.name == "Organ3"|| hit.transform.name == "Organ4")
                    {
                        
                        BodyClose.SetActive(true);
                        BodyOpen.SetActive(false);
                        
                        activeTool = Tools[Nadel];
                        ToolEndPos = hit.transform.position;
                        ToolStartPos = activeTool.transform.position;
                        

                        Hit = hit.transform.gameObject;
                        werf = true;
                        
                    }
                    
                }
            }    
        }


        
    }
    
    
    
    public void CameraMove()
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
        if (transform.rotation == Quaternion.Euler(middle))
        {
            targetRotation = Quaternion.Euler(left);
            targetRotReached = false;
            
            targetPosition = new Vector3(0,2.25f,2f);
            targetPosReached = false;
        }
        else if (transform.rotation == Quaternion.Euler(right))
        {
            targetRotation = Quaternion.Euler(middle);
            targetRotReached = false;

            targetPosition = new Vector3(0,2.8f,-1.8f);
            targetPosReached = false;
        }
        
        //rotateAround
        
    }

    public void ButtonRight()
    {
        if (transform.rotation == Quaternion.Euler(middle))
        {
            targetRotation = Quaternion.Euler(right);
            targetRotReached = false;
            targetPosition = new Vector3(0,2.25f,2f);
            targetPosReached = false;
        }
        else if (transform.rotation == Quaternion.Euler(left))
        {
            targetRotation = Quaternion.Euler(middle);
            targetRotReached = false;
            
            targetPosition = new Vector3(0,2.8f,-1.8f);
            targetPosReached = false;
        }
        
    }

    public void MonsterMode()
    {
        if (monsterMode == false)
        {
            targetPosition =  new Vector3(0f,2.8f,-1.8f);
            targetPosReached = false;
            //buttonL.SetActive(false);
            //buttonR.SetActive(false);
            //buttonBack.SetActive(true);
            tube.SetActive(false);
            monsterMode = true;
        }
        else
        {
            targetPosition =  new Vector3(0f,2.25f,2f);
            targetPosReached = false;
            //buttonL.SetActive(true);
            //buttonR.SetActive(true);
            //buttonBack.SetActive(false);
            tube.SetActive(true);
            monsterMode = false;
        }


        //Debug.Log(targetPosition);
        //Debug.Log(transform.position);
    }


}
