using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾丸に関するデータの保存用クラス
/// </summary>
[CreateAssetMenu(menuName ="MyScriptable/BulletDataList")]
public class BulletDataList : ScriptableObject
{
    public List<BulletData> bulletDataList = new List<BulletData>();

}

/// <summary>
/// 弾丸に関するデータの中身のクラス
/// </summary>
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



