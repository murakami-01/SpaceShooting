using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルでプレイヤーの動きを制御するクラス
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
     * 最後の移動処理
     * </summary>
     * */
    public void Finish()
    {
        rb.velocity = new Vector3(0, 5, 0);
    }

    /**
     * <summary>
     * 動きを止める処理
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
     * 動けるようにする処理
     * </summary>
     * */
    public void StartMove()
    {
        isActive = true;
        playerPos = this.gameObject.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
