using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHpManager : HpManager
{
    private GameObject sceneManager;
    private ItemCreator itemCreator;
    [SerializeField] private GameObject effect;

    public override void Awake()
    {
        hp = maxHp;
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (this.transform.position.y <= 5)
        {
            if (other.tag == "PlayerBullet" || other.tag == "PlayerLaser")
            {
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack);
            }
            else if (other.tag == "Player")
            {
                Damage(maxHp);
            }
        }

        if(other.tag == "BossBomb")
        {
            Instantiate(effect, this.transform.position, Quaternion.identity);
            sceneManager.GetComponent<ResultJudgment>().num++;
            Destroy(this.gameObject);
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (this.transform.position.y <= 5)
        {
            if (other.tag == "PlayerLaser")
            {
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack * 0.1f);
            }
        }
        
    }

    public override void Die()
    {
        itemCreator = sceneManager.GetComponent<ItemCreator>();
        itemCreator.RandomCreate(this.transform.position);
        sceneManager.GetComponent<ResultJudgment>().num++;
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
