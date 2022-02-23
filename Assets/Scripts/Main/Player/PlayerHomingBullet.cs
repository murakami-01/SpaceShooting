using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃z�[�~���O�e�𐧌䂷��N���X
/// </summary>
public class PlayerHomingBullet : BulletManager
{
    Rigidbody rb;
    public override int bulletid { get; set; } = 2;
    public GameObject target { private get; set; }
    private float limitSpeed;


    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        limitSpeed = bulletDataList.bulletDataList[bulletid].Speed;
    }

    public override void FixedUpdate()
    {
        /*�Ώ۔j�󎞂ɍČ�������@�\�̓Q�[���o�����X�̊ϓ_�����~
        if (target == null)
        {
            float distance = float.MaxValue;
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemyList.Length; i++)
            {
                float tempDistance = Vector3.Distance(this.transform.position, enemyList[i].transform.position);
                if (distance > tempDistance)
                {
                    target = enemyList[i];
                    distance = tempDistance;
                }
            }

            
        }
        */

        if(target!=null)
        {
            //�ΏۂɌ������ĉ�]���A�ړ�
            Vector3 vector = target.transform.position - this.transform.position;
            this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, vector);
            rb.velocity = vector.normalized * limitSpeed;

        }
        else
        {
            Destroy(this.gameObject);
        }

        //��ʊO�ɏo����j��
        bool isOutOfRange = this.transform.position.y >= 5.5f * ScreenAdjust.heightRatio || this.transform.position.y <= -5.5f * ScreenAdjust.heightRatio
            || this.transform.position.x > 2.9f * ScreenAdjust.widthRatio || this.transform.position.x < -2.9f * ScreenAdjust.widthRatio;
        if (isOutOfRange) Destroy(this.gameObject);
        
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
