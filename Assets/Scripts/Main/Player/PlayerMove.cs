using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動きを制御する
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [System.NonSerialized]public Vector3 playerPos;
    [System.NonSerialized]public Vector3 mousePos;
    [System.NonSerialized]public Rigidbody rb;
    public bool isActive { get; set; } = true;

    public void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }


    public virtual void Update()
    {
        if (isActive)
        {
            PlayerControl(-2.35f * ScreenAdjust.widthRatio, 2.35f * ScreenAdjust.widthRatio, -4.5f * ScreenAdjust.heightRatio, 4.5f * ScreenAdjust.heightRatio);
        }
    }

    public void PlayerControl(float wightMin, float wightMax,float heightMin,float heightMax)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //タップされたときの処理
            playerPos = this.gameObject.transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePos;

            //タッチ対応デバイス向け、1本目の指にのみ反応
            if (Input.touchSupported)
            {
                diff = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - mousePos;
            }

            Vector2 nextPos = new Vector2((playerPos + diff).x, (playerPos + diff).y);
            nextPos.x = Mathf.Clamp(nextPos.x, wightMin, wightMax);
            nextPos.y = Mathf.Clamp(nextPos.y, heightMin, heightMax);

            rb.MovePosition(nextPos);

        }

        if (Input.GetMouseButtonUp(0))
        {
            //押されなくなったら初期化
            playerPos = Vector3.zero;
            mousePos = Vector3.zero;
        }
    }


}
