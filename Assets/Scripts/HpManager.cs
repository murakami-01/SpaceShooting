using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// hp�Ǘ��̒��ۃN���X
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
     * �󂯂��_���[�W�ɑ΂��鏈��
     * </summary>
     * <param name="damage"> �_���[�W�̒l </param>
     * */
    public abstract void Damage(float damage);

    /**
     * <summary>
     * ���S����
     * </summary>
     * */
    public abstract void Die();
}
