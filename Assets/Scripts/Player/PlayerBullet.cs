using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletManager
{
    Rigidbody rb;
    public override int bulletid { get; set; } = 0;
    private float limitSpeed;

    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        limitSpeed = bulletDataList.bulletDataList[bulletid].Speed;
        rb.velocity = new Vector3(0, 1, 0) * limitSpeed;
    }
    public override void  FixedUpdate()
    {
        if (this.transform.position.y > 5)
        {
            Destroy(this.gameObject);
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
