using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    public int reibe_price;
    public int pflaster_price;
    public int needle_price;
    public int tweezers_price;
    public int acid_price;


    public GameObject RocketShipShop;
    public GameObject ReibeShop;
    public GameObject PflasterShop;
    public GameObject NeedleShop;
    public GameObject TweezersShop;
    public GameObject AcidShop;
    
    
    public GameObject ReibeTable;
    public GameObject PflasterTable;
    public GameObject NeedleTable;
    public GameObject TweezersTable;

    public GameObject InputField;
    
    public GameObject JarTable;

    private GlobalVariables GV;

    private Display display;
    
    public void Start()
    {
        GV = GameObject.FindWithTag("GlobalVariables").GetComponent<GlobalVariables>();
        display = GameObject.FindWithTag("DisplayScript").GetComponent<Display>();
    }

    public void On_Rocketship_Buy()
    {
        if (GV.earnedMoney >= GV.neededMoney)
        {
            GV.AddMoneyUpdate(-GV.neededMoney);
            GV.neededMoney *= 1.5f;
            display.UpdateProcessbar(GV.earnedMoney,GV.neededMoney);
            RocketShipShop.transform.GetChild(0).GetComponent<Text>().text = "Todays Spaceship Component: \n This Component will help you to leave the Planet after the 7 Days you are supposed to be here. \n"+GV.neededMoney+"$";
        }
    }
    public void On_Reibe_Buy()
    {
        if (GV.earnedMoney >= reibe_price)
        {
            ReibeTable.SetActive(true);
            ReibeShop.SetActive(false);
            GV.AddMoneyUpdate(-reibe_price);
        }
    }
    
    public void On_Pflaster_Buy()
    {
        if (GV.earnedMoney >= pflaster_price)
        {
            PflasterTable.SetActive(true);
            GV.AddPflaster(+1);
            GV.AddMoneyUpdate(-pflaster_price);
        }
    }    
    public void On_Needle_Buy()
    {
        if (GV.earnedMoney >= needle_price)
        {
            NeedleTable.SetActive(true);
            NeedleShop.SetActive(false);
            GV.AddMoneyUpdate(-needle_price);
        }
    }    
    public void On_Tweezers_Buy()
    {
        if (GV.earnedMoney >= tweezers_price)
        {
            TweezersTable.SetActive(true);
            JarTable.SetActive(true);
            TweezersShop.SetActive(false);
            GV.AddMoneyUpdate(-tweezers_price);
        }
    }
    public void On_Acid_Buy()
    {
        if (GV.earnedMoney >= acid_price)
        {
            GV.acid_code = Random.Range(1000,9999);
            AcidShop.transform.GetChild(0).GetComponent<Text>().text = "AcidCode: " + GV.acid_code;
            AcidShop.GetComponent<Button>().interactable = false;
            GV.acid_bought = true;
            GV.AddMoneyUpdate(-acid_price);
        }
    }

    public void Acid_Input(string s)
    {
        int i = int.Parse(s);
        if (i == GV.acid_code)
        {
            GV.acid_bought = false;
            GV.acid_code = 0;
            GV.SetRating(10f);
            display.FinishTreatment();
            AcidShop.transform.GetChild(0).GetComponent<Text>().text = "Acid: \n Gives you a Code for opening the Acid Chamber to get rid of an Alien and still get a good review.\n 450$";
            AcidShop.GetComponent<Button>().interactable = true;
            InputField.GetComponent<TMP_InputField>().text = "";
            InputField.SetActive(false);
            
        }
    }

}
