using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mob‚ÌˆÚ“®‚ğ§Œä‚·‚éƒNƒ‰ƒX
/// </summary>
public class MobMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -speed, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
