using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの挙動を制御するクラス
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField] private float speed = 0.6f;
    public string itemName;
    private Rigidbody rb;


    public virtual void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -speed, 0);
    }

    void FixedUpdate()
    {
        //画面外に出たら破壊
        if (this.transform.position.y <= -5.5f * ScreenAdjust.heightRatio) Destroy(this.gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //プレイヤーが獲得
            Destroy(this.gameObject);
        }
    }
}
