using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bossの移動制御用クラス
/// </summary>
public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private BossHpManager bossHpManager;
    [SerializeField] private BossAttack bossAttack;
    private Rigidbody rb;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        bossHpManager.isActive = false;
        rb.velocity = new Vector3(0, -speed, 0);
    }

    private void FixedUpdate()
    {
        if (this.transform.position.y <= 2.5f*ScreenAdjust.heightRatio)
        {
            //特定の座標に着いたら停止、hpと攻撃関係の処理を有効化
            this.gameObject.transform.position = new Vector3(0, 2.5f * ScreenAdjust.heightRatio, 0);
            rb.velocity = Vector3.zero; 
            bossHpManager.isActive = true;
            bossAttack.StartCoroutine(bossAttack.FirstAttack());
            Destroy(this);
        }
        
    }
}
