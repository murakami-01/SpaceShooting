using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのnway弾を制御するクラス。
/// </summary>
public class PlayerNwayBullet : BulletManager
{
    private Rigidbody rb;
    public float theta { private get; set; }
    public override int bulletid { get; set; } = 1;
    private float limitSpped;

    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        limitSpped = bulletDataList.bulletDataList[bulletid].Speed;
        rb.velocity = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * limitSpped;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            Damage();
        }
    }
}
