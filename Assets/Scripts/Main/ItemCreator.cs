using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// アイテムのドロップを制御するクラス
/// </summary>
public class ItemCreator : MonoBehaviour
{
    private System.Random r = new System.Random();
    private int noCreated = 1;
    [SerializeField] private List<GameObject> itemObjectList = new List<GameObject>();
    private List<int> itemNumList = new List<int> { 1,2,2};

    /**
     * <summary>
     * アイテムがドロップするか抽選する処理
     * </summary>
     * <param name="pos"> アイテムを生成する座標</param>
     * */
    public void RandomCreate(Vector3 pos)
    {
        int result = r.Next(1, 100);
        if (result <= noCreated * 4)
        {
            //アイテムがドロップすること自体は確定したので、何がドロップするかを判定
            int sum = itemNumList.Sum();
            if (sum > 0)
            {
                int itemNum = r.Next(1, sum);
                int i = 0;
                while (itemNum > 0)
                {
                    itemNum -= itemNumList[i];
                    if (itemNum <= 0)
                    {
                        Instantiate(itemObjectList[i], pos, Quaternion.identity);
                        itemNumList[i]--;
                        noCreated = 1;
                        break;
                    }
                    if (i >= itemObjectList.Count) break;
                    i++;
                }
                
            }
            
        }
        else
        {
            //アイテムがドロップしなかった場合次回移行ドロップする確率が増加
            noCreated++;
        }
    }

    /**
     * <summary>
     * プレイヤーがアイテムを失った際にそれを抽選に反映させる処理
     * </summary>
     * <param name="name"> アイテム名</param> 
     * */
    public void AddItem(string name)
    {
        switch (name)
        {
            case "nway":
                itemNumList[0]++;
                break;
            case "homing":
                itemNumList[1]++;
                break;
            case "Laser":
                itemNumList[2]++;
                break;
        }
    }
}
