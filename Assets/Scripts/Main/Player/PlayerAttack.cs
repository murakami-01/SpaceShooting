using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーの攻撃を制御するクラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject nwayBullet;
    public GameObject homingBullet;
    public GameObject Laser;
    public int nwayCount = 4;
    public float nwayDegree = 90;
    public PlayerItemManager itemManager;
    public float coolTime = 0.13f;

    protected List<GameObject> laserList = new List<GameObject>(2);
    protected int laserNum = 0;
    protected int drawnLaser = 0;
    public bool isActive { get; set; } = true;//外部から攻撃を制御するための変数
    [System.NonSerialized]public bool isActivated = false;//攻撃中かどうか
    public Subject<int> Attack = new Subject<int>();

    /**
     * <summary>
     * それぞれの攻撃処理の購読
     * </summary>
     * */
    public void Start()
    {
        Attack.Subscribe(n=>DefaultAttack());
    }

    public virtual void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0) && !isActivated)
            {
                //画面が押されたら攻撃開始
                StartCoroutine(AttackCounter());
                isActivated = true;
                for(int i =1; i <= laserNum; i++)
                {
                    ActivateLaser(i);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                //画面が押されなくなったら攻撃中止
                StopAllCoroutines();
                isActivated = false;
                laserNum = drawnLaser;
                DeactivateLaser(drawnLaser);
            }
        }
        else
        {
            StopAllCoroutines();
            isActivated = false;
            laserNum = drawnLaser;
            DeactivateLaser(drawnLaser);
        }
        
    }

    /**
     * <summary>
     * 攻撃を呼び続ける処理
     * </summary>
     * */
    public IEnumerator AttackCounter()
    {
        int attackTime = 0;
        while (true)
        {
            Attack.OnNext(attackTime);
            yield return new WaitForSeconds(coolTime);
            attackTime++;
        }
    }
    
    /**
     * <summary>
     * 通常攻撃
     * </summary>
     * */
    public void DefaultAttack()
    {
        Instantiate(normalBullet,this.transform.position,Quaternion.identity);
    }

    /**
     * <summary>
     * nway攻撃
     * </summary>
     * */
    public void NwayAttack(int num)
    {
        //それぞれの回転角を求めてインスタンス生成
        float nwayRange = Mathf.PI * (nwayDegree / 180);
        for(int i = 0; i < num*2; i++)
        {
            float theta = (nwayRange / (num*2-1)) * i + 0.5f * (Mathf.PI - nwayRange);
            GameObject nwayBulletObject = (GameObject)Instantiate(nwayBullet, this.transform.position, Quaternion.Euler(0, 0, theta / Mathf.PI * 180 - 90));
            PlayerNwayBullet nwayCs = nwayBulletObject.GetComponent < PlayerNwayBullet > ();
            nwayCs.theta = theta;
        }
    }


    /**
     * <summary>
     * ホーミング攻撃
     * </summary>
     * */
    public void HomingAttack(int num)
    {
        //敵の検索
        float distance = float.MaxValue;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject target = null;

        for(int i = 0; i < enemyList.Length; i++)
        {
            //検索したものの中から一番近いものを探す
            float tempDistance = Vector3.Distance(this.transform.position, enemyList[i].transform.position);
            if (distance > tempDistance)
            {
                target = enemyList[i];
                distance = tempDistance;
            }
        }

        if (target != null)
        {
            for (int i = 0; i < num; i++)
            {
                //インスタンス生成し、インスタンスに敵の情報を渡す
                Vector3 position = this.transform.position + new Vector3(0.5f * (1 - 2 * i), 0.5f, 0);
                GameObject homingBulletObject = (GameObject)Instantiate(homingBullet, position, Quaternion.identity);
                var homingCs = homingBulletObject.GetComponent<PlayerHomingBullet>();
                homingCs.target = target;
            }
        }
        
    }


    /**
     * <summary>
     * レーザーを指定された本数消去
     * </summary>
     * <param name="num">何本消去するか </param>
     * */
    public void DeactivateLaser(int num)
    {
        num = Mathf.Min(num, drawnLaser);
        for(int i = 0; i < num; i++)
        {
            Destroy(laserList[drawnLaser - 1]);
            laserList.RemoveAt(drawnLaser - 1);
            drawnLaser--;
        }
        
    }

    /**
     * <summary>
     * レーザーの生成
     * </summary>
     * <param name="num"> レーザーが何本目か </param>
     * */
    public void ActivateLaser(int num)
    {

        if (Input.GetMouseButton(0))
        {
            drawnLaser++;
            Vector3 position = this.gameObject.transform.position + new Vector3(0.25f * (1 - 2 * (num - 1)), 0.5f, 0);
            GameObject instance = (GameObject)Instantiate(Laser, position, Quaternion.identity);
            instance.transform.parent = this.gameObject.transform;
            PlayerLaser playerLaser = instance.GetComponent<PlayerLaser>();
            playerLaser.playerTrans = this.gameObject.transform;
            laserList.Add(instance);
        }
        else
        {
            laserNum++;
        }
        
    }

    /**
     * <summary>
     * 攻撃を停止
     * </summary>
     * */
    public void StopAttack()
    {
        StopAllCoroutines();
    }
}
