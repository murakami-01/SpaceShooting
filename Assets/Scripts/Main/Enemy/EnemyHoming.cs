using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のホーミング弾を制御するクラス
/// </summary>
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


    void FixedUpdate()
    {
        if (player != null)
        {
            float sp = bulletDataList.bulletDataList[bulletid].Speed;
            if (isTargetting)
            {
                Vector3 dir = player.transform.position - this.gameObject.transform.position;
                this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                rb.velocity = dir.normalized * sp;
                if (Vector3.Distance(this.transform.position, player.transform.position) < 4.2f)
                {
                    //特定距離まで近づいたら追尾を終える
                    isTargetting = false;
                }
            }

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }else if (other.CompareTag("PlayerBullet"))
        {
            //弾丸同士ならダメージ処理
            Damage();
        }
    }
}
