using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// チュートリアルでプレイヤーのhpを制御するクラス
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
        //死亡時に敵を破壊
        if (MobHpManager != null)
        {
            MobHpManager.Die();
        }
    }
}
