using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(target!=null)
        {
            /*
            Vector3 position = Vector3.MoveTowards(this.gameObject.transform.position, target.transform.position, bulletDataList.bulletDataList[bulletid].Speed * Time.fixedDeltaTime);
            if (position.y > 7)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Vector3 dir = target.transform.position - this.gameObject.transform.position;
                this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                rb.MovePosition(position);
                Debug.Log($"åªç›ín{this.transform.position}, à⁄ìÆêÊ{position}, ìGà íu{target.transform.position}");
            }
            */



            Vector3 vector = target.transform.position - this.transform.position;
            this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, vector);
            rb.velocity = vector.normalized*limitSpeed;
            
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
