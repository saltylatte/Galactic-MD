using System.Collections;
using System.Collections.Generic;
using UniGLTF;
using Unity.VisualScripting;
using UnityEngine;

public class WaitingRoom : MonoBehaviour
{

    public GlobalVariables GV;

    public List<GameObject> Level1Aliens;
    public List<GameObject> Level2Aliens;
    public List<GameObject> Level3Aliens;

    private int j;

    public float Level1Rating = 1.5f;
    public float Level2Rating = 2.5f;
    public float Level3Rating = 3.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DeactivateChildren()
    {
        foreach (var child in this.transform.GetChildren())
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnWaitingRoomOpened()
    {
        if (GV.TreatedAliens % 3 == 0)
        {
            Debug.Log("Hello");
            DeactivateChildren();
            
            if (GV.AverageRating <= Level1Rating)
            {
                for (int i = 0; i < 4; i++)
                {
                    j = Random.Range(0, Level1Aliens.Count);
                    if (Level1Aliens[j].activeSelf == false)
                    {
                        Level1Aliens[j].SetActive(true);    
                    }
                    else
                    {
                        i--;
                    }
                    
                }

            }
            else if (GV.AverageRating <= Level3Rating && GV.AverageRating > Level2Rating)
            {
                for (int i = 0; i < 4; i++)
                {
                    j = Random.Range(0, Level2Aliens.Count);
                    if (Level2Aliens[j].activeSelf == false)
                    {
                        Level2Aliens[j].SetActive(true);    
                    }
                    else
                    {
                        i--;
                    }
                    
                }
            }
            else if (GV.AverageRating > Level3Rating)
            {
                for (int i = 0; i < 4; i++)
                {
                    
                    j = Random.Range(0, Level3Aliens.Count);
                    if (Level3Aliens[j].activeSelf == false)
                    {
                        Level3Aliens[j].SetActive(true);    
                    }
                    else
                    {
                        i--;
                    }
                    
                }
            }
        }
            

        //GV.AllRatings = GV.Rating;
        //GV.GeneralRating = GV.AllRatings / 3;
        GV.SetRating(5f);
        GV.SetRating(-2.5f);
        
        
        
    }
    
}
