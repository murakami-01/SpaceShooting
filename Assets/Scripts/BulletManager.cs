using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾丸に関する抽象クラス
/// </summary>
public abstract class BulletManager : MonoBehaviour
{
    public int bulletHp;
    public BulletDataList bulletDataList;
    public abstract int bulletid { get; set; }

    public virtual int Attack
    {
        get { return bulletDataList.bulletDataList[bulletid].Attack; }
    }

    /**
     * <summary>
     * 初期加速用
     * </summary>
     * */
    public abstract void Start();

    /**
     * <summary>
     * 移動計算のたびに行う判定用
     * </summary>
     * */
    public abstract void FixedUpdate();


    public abstract void OnTriggerEnter(Collider other);  
    
    /**
     * <summary>
     * 弾丸自体への当たり判定処理
     * </summary>
     * */
    public void Damage()
    {
        bulletHp--;
        if (bulletHp <= 0) Destroy(this.gameObject);
    }
}
