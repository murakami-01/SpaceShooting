using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEffect : MonoBehaviour
{
    private float time = 0;
    private void Start()
    {

        Debug.Log(this.transform.position);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5)
        {
            Destroy(this.gameObject);
        }
    }
}
