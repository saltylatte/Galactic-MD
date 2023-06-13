using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{

    public static bool mouseMode = true;
    public static bool Rotate;

    public float Rating;
    public float AverageRating = 0;
    float OverallRatings = 0;
    public int TreatedAliens = 0;
    public float earnedMoney = 0f;
    public float neededMoney = 500f;
    public float treatmentMoney = 0f;
    int days = 1;
    public Display DisplayScript;

    public int pflaster = 0;
    public int stolen_organs = 0;
    
    //Tools
    public bool Knife = false;
    public bool Saw = false;
    public bool Spritze = false;
    public bool Salbe = false;
    public bool Nadel = false;
    public bool Tweezers = false;
    public bool Pflaster = false;
    public bool Reibe = false;

    public static Dictionary<string, bool> Tools = new Dictionary<string, bool>();

    //Acid
    public int acid_code;
    public bool acid_bought;
    
    
    //
    public float collectedMoney;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Rating = 2.5f;
        Tools.Add("Knife",Knife);
        Tools.Add("Saw",Saw);
        Tools.Add("Spritze",Spritze);
        Tools.Add("Salbe",Salbe);
        Tools.Add("Nadel",Nadel);
        Tools.Add("Tweezers",Tweezers);
        Tools.Add("Pflaster",Pflaster);
        Tools.Add("Reibe",Reibe);
        AddPflaster(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(float money)
    {
        treatmentMoney += money;
        Debug.Log(treatmentMoney);
    }
    public void AddMoneyUpdate(float money)
    {
        earnedMoney += money;
        DisplayScript.UpdateProcessbar(earnedMoney, neededMoney);
    }


    public void AddPflaster(int add_pflaster)
    {
        pflaster += add_pflaster;
        
        DisplayScript.UpdatePflasterLabel(pflaster);
        
    }
    
    public static void SetTool(string ToolName)
    {
        Tools[ToolName] = true;

        foreach (string Tool in Tools.Keys)
        {
            if (Tools[Tool] == true&& Tool !=ToolName)
            {
                Tools[Tool] = false;
            }
        }
        
        Debug.Log("Knife "+ Tools["Knife"]);
        Debug.Log("Saw "+ Tools["Saw"]);
        Debug.Log("Spritze "+ Tools["Spritze"]);
        Debug.Log("Salbe "+ Tools["Salbe"]);
        Debug.Log("Nadel "+ Tools["Nadel"]);
        Debug.Log("Pflaster "+ Tools["Pflaster"]);
        Debug.Log("Reibe "+ Tools["Reibe"]);
    }
    
    
    

    public void SetRating(float r)
    {
        Rating += r;
        Debug.Log(Rating);
        if (Rating < 0)
        {
            Rating = 0;
        }
        if (Rating > 5)
        {
            Rating = 5;
        }
        DisplayScript.UpdateRatingLabel(Rating);
    }

    //Called on 'FinishTreatment'
    public void CalculateRating()
    {
        earnedMoney += treatmentMoney;
        treatmentMoney = 0;
        DisplayScript.UpdateProcessbar(earnedMoney, neededMoney);
        TreatedAliens += 1;
        OverallRatings += Rating;
        AverageRating = OverallRatings / TreatedAliens;
        DisplayScript.UpdateAverageRatingLabel(AverageRating);

        //Check if new Day
        if (TreatedAliens % 3 == 0 && TreatedAliens != 0)
        {
            /*if (earnedMoney >= neededMoney)
            {
                earnedMoney = earnedMoney - neededMoney;
                //Get Raumschiffteil
                
            }
            else
            {
                earnedMoney = earnedMoney;
            }
            //if new day double needed money
            neededMoney = neededMoney * 1.5f;
            
            DisplayScript.UpdateProcessbar(earnedMoney,neededMoney);
            */
            DisplayScript.UpdateDaysLabel(days+=1);
        }
        
    }
    
    
}
