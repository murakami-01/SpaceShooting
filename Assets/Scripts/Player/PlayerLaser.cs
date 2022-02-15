using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : BulletManager
{
    private Rigidbody rb;
    public override int bulletid { get; set; } = 3;
    private GameObject parent;
    private GameObject player;
    private int layerMask;

    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        parent = transform.parent.gameObject;
        player = transform.root.gameObject;
        layerMask = 1 << 7;
    }
    public override void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position,new Vector3(0,1,0), out hit,6f,layerMask))
        {

            float distance = hit.transform.position.y - parent.transform.position.y;
            float scaleRate = bulletDataList.bulletDataList[bulletid].Speed + parent.transform.localScale.y;
            if (distance <= scaleRate / player.transform.localScale.x)
            {
                parent.transform.localScale = new Vector3(parent.transform.localScale.x, distance / player.transform.localScale.x, parent.transform.localScale.z);
            }
            else
            {
                parent.transform.localScale = new Vector3(parent.transform.localScale.x, scaleRate, parent.transform.localScale.z);
            }
        }
        else if (parent.transform.localScale.y > 28)
        {
            parent.transform.localScale = new Vector3(parent.transform.localScale.x, 28, parent.transform.localScale.z);
        }
        else
        {
            Vector3 scaleRate = parent.transform.localScale + new Vector3(0, bulletDataList.bulletDataList[bulletid].Speed * Time.deltaTime, 0);
            parent.transform.localScale = scaleRate;
        }

        /*
        if (isTouchEnemy && enemy != null) 
        {
            float distance = enemy.transform.position.y - parent.transform.position.y;
            float scaleRate = bulletDataList.bulletDataList[bulletid].Speed * Time.deltaTime + parent.transform.localScale.y;
            if (distance <= scaleRate / player.transform.localScale.x) 
            {
                parent.transform.localScale = new Vector3(parent.transform.localScale.x, distance / player.transform.localScale.x, parent.transform.localScale.z);
            }
            else
            {
                parent.transform.localScale = new Vector3(parent.transform.localScale.x, scaleRate, parent.transform.localScale.z);
            }
        
        }
        else if (parent.transform.localScale.y > 35)
        {
            parent.transform.localScale = new Vector3(parent.transform.localScale.x, 35, parent.transform.localScale.z);
        }
        else
        {
            Vector3 scaleRate = parent.transform.localScale + new Vector3(0, bulletDataList.bulletDataList[bulletid].Speed * Time.deltaTime, 0);
            parent.transform.localScale = scaleRate;
        }

        isTouchEnemy = false;
        */
    }

    public override void OnTriggerEnter(Collider other)
    {

    }


}
