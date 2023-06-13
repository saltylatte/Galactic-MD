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


    public Texture2D OrganCursorTexture;
    public Texture2D KnifeTexture;
    
    

    public bool OrganInHand = false;
    public bool treated = false; 
    public GlobalVariables GV;

    public Material DefaultMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(camera.name);
        buttonBack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        





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

                    hit.transform.gameObject.SetActive(false);
                    Cursor.SetCursor(OrganCursorTexture,new Vector2(16,16), CursorMode.Auto);
                    OrganInHand = true;
                }

                if (hit.transform.name == "ConveyerBelt" && OrganInHand == true)
                {
                    OrganInHand = false;
                    Cursor.SetCursor(KnifeTexture,new Vector2(16,16), CursorMode.Auto);
                    
                    GV.AddMoney(350f);
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
                if (Tools[Saw].activeSelf == true)
                {
                    //if (Tools[Tool.Saw].activeSelf == true)
                    if (hit.transform.name == "ArmL" || hit.transform.name == "ArmR"|| hit.transform.name == "ArmDownL"|| hit.transform.name == "ArmDownR"|| hit.transform.name == "AntennaL"|| hit.transform.name == "AntennaR"|| hit.transform.name == "AntennaM")
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
                else if (Tools[Nadel].activeSelf )
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
                else if (Tools[Salbe].activeSelf && hit.transform.name == "AntennaL")
                {

                    /*BodyClose.SetActive(true);
                    BodyOpen.SetActive(false);

                    activeTool = Tools[Salbe];
                    ToolEndPos = hit.transform.position;
                    ToolStartPos = activeTool.transform.position;


                    Hit = hit.transform.gameObject;
                    werf = true;
                    */
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material = DefaultMaterial;
                        //Debug.Log("Salbe wirkt");
                    treated = true;
                }
            }    
        }


        
    }



    public void FinishTreatment()
    {
        if (treated == true)
        {
            GV.AddMoney(150f);
            
        }
    }
    
}

