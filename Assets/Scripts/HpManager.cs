using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// hp管理の抽象クラス
/// </summary>
public abstract class HpManager : MonoBehaviour
{
    public float maxHp = 50;
    public AudioClip clip;
    public float hp { get; set; }

    public Transform hpParentTransform;
    [System.NonSerialized] public bool isRed = false;

    public virtual void Awake()
    { 
        hp = maxHp;
    }


    public abstract void OnTriggerEnter(Collider other);

    /**
     * <summary>
     * 受けたダメージに対する処理
     * </summary>
     * <param name="damage"> ダメージの値 </param>
     * */
    public abstract void Damage(float damage);

    /**
     * <summary>
     * 死亡処理
     * </summary>
     * */
    public abstract void Die();
}
