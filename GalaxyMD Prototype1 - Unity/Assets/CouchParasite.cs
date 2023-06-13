using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchParasite : MonoBehaviour
{
    public GlobalVariables GV;
    public GameObject Parasite;
    public GameObject Cord;


    // Start is called before the first frame update

    public void Update()
    {
        //if (this.gameObject.activeSelf == true)
        //{
        //    GameObject GV = GameObject.FindGameObjectWithTag("GlobalVariables");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Parasite"))
        {
            Debug.Log("Parasite entered");
            GV.AddMoney(350f);
            GV.SetRating(3.5f);
            Destroy(other.transform.gameObject);
            //other.transform.gameObject.SetActive(false);
            //other.GetComponent<MeshRenderer>().enabled = false;
            Parasite.SetActive(true);
            Cord.SetActive(false);

        }
    }
}
