using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletManager : MonoBehaviour
{
    /// <summary>
    /// ’eŠÛ‚ÉŠÖ‚·‚é’ŠÛƒNƒ‰ƒX
    /// </summary>

    public BulletDataList bulletDataList;
    public abstract int bulletid { get; set; }

    public virtual int Attack
    {
        get { return bulletDataList.bulletDataList[bulletid].Attack; }
    }

    public abstract void Start();
    public abstract void FixedUpdate();


    public abstract void OnTriggerEnter(Collider other);  
    
}
