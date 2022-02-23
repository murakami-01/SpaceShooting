using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�i�̎����X�N���[���𐧌䂷��N���X
/// </summary>
public class AutoScroll : MonoBehaviour
{
    [SerializeField] private float speed = 1;
 
    void Update()
    {

        if (transform.localPosition.y <= -600f)
        {
            transform.localPosition = new Vector3(0, 1200f, 0);
        }

        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }
}
