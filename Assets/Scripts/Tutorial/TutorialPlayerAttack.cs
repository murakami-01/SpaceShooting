using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �`���[�g���A���Ńv���C���[�̍U���𐧌䂷��N���X
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
     * �`���[�g���A���ŏI�i�K�̍U������
     * </summary>
     * */
    public void FinishAttack()
    {
        isFinished = true;
        coolTime *= 1.1f;
        StartCoroutine(AttackCounter());
    }
}
