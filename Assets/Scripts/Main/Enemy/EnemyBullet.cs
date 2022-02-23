using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̒ʏ�e�𐧌䂷��N���X
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
            //�g�O�ɏo����j��
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
            //�e�ۓ��m�Ȃ�_���[�W����
            Damage();
        }
    }
}
