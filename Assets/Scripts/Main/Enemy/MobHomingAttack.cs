using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホーミングmobの攻撃を制御するクラス
/// </summary>
public class MobHomingAttack : EnemyAttack
{
    public override IEnumerator Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        while (true)
        {
            if (this.transform.position.y <= 5.5f * ScreenAdjust.heightRatio)
            {
                //画面外なら攻撃
                GameObject bulletObject = (GameObject)Instantiate(bullet, this.transform.position, Quaternion.identity);
                bulletObject.GetComponent<EnemyHoming>().player = player;
            }
            yield return new WaitForSeconds(coolTime);

        }
    }
}
