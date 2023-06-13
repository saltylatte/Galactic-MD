using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Display : MonoBehaviour
{

    public Image ProgBar;
    public Image RatingBar;
    public Text ProgLabel;
    public Transform Aliens;

    public GameObject Alien1;
    public GameObject Alien2;
    public GameObject Alien3;
    public GameObject Alien4;
    public GameObject Alien5;
    public GameObject Alien6;
    public GameObject Alien7;

    public GameObject WaitingRoom;
    public GameObject RatingWindow;
    public Text RatingLabel;
    public Text AverageRatingLabel;
    public Text DaysLabel;
    public GameObject FinishButton;
    
    public GameObject PflasterLabel3D;
    public GameObject PflasterLabel;

    public Vector3 AlienPosition;
    
    GlobalVariables GV;
    
    public void Start()
    {
        (Instantiate(Alien1, AlienPosition, Quaternion.identity) as GameObject).transform.parent = Aliens.transform;
        WaitingRoom.SetActive(false);
        RatingWindow.SetActive(false);
        GV = GameObject.FindWithTag("GlobalVariables").GetComponent<GlobalVariables>();
    }

    public void UpdateProcessbar(float newAmount, float neededMoney)
    {
        ProgBar.fillAmount = newAmount / neededMoney;
        ProgLabel.text = newAmount +"/"+neededMoney;
    }

    public void UpdatePflasterLabel(int new_pflaster)
    {
        PflasterLabel3D.GetComponent<TextMeshPro>().text = new_pflaster+"";
        PflasterLabel.GetComponent<Text>().text = new_pflaster+"";
    }
    
    
    public void DestroyAliens()
    {
        int childCount = Aliens.childCount;
        
        for (int i = childCount - 1; i >= 0; i--)
        {
            if (Aliens.GetChild(i).name == "GregLivingroomParasite")
            {
                if (Aliens.GetChild(i).gameObject.activeSelf == true)
                {
                    Aliens.GetChild(i).gameObject.SetActive(false);
                }        
                    
            }
            else
            {
                Transform child = Aliens.GetChild(i);
                Destroy(child.gameObject);    
            }
            
        }
    }

    public void AlienButton1()
    {
        DestroyAliens();
        (Instantiate(Alien1, AlienPosition, Quaternion.identity) as GameObject).transform.parent = Aliens.transform;

        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
    }
    
    public void AlienButton2()
    {
        DestroyAliens();
        (Instantiate(Alien2, AlienPosition, Quaternion.identity)as GameObject).transform.parent = Aliens.transform;


        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
        Debug.Log(RatingLabel.text);
    }
    
    public void AlienButton3()
    {
        DestroyAliens();
        (Instantiate(Alien3, AlienPosition, Quaternion.identity)as GameObject).transform.parent = Aliens.transform;
        
        
        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
    }
    public void AlienButton4()
    {
        DestroyAliens();
        (Instantiate(Alien4, AlienPosition, Quaternion.identity)as GameObject).transform.parent = Aliens.transform;
        
        
        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
    }
    public void AlienButton5()
    {
        DestroyAliens();
        (Instantiate(Alien5, AlienPosition, Quaternion.identity)as GameObject).transform.parent = Aliens.transform;
        
        
        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
    }    
    public void AlienButton6()
    {
        DestroyAliens();
        (Instantiate(Alien6, AlienPosition, Quaternion.identity)as GameObject).transform.parent = Aliens.transform;
        
        
        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
    }
    public void AlienButton7()
    {
        DestroyAliens();
        Alien7.SetActive(true);
        
        
        WaitingRoom.SetActive(false);
        FinishButton.SetActive(true);
    }
    public void FinishTreatment()
    {
        RatingWindow.SetActive(true);
        FinishButton.SetActive(false);
        GV.stolen_organs = 0;
        GV.CalculateRating();
    }

    public void OpenWaitingRoom()
    {
        RatingWindow.SetActive(false);
        WaitingRoom.SetActive(true);
    }

    public void UpdateRatingLabel(float r)
    {
        RatingLabel.text = r + "/5";


    }

    public void UpdateAverageRatingLabel(float AverageRating)
    {
        
        AverageRatingLabel.text = (Mathf.Round(AverageRating*10f)/10f) + "/5";
        RatingBar.fillAmount = AverageRating /5;
    }

    public void UpdateDaysLabel(int day)
    {
        DaysLabel.text = "DAY " + day;
    }


    
    
}
