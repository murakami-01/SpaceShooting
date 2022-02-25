using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�ۂɊւ��钊�ۃN���X
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
     * ���������p
     * </summary>
     * */
    public abstract void Start();


    public abstract void OnTriggerEnter(Collider other);

    /**
     * <summary>
     * ��ʊO�ɏo����j��
     * </summary>
     * */
    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    /**
     * <summary>
     * �e�ێ��̂ւ̓����蔻�菈��
     * </summary>
     * */
    public void Damage()
    {
        bulletHp--;
        if (bulletHp <= 0) Destroy(this.gameObject);
    }
}
