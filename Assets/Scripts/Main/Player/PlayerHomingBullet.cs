using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのホーミング弾を制御するクラス
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

    void FixedUpdate()
    {
        /*対象破壊時に再検索する機能はゲームバランスの観点から停止
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
            //対象に向かって回転し、移動
            Vector3 vector = target.transform.position - this.transform.position;
            this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, vector);
            rb.velocity = vector.normalized * limitSpeed;

        }
        else
        {
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
