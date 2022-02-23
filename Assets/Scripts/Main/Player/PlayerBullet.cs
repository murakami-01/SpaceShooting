using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̒ʏ�e�𐧌䂷��N���X
/// </summary>
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
        if (this.transform.position.y > 5.5f * ScreenAdjust.heightRatio)
        {
            //��ʊO�ɏo����j��
            Destroy(this.gameObject);
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }else if (other.CompareTag("EnemyBullet"))
        {
            Damage();
        }
    }
}