using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GimobjÌUÉÖ·éÛNX
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
     * ÀÛÉñ·UÌg
     * </summary>
     * */
    public abstract IEnumerator Attack();
}
