using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollTexto : MonoBehaviour
{
 
    public float scrollSpeed = 20;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -25){
            Vector3 pos = transform.position;

            Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);

            pos += localVectorUp * scrollSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
