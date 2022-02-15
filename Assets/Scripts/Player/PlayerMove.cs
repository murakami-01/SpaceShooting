using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�̓����𐧌䂷��
    /// </summary>

    private Vector3 playerPos;
    private Vector3 mousePos;
    private Rigidbody rb;
    public bool isActive { get; set; } = true;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (isActive)
        {
            PlayerControl();
        }
    }

    void PlayerControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerPos = this.gameObject.transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePos;

            //�^�b�`�Ή��f�o�C�X�����A1�{�ڂ̎w�ɂ̂ݔ���
            if (Input.touchSupported)
            {
                diff = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - mousePos;
            }

            Vector2 nextPos = new Vector2((playerPos + diff).x, (playerPos + diff).y);
            if (nextPos.x > 2.2f) nextPos.x = 2.2f;
            else if (nextPos.x < -2.2f) nextPos.x = -2.2f;
            if (nextPos.y > 4) nextPos.y = 4;
            else if (nextPos.y < -4) nextPos.y = -4;

            rb.MovePosition(nextPos);

        }

        if (Input.GetMouseButtonUp(0))
        {
            playerPos = Vector3.zero;
            mousePos = Vector3.zero;
        }
    }


}
