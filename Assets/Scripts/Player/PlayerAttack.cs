using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject normalBullet;
    [SerializeField] private GameObject nwayBullet;
    [SerializeField] private GameObject homingBullet;
    [SerializeField] private GameObject Laser;
    [SerializeField] private int itemTime = 4;
    [SerializeField] private int nwayCount = 4;
    [SerializeField] private float nwayDegree = 90;

    private float coolTime = 0.15f;
    private Dictionary<string, float> itemList = new Dictionary<string, float>
    {
        ["nway"] = 0,
        ["laser"] = 0,
        ["homing"] = 0
    };
    private List<GameObject> laserList = new List<GameObject>(2);
    private bool isInstantiateLaser = false;
    public bool isActive { get; set; } = true;
    Subject<int> Attack = new Subject<int>();

    private void Start()
    {
        Attack.Subscribe(n=>DefaultAttack());
        Attack.Where(n => itemList["nway"] > 0).Subscribe(n => NwayAttack());
        Attack.Where(n => itemList["homing"] > 0 && n % 2 == 0).Subscribe(n => HomingAttack());
        Attack.Where(n => itemList["laser"] > 0).Subscribe(n => LaserAttack());
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine("AttackCounter");
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopCoroutine("AttackCounter");
                if (isInstantiateLaser)
                {
                    laserList.ForEach(ins => Destroy(ins));
                    laserList.RemoveAll(i => i != null);
                    isInstantiateLaser = false;
                }
            }
        }
        else
        {
            StopAllCoroutines();
        }
        
    }

    IEnumerator AttackCounter()
    {
        int attackTime = 0;
        while (true)
        {
            Attack.OnNext(attackTime);
            yield return new WaitForSeconds(coolTime);
            attackTime++;
        }
    }
    
    //í èÌçUåÇ
    void DefaultAttack()
    {
        Instantiate(normalBullet,this.transform.position,Quaternion.identity);
    }

    //N-wayçUåÇ
    void NwayAttack()
    {
        if (nwayCount <= 1) nwayCount = 2;
        float nwayRange = Mathf.PI * (nwayDegree / 180);

        for(int i = 0; i < nwayCount; i++)
        {
            float theta = (nwayRange / (nwayCount - 1)) * i + 0.5f * (Mathf.PI - nwayRange);
            GameObject nwayBulletObject = (GameObject)Instantiate(nwayBullet, this.transform.position, Quaternion.Euler(0, 0, theta / Mathf.PI * 180 - 90));
            PlayerNwayBullet nwayCs = nwayBulletObject.GetComponent < PlayerNwayBullet > ();
            nwayCs.theta = theta;
        }
        itemList["nway"] -= coolTime;
    }

    void HomingAttack()
    {
        float distance = float.MaxValue;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject target = null;

        for(int i = 0; i < enemyList.Length; i++)
        {
            float tempDistance = Vector3.Distance(this.transform.position, enemyList[i].transform.position);
            if (distance > tempDistance)
            {
                target = enemyList[i];
                distance = tempDistance;
            }
        }

        if (target != null)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 position = this.transform.position + new Vector3(0.5f * (1 - 2 * i), 0.5f, 0);
                GameObject homingBulletObject = (GameObject)Instantiate(homingBullet, position, Quaternion.identity);
                var homingCs = homingBulletObject.GetComponent<PlayerHomingBullet>();
                homingCs.target = target;
            }
            itemList["homing"] -= coolTime * 2;
        }
        
    }

    void LaserAttack()
    {
        if (!isInstantiateLaser)
        {
            Vector3 distance = new Vector3(0.4f, 0.5f, 0);

            for (int i = 0; i < 2; i++)
            {
                Vector3 position = this.gameObject.transform.position + new Vector3(0.25f * (1 - 2 * i), 0.5f, 0);
                GameObject instance = (GameObject)Instantiate(Laser, position, Quaternion.identity);
                instance.gameObject.transform.parent = this.gameObject.transform;
                laserList.Add(instance);
            }
            isInstantiateLaser = true;
        }

        itemList["laser"] -= coolTime;
        if (itemList["laser"] <= 0)
        {
            laserList.ForEach(ins =>Destroy(ins)) ;
            laserList.RemoveAll(i=>i!=null);
            isInstantiateLaser = false;
        }

    }

    public void GetItem(string itemName)
    {
        itemList[itemName] = itemTime;
    }

    public void StopAttack()
    {
        StopAllCoroutines();
    }
}
