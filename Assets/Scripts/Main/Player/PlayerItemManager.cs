using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアイテムを管理するクラス
/// </summary>
public class PlayerItemManager : MonoBehaviour
{
    private PlayerHpManager hpManager;
    public Dictionary<string, int> itemList { get; set; } = new Dictionary<string, int> { ["nway"] = 1, ["homing"] = 0, ["laser"] = 0 };

    void Start()
    {
        hpManager = this.gameObject.GetComponent<PlayerHpManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            var itemCs = other.GetComponent<ItemManager>();
            var itemName = itemCs.itemName;

            if (itemList.ContainsKey(itemName))
            {
                itemList[itemName]++;
            }
            else
            {
                itemList.Add(itemName, 1);
            }

            hpManager.GetItem(itemName);
        }
    }
}
