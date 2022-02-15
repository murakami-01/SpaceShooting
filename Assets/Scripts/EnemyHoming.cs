using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoming : BulletManager
{
    public override int bulletid { get; set; } = 4;
    private Rigidbody rb;
    public GameObject player { get; set; }
    private bool isTargetting = true;


    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }


    public override void FixedUpdate()
    {
        if (player != null)
        {
            float sp = bulletDataList.bulletDataList[bulletid].Speed;
            if (isTargetting)
            {
                if (Vector3.Distance(this.transform.position, player.transform.position) >= 3.5f)
                {
                    Vector3 dir = player.transform.position - this.gameObject.transform.position;
                    this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                    rb.velocity = dir.normalized * sp;
                }
                else
                {
                    isTargetting = false;
                }
            }

            if (this.transform.position.y <= -6) Destroy(this.gameObject);
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
