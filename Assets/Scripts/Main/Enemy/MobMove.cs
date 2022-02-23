using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mob�̈ړ��𐧌䂷��N���X
/// </summary>
public class MobMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -speed, 0);
    }

    void FixedUpdate()
    {
        //��ʊO�ɏo����j��
        if (this.gameObject.transform.position.y <= -5.5f * ScreenAdjust.heightRatio) Destroy(this.gameObject);
    }
}
