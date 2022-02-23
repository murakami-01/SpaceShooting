using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �`���[�g���A����mob�̍U���𐧌䂷��N���X
/// </summary>
public class TutorialMobAttack : EnemyAttack
{
    private bool isActive = false;
    public override IEnumerator Attack()
    {
        while (true)
        {
            if (isActive)
            {
                if (this.gameObject.transform.position.y < 5)
                {
                    Instantiate(bullet, this.transform.position, Quaternion.identity);
                }
            }
            else
            {
                //�v���C���[�����U�����J�n������U�����J�n����
                if (Input.GetMouseButton(0)) isActive = true;
            }
            
            yield return new WaitForSeconds(coolTime);
        }

    }
}
