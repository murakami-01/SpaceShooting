using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HpManager : MonoBehaviour
{
    public float maxHp = 50;
    public float hp { get; set; }

    public Transform hpParentTransform;
    [System.NonSerialized] public bool isRed = false;

    public virtual void Awake()
    { 
        hp = maxHp;
    }


    public abstract void OnTriggerEnter(Collider other);

    public virtual void Damage(float damage)
    {
        if (hp <= damage)
        {
            hpParentTransform.localScale = new Vector3(0, hpParentTransform.localScale.y, hpParentTransform.localScale.z);
            Die();
        }
        else
        {
            hp -= damage;
            hpParentTransform.localScale = new Vector3(hp / maxHp, hpParentTransform.localScale.y, hpParentTransform.localScale.z);
            if (hp / maxHp <= 0.2f && !isRed)
            {
                var hpgauge = hpParentTransform.GetChild(0);
                hpgauge.GetComponent<SpriteRenderer>().color = Color.red;
                isRed = true;
            }
            else if (isRed && hp / maxHp > 0.2f)
            {
                var hpgauge = hpParentTransform.GetChild(0);
                hpgauge.GetComponent<SpriteRenderer>().color = Color.green;
            }

        }
    }

    public abstract void Die();
}
