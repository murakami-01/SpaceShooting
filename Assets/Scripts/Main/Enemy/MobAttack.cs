using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ʏ�mob�̍U���𐧌䂷��N���X
/// </summary>
public class MobAttack : EnemyAttack
{
    public override IEnumerator Attack()
    {
        while (true)
        {
            if (this.gameObject.transform.position.y < 5.5f*ScreenAdjust.heightRatio)
            {
                //��ʓ��Ȃ�U��
                Instantiate(bullet, this.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(coolTime);
        }

    }
}
