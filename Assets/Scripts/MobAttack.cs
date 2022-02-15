using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : EnemyAttack
{

    public override IEnumerator Attack()
    {
        while (true)
        {
            if (this.gameObject.transform.position.y < 5)
            {
                Instantiate(bullet, this.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(coolTime);
        }

    }
}
