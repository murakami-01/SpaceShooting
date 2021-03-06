using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通常mobの攻撃を制御するクラス
/// </summary>
public class MobAttack : EnemyAttack
{
    public override IEnumerator Attack()
    {
        while (true)
        {
            if (this.gameObject.transform.position.y < 5.5f*ScreenAdjust.heightRatio)
            {
                //画面内なら攻撃
                Instantiate(bullet, this.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(coolTime);
        }

    }
}
