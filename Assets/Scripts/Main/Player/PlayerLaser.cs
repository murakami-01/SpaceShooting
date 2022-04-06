using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃��[�U�[�𐧌䂷��N���X
/// </summary>
public class PlayerLaser : BulletManager
{
    public override int bulletid { get; set; } = 3;
    public Transform playerTrans { get; set; }
    private int layerMask;

    public override void Start()
    {
        layerMask = 1 << 7;
    }
    void FixedUpdate()
    {
        if(Physics.Raycast(this.transform.position,new Vector3(0,1,0), out RaycastHit hit,10f,layerMask))
        {
            //���[�U�[�Ǝː�ɓG������ꍇ�̏���
            if (hit.transform.position.y <= 4 * ScreenAdjust.heightRatio)
            {
                if(hit.transform.gameObject.TryGetComponent<HpManager>(out HpManager hpManager))
                {
                    //��ʓ��Ȃ�_���[�W
                    hpManager.Damage(this.Attack);
                }
                
            }
            

            float distance = hit.transform.position.y - this.transform.position.y;
            float scaleRate = bulletDataList.bulletDataList[bulletid].Speed + this.transform.localScale.y;
            if (distance <= scaleRate / playerTrans.localScale.x)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x, distance / playerTrans.localScale.x, this.transform.localScale.z);
            }
            else
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x, scaleRate, this.transform.localScale.z);
            }
        }
        else if (this.transform.localScale.y > 60)
        {
            //��ʊO�ɏo�鎞�̏���
            this.transform.localScale = new Vector3(this.transform.localScale.x, 60, this.transform.localScale.z);
        }
        else
        {
            Vector3 scaleRate = this.transform.localScale + new Vector3(0, bulletDataList.bulletDataList[bulletid].Speed * Time.fixedDeltaTime / playerTrans.localScale.x, 0);
            this.transform.localScale = scaleRate;
        }

    }

    public override void OnTriggerEnter(Collider other)
    {

    }


}
