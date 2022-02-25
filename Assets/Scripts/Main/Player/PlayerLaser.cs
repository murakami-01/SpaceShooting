using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃��[�U�[�𐧌䂷��N���X
/// </summary>
public class PlayerLaser : BulletManager
{
    public override int bulletid { get; set; } = 3;
    private GameObject parent;
    private GameObject player;
    private int layerMask;

    public override void Start()
    {
        parent = transform.parent.gameObject;
        player = transform.root.gameObject;
        layerMask = 1 << 7;
    }
    void FixedUpdate()
    {
        if(Physics.Raycast(parent.transform.position,new Vector3(0,1,0), out RaycastHit hit,10f,layerMask))
        {
            //���[�U�[�Ǝː�ɓG������ꍇ�̏���
            float distance = hit.transform.position.y - parent.transform.position.y;
            float scaleRate = bulletDataList.bulletDataList[bulletid].Speed + parent.transform.localScale.y;
            if (distance <= scaleRate / player.transform.localScale.x)
            {
                parent.transform.localScale = new Vector3(parent.transform.localScale.x, distance / player.transform.localScale.x, parent.transform.localScale.z);
            }
            else
            {
                parent.transform.localScale = new Vector3(parent.transform.localScale.x, scaleRate, parent.transform.localScale.z);
            }
        }
        else if (parent.transform.localScale.y > 60)
        {
            //��ʊO�ɏo�鎞�̏���
            parent.transform.localScale = new Vector3(parent.transform.localScale.x, 60, parent.transform.localScale.z);
        }
        else
        {
            Vector3 scaleRate = parent.transform.localScale + new Vector3(0, bulletDataList.bulletDataList[bulletid].Speed * Time.fixedDeltaTime / player.transform.localScale.x, 0);
            parent.transform.localScale = scaleRate;
        }

    }

    public override void OnTriggerEnter(Collider other)
    {

    }


}
