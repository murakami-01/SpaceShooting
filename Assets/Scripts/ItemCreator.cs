using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    private System.Random r = new System.Random();
    private int noCreated = 1;
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();

    public void RandomCreate(Vector3 pos)
    {
        int result = r.Next(1, 100);
        if (result <= noCreated * 4)
        {
            int itemNum = r.Next(0, 4);
            Instantiate(itemList[itemNum], pos, Quaternion.identity);
            noCreated = 1;
        }
        else
        {
            noCreated++;
        }
    }
}
