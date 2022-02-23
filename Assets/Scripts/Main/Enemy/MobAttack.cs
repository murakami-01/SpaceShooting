using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ’Êímob‚ÌUŒ‚‚ğ§Œä‚·‚éƒNƒ‰ƒX
/// </summary>
public class MobAttack : EnemyAttack
{
    public override IEnumerator Attack()
    {
        while (true)
        {
            if (this.gameObject.transform.position.y < 5.5f*ScreenAdjust.heightRatio)
            {
                //‰æ–Ê“à‚È‚çUŒ‚
                Instantiate(bullet, this.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(coolTime);
        }

    }
}
