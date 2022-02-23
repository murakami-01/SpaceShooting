using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “G‚Ì’Êí’e‚ğ§Œä‚·‚éƒNƒ‰ƒX
/// </summary>
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
        if (this.transform.position.y < -5.5f*ScreenAdjust.heightRatio)
        {
            //˜gŠO‚Éo‚½‚ç”j‰ó
            Destroy(this.gameObject);
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }else if(other.CompareTag("PlayerBullet"))
        {
            //’eŠÛ“¯m‚È‚çƒ_ƒ[ƒWˆ—
            Damage();
        }
    }
}
