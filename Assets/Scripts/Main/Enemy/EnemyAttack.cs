using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�imob�j�̍U���Ɋւ��钊�ۃN���X
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
     * ���ۂɉ񂷍U�������̒��g
     * </summary>
     * */
    public abstract IEnumerator Attack();
}
