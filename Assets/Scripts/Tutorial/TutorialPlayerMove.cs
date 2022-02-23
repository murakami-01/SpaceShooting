using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �`���[�g���A���Ńv���C���[�̓����𐧌䂷��N���X
/// </summary>
public class TutorialPlayerMove : PlayerMove
{

    public override void Update()
    {
        if (isActive)
        {
            PlayerControl(-2.35f *ScreenAdjust.widthRatio, 2.35f *ScreenAdjust.widthRatio, -1.5f *ScreenAdjust.heightRatio, 4.1f *ScreenAdjust.heightRatio);
        }
    }

    /**
     * <summary>
     * �Ō�̈ړ�����
     * </summary>
     * */
    public void Finish()
    {
        rb.velocity = new Vector3(0, 5, 0);
    }

    /**
     * <summary>
     * �������~�߂鏈��
     * </summary>
     * */
    public void StopMove()
    {
        isActive = false; 
        playerPos = Vector3.zero;
        mousePos = Vector3.zero;
    }

    /**
     * <summary>
     * ������悤�ɂ��鏈��
     * </summary>
     * */
    public void StartMove()
    {
        isActive = true;
        playerPos = this.gameObject.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
