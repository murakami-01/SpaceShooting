using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss�U���p�N���X
/// </summary>
public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject normalBullet;
    [SerializeField] private GameObject homingBullet;
    [SerializeField] private float firstCoolTime;
    [SerializeField] private float secondCooltime;

    /**
    * <summary>
    *���i�K�̍U��
    *</summary> 
    **/
    public IEnumerator FirstAttack()
    {
        while (true)
        {
            //�E������2��
            for(int i = 0; i < 2; i++)
            {
                Instantiate(normalBullet, this.transform.position + new Vector3(0.2f, -0.7f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(firstCoolTime);

            //��������2��
            for(int i = 0; i < 2; i++)
            {
                Instantiate(normalBullet, this.transform.position + new Vector3(-0.2f, -0.7f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(firstCoolTime);
        }
    }

    /**
     * <summary>
     * ���i�K�̍U��
     * </summary>
     * */
    private IEnumerator SecondAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        while (true)
        {
            for(int i = 0; i < 2; i++)
            {
                var instance = (GameObject) Instantiate(homingBullet, this.transform.position + new Vector3(-0.4f + 0.8f*i, 0.9f, 0), Quaternion.identity);
                var enemyHomingCs = instance.GetComponent<EnemyHoming>();
                enemyHomingCs.player = player;
            }
            yield return new WaitForSeconds(secondCooltime);
        }
    }

    /**
     * <summary>
     * �U����2�i�K�ڂɈڍs
     * </summary>
     * */
    public void ChangeAttack()
    {
        StopAllCoroutines();
        StartCoroutine(SecondAttack());
    }


}
