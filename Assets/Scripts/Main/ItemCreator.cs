using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �A�C�e���̃h���b�v�𐧌䂷��N���X
/// </summary>
public class ItemCreator : MonoBehaviour
{
    private System.Random r = new System.Random();
    private int noCreated = 1;
    [SerializeField] private List<GameObject> itemObjectList = new List<GameObject>();
    private List<int> itemNumList = new List<int> { 1,2,2};

    /**
     * <summary>
     * �A�C�e�����h���b�v���邩���I���鏈��
     * </summary>
     * <param name="pos"> �A�C�e���𐶐�������W</param>
     * */
    public void RandomCreate(Vector3 pos)
    {
        int result = r.Next(1, 100);
        if (result <= noCreated * 50)
        {
            //�A�C�e�����h���b�v���邱�Ǝ��̂͊m�肵���̂ŁA�����h���b�v���邩�𔻒�
            int sum = itemNumList.Sum();
            if (sum > 0)
            {
                int itemNum = r.Next(1, sum);
                Debug.Log($"List:{itemNumList[0]},{itemNumList[1]},{itemNumList[2]}, num {itemNum}, max {sum}");
                for (int i = 0; i < itemNumList.Count; i++)
                {
                    itemNum -= itemNumList[i];
                    if (itemNum <= 0)
                    {
                        GameObject instance = (GameObject) Instantiate(itemObjectList[i], pos, Quaternion.identity);
                        instance.GetComponent<ItemManager>().itemCreator = this;
                        itemNumList[i]--;
                        noCreated = 1;
                        break;
                    }
                }
                
            }
            
        }
        else
        {
            //�A�C�e�����h���b�v���Ȃ������ꍇ����ȍ~�h���b�v����m��������
            noCreated++;
        }
    }

    /**
     * <summary>
     * �v���C���[���A�C�e�����������ۂɂ���𒊑I�ɔ��f�����鏈��
     * </summary>
     * <param name="name"> �A�C�e����</param> 
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
            case "laser":
                itemNumList[2]++;
                break;
        }
    }
}
