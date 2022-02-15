using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public GameObject bullet;
    public float coolTime;

    void Start()
    {
        StartCoroutine("Attack");
    }

    public abstract IEnumerator Attack();
}
