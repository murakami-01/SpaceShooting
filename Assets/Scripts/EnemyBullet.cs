using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletManager
{
    Rigidbody rb;
    public override int bulletid { get; set; } = 4;

    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -1, 0) * bulletDataList.bulletDataList[bulletid].Speed;
    }
    public override void FixedUpdate()
    {
        if (this.transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
