using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRotate : MonoBehaviour
{ 
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        float XAxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        
        transform.Rotate(Vector3.up, -XAxisRotation);
        
    }
}
