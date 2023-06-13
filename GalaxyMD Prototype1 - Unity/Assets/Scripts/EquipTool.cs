using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : MonoBehaviour
{
    public Texture2D CursorTexture;
    private GameObject TableObject;

    public EquipTool[] Tools;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        TableObject = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Euip()
    {
        //if (this.TableObject.activeSelf == false)
        //{
        //    Dequip();
        //}
        
        foreach (EquipTool Tool in Tools)
        {
            if (Tool.gameObject.activeSelf == true)
            {
                if (Tool.TableObject.activeSelf == false)
                {
                    Tool.Dequip();
                }
            }        
                
        }
        
        if (TableObject.activeSelf == true)
        {
            Cursor.SetCursor(CursorTexture,new Vector2(16,16), CursorMode.Auto);
            
            TableObject.SetActive(false);
            GlobalVariables.SetTool(TableObject.name);
            Debug.Log(TableObject.name);
        }
        else
        {
            TableObject.SetActive(true);
        }
    }

    public void Dequip()
    {
        TableObject.SetActive(true);
    }
}
