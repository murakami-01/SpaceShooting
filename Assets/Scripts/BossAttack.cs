using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject normalBullet;
    [SerializeField] private GameObject homingBullet;


    public IEnumerator FirstAttack()
    {
        while (true)
        {
            for(int i = 0; i < 2; i++)
            {
                Instantiate(normalBullet, this.transform.position + new Vector3(0.2f, -0.7f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);

            for(int i = 0; i < 2; i++)
            {
                Instantiate(normalBullet, this.transform.position + new Vector3(-0.2f, -0.7f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

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
            yield return new WaitForSeconds(2);
        }
    }

    public void ChangeAttack()
    {
        StopAllCoroutines();
        StartCoroutine(SecondAttack());
    }


}
