using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InteractiveToolScript : MonoBehaviour
{
    public Camera camera;
    public ParticleSystem BloodParticles;

    public GlobalVariables GV;
    public Material DefaultMaterial;

    public GameObject BodyClosed;
    public GameObject BodyOpen;

    //Try Variables
    Vector3 mousePosition;
    Vector3 ParasiteMousePos;
    GameObject Parasite;
    public GameObject Trash;

    public GameObject PflasterDecal;
    public GameObject PflasterTable;
    private Display display;

    public Texture2D DirtBrushTexture;
    //public Texture2D DirtMask; 



    private States state = States.NOTDRAGGING;

    enum States
    {
        NOTDRAGGING,
        DRAGGING
    }


    //Trashy Variables
    public bool OrganInHand = false;
    public Texture2D KnifeTexture;
    public Texture2D OrganTexture;
    private int layer_mask = 1;



    public float OrganPrice = 150f;
    public float RatingPerOrgan = -1f;
    public float RatingPerShroomCut = 0.5f;
    public float ShroomCutMoney = 15f;
    public float RatingPerCut = -1f;
    public float RatingSaw = -0.5f;
    public float SalbeRating = 0.5f;
    public float SalbeMoney = 15f;
    public float WoundRating = 0.85f;
    public float WoundMoney = 25f;
    public float ShroomReibeRating = 0.11f;
    public float ShroomReibeMoney = 10f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            mousePosition = Input.mousePosition - GetMousePos();

        }

        if (Input.GetMouseButton(0))
        {
            if (GlobalVariables.Tools["Reibe"] == true)// || GlobalVariables.Tools["Salbe"] == true)
            {
                CastRay();
            }
        }

        if (GlobalVariables.Tools["Tweezers"] == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                state = States.NOTDRAGGING;
                if (Parasite.transform.parent.name != "JarPrototype" || Parasite.transform.parent.name != "ParasitePos")
                {
                    Parasite.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }

            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            TakeOrgan();
        }


        if (state == States.DRAGGING)
        {
            ParasiteMousePos = camera.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            Parasite.transform.position =
                new Vector3(ParasiteMousePos.x, ParasiteMousePos.y, Parasite.transform.position.z);
            Parasite.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (GlobalVariables.Tools["Tweezers"] == false)
        {
            Trash.transform.position = new Vector3(-2.5f, 1.6f, -3.4f);
        }
        else
        {
            Trash.transform.position = new Vector3(-2.3f, 2f, -7);
        }



    }

    private Vector3 GetMousePos()
    {
        if (Parasite != null)
        {
            return camera.WorldToScreenPoint(Parasite.transform.position);
        }

        return new Vector3(0, 0, 0);
    }

    void TakeOrgan()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.name == "Organ1" || hit.transform.name == "Organ2" || hit.transform.name == "Organ3" ||
                hit.transform.name == "Organ4")
            {
                hit.transform.gameObject.SetActive(false);
                Cursor.SetCursor(OrganTexture, new Vector2(16, 16), CursorMode.Auto);
                OrganInHand = true;
            }

            if (hit.transform.name == "ConveyerBelt" && OrganInHand == true)
            {
                OrganInHand = false;
                Cursor.SetCursor(KnifeTexture, new Vector2(16, 16), CursorMode.Auto);
                GV.SetRating(RatingPerOrgan);
                GV.AddMoneyUpdate(OrganPrice);
                GV.stolen_organs += 1;
                if (GV.stolen_organs > 2)
                {
                    display.FinishTreatment();
                    GV.SetRating(-5f);

                }
            }
        }
    }





    void CastRay()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {
            if (GlobalVariables.Tools["Knife"] == true)
            {
                if (hit.transform.gameObject.GetComponent<BodypartScript>()?.Knifeable == true)
                {
                    Debug.Log("Knife");
                    KnifeAction(hit);
                }
            }

            if (GlobalVariables.Tools["Saw"] == true)
            {
                if (hit.transform.gameObject.GetComponent<BodypartScript>()?.Sawable == true)
                {
                    Debug.Log("Saw");
                    SawAction(hit);
                }
            }

            if (GlobalVariables.Tools["Spritze"] == true)
            {
                SprizeAction(hit);
            }

            if (GlobalVariables.Tools["Salbe"] == true)
            {
                if (hit.transform.gameObject.GetComponent<BodypartScript>()?.Salbeable == true)
                {
                    Debug.Log("Salbe");
                    OldSalbeAction(hit);
                }
            }

            if (GlobalVariables.Tools["Nadel"] == true)
            {
                if (hit.transform.gameObject.GetComponent<BodypartScript>()?.Nadelable == true)
                {
                    Debug.Log("Nadel");
                    NadelAction(hit);
                }
            }

            if (GlobalVariables.Tools["Tweezers"] == true)
            {
                if (hit.transform.gameObject.CompareTag("Parasite"))
                {
                    Debug.Log("Tweezers");
                    TweezerAction(hit);
                }
            }

            if (GlobalVariables.Tools["Pflaster"] == true)
            {
                Debug.Log("Pflaster");
                PflasterAction(hit);
            }

            if (GlobalVariables.Tools["Reibe"] == true)
            {
                if (hit.transform.CompareTag("ShroomReibe"))
                {
                    Debug.Log("Reibe");
                    ReibeAction(hit);
                }
            }
        }
    }


    void KnifeAction(RaycastHit hit)
    {

        hit.transform.gameObject.SetActive(false);
        if (hit.transform.CompareTag("ShroomCut"))
        {
            GV.SetRating(RatingPerShroomCut);
            GV.AddMoney(ShroomCutMoney);
        }
        else if (hit.transform.name == "Lappen")
        {
            BodyClosed = hit.transform.gameObject;
            GV.SetRating(RatingPerCut);
        }


    }


    void SawAction(RaycastHit hit)
    {


        ParticleSystem p = new ParticleSystem();
        p = BloodParticles;
        p.transform.position = hit.transform.GetChild(0).position;
        p.transform.rotation = hit.transform.GetChild(0).rotation;
        Instantiate(p);
        p.Play();

        hit.transform.gameObject.SetActive(false);

        //Rating Stuff
        GV.SetRating(RatingSaw);

    }

    void OldSalbeAction(RaycastHit hit)
    {
        hit.transform.gameObject.GetComponent<MeshRenderer>().material = DefaultMaterial;
        hit.transform.gameObject.GetComponent<BodypartScript>().Salbeable = false;
        //Rating Stuff
        GV.SetRating(SalbeRating);
        GV.AddMoney(SalbeMoney);
    }


    void SalbeAction(RaycastHit hit)
    {

        Material material = hit.transform.gameObject.GetComponent<Renderer>().material;
        Texture2D OriginalDirtMask = (Texture2D)material.GetTexture("_MainTex");

        Texture2D DirtMask = new Texture2D(OriginalDirtMask.width, OriginalDirtMask.height);
        DirtMask = Instantiate(OriginalDirtMask);

        Vector2 textureCoord = hit.textureCoord;

        Debug.Log(DirtMask.name);

        int pixelX = (int)(textureCoord.x * DirtMask.width);
        int pixelY = (int)(textureCoord.y * DirtMask.height);


        Vector2Int PaintPixelPosition = new Vector2Int(pixelX, pixelY);

        Debug.Log("UV: " + textureCoord + "; Pixels: " + PaintPixelPosition);


        int pixelXOffset = pixelX - (DirtBrushTexture.width / 2);
        int pixelYOffset = pixelY - (DirtBrushTexture.height / 2);


        /*DirtMask.SetPixel(
            pixelX,
            pixelY,
            //new Color(0, pixelDirtMask.g * pixelDirt.g, 0)
            new Color(0,0,0)
        );*/


        for (int x = 0; x < DirtBrushTexture.width; x++)
        {
            for (int y = 0; y < DirtBrushTexture.height; y++)
            {
                Color pixelDirt = DirtBrushTexture.GetPixel(x, y);
                Color pixelDirtMask = DirtMask.GetPixel(pixelXOffset + x, pixelYOffset + y);

                DirtMask.SetPixel(
                    pixelXOffset + x,
                    pixelYOffset + y,
                    new Color(0, pixelDirtMask.g * pixelDirt.g, 0)
                    //new Color(0,0,0)
                );



            }


        }

        material.SetTexture("_MainTex", DirtMask);
        DirtMask.Apply();


        
        /*hit.transform.gameObject.GetComponent<MeshRenderer>().material = DefaultMaterial;
        //Rating Stuff
        GV.SetRating(SalbeRating); 
        GV.AddMoney(SalbeMoney);
        */
    }


void SprizeAction(RaycastHit hit)
    {
        hit.transform.gameObject.GetComponentInParent<Animator>();
    }
    
    

    void NadelAction(RaycastHit hit)
    {
        BodyClosed.SetActive(true);
    }

    void TweezerAction(RaycastHit hit)
    {
        state = States.DRAGGING; 
        Parasite = hit.transform.gameObject; 
        Debug.Log(Parasite.name);
        
    }

    void PflasterAction(RaycastHit hit)
    {
        if (GV.pflaster > 0)
        {
            if (hit.transform.CompareTag("Wound"))
            {
                Debug.Log("Wound");
                GameObject p;
                p = Instantiate(PflasterDecal, new Vector3(hit.point.x, hit.point.y,hit.point.z), new Quaternion(0f,180f,0f,0f));
                p.transform.parent = hit.transform.parent;
                hit.transform.gameObject.SetActive(false);
                GV.SetRating(WoundRating);
                GV.AddMoney(WoundMoney);
            }
            else if (hit.transform.gameObject.GetComponent<BodypartScript>().Pflasterable == true)
            {
                GameObject p;
                p = Instantiate(PflasterDecal, new Vector3(hit.point.x, hit.point.y,hit.point.z), new Quaternion(0f,180f,0f,0f));
                p.transform.parent = hit.transform.parent;
            }

            GV.AddPflaster(-1);
            
        }
        else
        {
            PflasterTable.SetActive(false);
        }
            

 
        
    }

    void ReibeAction(RaycastHit hit)
    {

        hit.transform.gameObject.SetActive(false); 
        GV.SetRating(ShroomReibeRating); 
        GV.AddMoney(ShroomReibeMoney);
        
    }


    

}
