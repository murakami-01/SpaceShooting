using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルでmobの攻撃を制御するクラス
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
                //プレイヤー側が攻撃を開始したら攻撃を開始する
                if (Input.GetMouseButton(0)) isActive = true;
            }
            
            yield return new WaitForSeconds(coolTime);
        }

    }
}
