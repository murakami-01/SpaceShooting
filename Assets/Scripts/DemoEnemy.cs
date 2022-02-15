using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnemy : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] float speed = 0.5f;

    private void Update()
    {
        this.gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if (this.gameObject.transform.position.x > 2.2 || this.gameObject.transform.position.x < -2.2)
        {
            speed *= -1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            var n = other.GetComponent<BulletManager>();
            hp -= n.Attack;
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
