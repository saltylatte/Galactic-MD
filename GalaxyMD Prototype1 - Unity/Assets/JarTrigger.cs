using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class JarTrigger : MonoBehaviour
{
    public UnityEvent parasiteEvent;
    public GlobalVariables GV;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Parasite"))
        {
            GV.AddMoney(50f);
            GV.SetRating(1.5f);
            other.transform.SetParent(this.transform);
            //this.transform.position = new Vector3(-2.3f, 10f, -7);
        }
    }
    
}
