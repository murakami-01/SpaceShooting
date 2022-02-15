using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private float speed = 0.6f;
    [SerializeField] private string itemName;
    private Rigidbody rb;

    public string ItemName
    {
        get { return this.itemName; }
    }

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 distance = new Vector3(0, speed * Time.deltaTime, 0);
        rb.MovePosition(this.transform.position - distance);

        if (this.transform.position.y <= -7) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
