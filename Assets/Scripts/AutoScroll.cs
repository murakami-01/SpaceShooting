using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] private float speed = 1;
 
    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= -10.24f)
        {
            transform.position = new Vector3(0, 20.48f, 0);
        }

        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }
}
