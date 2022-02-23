using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���̋����𐧌䂷��N���X
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField] private float speed = 0.6f;
    public string itemName;
    private Rigidbody rb;


    public virtual void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -speed, 0);
    }

    void FixedUpdate()
    {
        //��ʊO�ɏo����j��
        if (this.transform.position.y <= -5.5f * ScreenAdjust.heightRatio) Destroy(this.gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //�v���C���[���l��
            Destroy(this.gameObject);
        }
    }
}
