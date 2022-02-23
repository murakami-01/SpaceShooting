using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// チュートリアルでプレイヤーの攻撃を制御するクラス
/// </summary>
public class TutorialPlayerAttack : PlayerAttack
{
    private bool isFinished = false;

    public override void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButton(0) && !isActivated)
            {
                StartCoroutine(AttackCounter());
                isActivated = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopAllCoroutines();
                isActivated = false;
                if (laserNum > 0)
                {
                    laserList.ForEach(ins => Destroy(ins));
                    laserList.RemoveAll(i => i != null);
                    laserNum = 0;
                }
            }
        }
        else if(!isFinished)
        {
            StopAllCoroutines();
            isActivated = false;
            if (laserNum > 0)
            {
                laserList.ForEach(ins => Destroy(ins));
                laserList.RemoveAll(i => i != null);
                laserNum = 0;
            }
        }

    }

    /**
     * <summary>
     * チュートリアル最終段階の攻撃処理
     * </summary>
     * */
    public void FinishAttack()
    {
        isFinished = true;
        coolTime *= 1.1f;
        StartCoroutine(AttackCounter());
    }
}
