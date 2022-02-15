using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyScriptable/BulletDataList")]
public class BulletDataList : ScriptableObject
{
    /// <summary>
    /// íeä€Ç…ä÷Ç∑ÇÈÉfÅ[É^ÇÃï€ë∂
    /// </summary>

    public List<BulletData> bulletDataList = new List<BulletData>();

}

[System.Serializable]
public class BulletData
{

    [SerializeField] private string bulletName;
    [SerializeField] private int attack;
    [SerializeField] private float speed;

    public string BulletName
    {
        private set { this.bulletName = value; }
        get { return bulletName; }
    }

    public int Attack
    {
        private set { this.attack = value; }
        get { return attack; }
    }

    public float Speed
    {
        private set { this.speed = value; }
        get { return speed; }
    }
}



