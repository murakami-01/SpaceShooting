using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのアイテムを管理するクラス
/// </summary>
public class PlayerItemManager : MonoBehaviour
{
    private PlayerHpManager hpManager;
    private PlayerAttack playerAttack;
    public Dictionary<string, int> itemList { get; set; } = new Dictionary<string, int> { ["nway"] = 1 };

    void Start()
    {
        hpManager = this.gameObject.GetComponent<PlayerHpManager>();
        playerAttack = this.gameObject.GetComponent<PlayerAttack>();
        playerAttack.Attack.Subscribe(n =>playerAttack.NwayAttack(itemList["nway"]));
    }

    /**
     * <summary>
     * アイテムを取得した際に攻撃を強化する
     * </summary>
     * <param name="itemName"> アイテムの名前</param>
     * */
    public void GetItem(string itemName)
    {
        if (itemName == "laser")
        {
            if (itemList.ContainsKey(itemName))
            {
                itemList[itemName]++;
                playerAttack.ActivateLaser(itemList[itemName]);
            }
            else
            {
                itemList.Add(itemName, 1);
                playerAttack.ActivateLaser(1);
            }
        }
        else
        {
            if (itemList.ContainsKey(itemName))
            {
                itemList[itemName]++;
            }
            else
            {
                itemList.Add(itemName, 1);
                if (itemName == "homing")
                {
                    playerAttack.Attack.Subscribe(n => playerAttack.HomingAttack(itemList["homing"]));
                }
            }
        }
    }

    /**
     * <summary>
     * レーザーを１本消す
     * </summary>
     * */
    public void DeleteLaser()
    {
        playerAttack.DeactivateLaser(1);
    }
}
