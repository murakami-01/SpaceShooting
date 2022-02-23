using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �`���[�g���A���Ńv���C���[��hp�𐧌䂷��N���X
/// </summary>
public class TutorialPlayerHpManager : PlayerHpManager
{
    [SerializeField] private TutorialMobHpManager MobHpManager;

    public override void Start()
    {
        var nwayInstance = (GameObject)Instantiate(nwayIcon);
        SetIcon(nwayInstance);
        iconStack.Push(nwayInstance);
    }

    public override void Die()
    {
        //���S���ɓG��j��
        if (MobHpManager != null)
        {
            MobHpManager.Die();
        }
    }
}
