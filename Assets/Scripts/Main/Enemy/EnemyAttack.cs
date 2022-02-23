using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵（mob）の攻撃に関する抽象クラス
/// </summary>
public abstract class EnemyAttack : MonoBehaviour
{
    public GameObject bullet;
    public float coolTime;

    void Start()
    {
        StartCoroutine(Attack());
    }

    /**
     * <summary>
     * 実際に回す攻撃処理の中身
     * </summary>
     * */
    public abstract IEnumerator Attack();
}
