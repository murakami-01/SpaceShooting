using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerHpManager hpManager;

    void Start()
    {
        playerAttack = this.gameObject.GetComponent<PlayerAttack>();
        hpManager = this.gameObject.GetComponent<PlayerHpManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            var itemCs = other.GetComponent<ItemManager>();
            var itemName = itemCs.ItemName;
            if (itemName == "recovery") hpManager.Recovery();
            else playerAttack.GetItem(itemName);
        }
    }
}
