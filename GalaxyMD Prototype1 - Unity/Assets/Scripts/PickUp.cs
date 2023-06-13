using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PickUp : MonoBehaviour
{
    public bool add = false;
    public GameObject TableObject;
    public GameObject OnPlayerObject;
    public Vector3 StartingPosition;
    public Quaternion StartingRotation;
    [SerializeField] public Texture2D CursorTexture;
    
    
    public bool isEquipped = false;

    
    public PickUp[] Tools;

    public PickUp myself;

    public StaticCameraScript camScript;
    
    
    // Start is called before the first frame update
    void Start()
    {
        TableObject = transform.GetChild(0).gameObject;
        OnPlayerObject.SetActive(false);
        StartingPosition = myself.TableObject.transform.position;
        StartingRotation = myself.TableObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (myself.add == true)
        {

            if (!GlobalVariables.mouseMode)
            {
                myself.moveToHand(myself);    
            }
            else
            {
                //CursorFromTable();
            }
            
        }
        else
        {
            if (!GlobalVariables.mouseMode)
            {
                myself.moveToTable(myself);
            }
            else
            {
                CursorToTable();
            }
        }
    }

    public void moveToHand(PickUp pu)
    {
        
        if (pu.TableObject.transform.position != pu.OnPlayerObject.transform.position)
        {
            pu.TableObject.transform.position = Vector3.Lerp(pu.TableObject.transform.position, pu.OnPlayerObject.transform.position, 50f*Time.deltaTime);
            pu.TableObject.transform.rotation = Quaternion.Lerp(TableObject.transform.rotation, pu.OnPlayerObject.transform.rotation, 50f*Time.deltaTime);
            pu.isEquipped = true;
        }
        else
        {
            pu.TableObject.SetActive(false);
            pu.OnPlayerObject.SetActive(true);
            //camScript.Tool = pu.gameObject;
        }
    }
    
    public void moveToTable(PickUp pu)
    {
        if (pu.TableObject.transform.position != pu.StartingPosition)
        {
            //Debug.Log(pu.TableObject);
            pu.TableObject.SetActive(true);
            pu.OnPlayerObject.gameObject.SetActive(false);
            pu.TableObject.transform.position = Vector3.Lerp(pu.TableObject.transform.position, pu.StartingPosition, 50f*Time.deltaTime);
            pu.TableObject.transform.rotation = Quaternion.Lerp(pu.TableObject.transform.rotation, pu.StartingRotation, 50f*Time.deltaTime);
            pu.isEquipped = false;
        }
        
        
        
    }

    public void CursorFromTable()
    {
        Cursor.SetCursor(CursorTexture,new Vector2(16,16), CursorMode.Auto);
        TableObject.SetActive(false);
        OnPlayerObject.SetActive(true);
        isEquipped = true;
    }

    public void CursorToTable()
    {
        TableObject.SetActive(true);
        OnPlayerObject.SetActive(false);
        isEquipped = false;
    }
    
    
    public void PickUpDrop()
    {
        
        foreach (PickUp pu in Tools)
        {
            if (pu.isEquipped == true && pu != myself)
            {
                pu.add = false;
                //Debug.Log(pu.TableObject);
                
            }
        }

        if (!GlobalVariables.mouseMode)
        {
            myself.moveToHand(myself);
        }
        

        if (myself.add == false)
        {
            CursorFromTable();
            myself.add = true;
        }
        else
        {
            CursorToTable();
            myself.add = false;
        }

    }
    
}
