using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public override void FixedUpdate()
    {
        if (this.transform.position.y > 5 || this.transform.position.x > 3 || this.transform.position.x < -3) 
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
