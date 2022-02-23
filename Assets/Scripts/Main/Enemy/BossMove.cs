using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss�̈ړ�����p�N���X
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
            //����̍��W�ɒ��������~�Ahp�ƍU���֌W�̏�����L����
            this.gameObject.transform.position = new Vector3(0, 2.5f * ScreenAdjust.heightRatio, 0);
            rb.velocity = Vector3.zero; 
            bossHpManager.isActive = true;
            bossAttack.StartCoroutine(bossAttack.FirstAttack());
            Destroy(this);
        }
        
    }
}
