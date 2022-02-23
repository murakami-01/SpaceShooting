using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mobの移動を制御するクラス
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
        //画面外に出たら破壊
        if (this.gameObject.transform.position.y <= -5.5f * ScreenAdjust.heightRatio) Destroy(this.gameObject);
    }
}
