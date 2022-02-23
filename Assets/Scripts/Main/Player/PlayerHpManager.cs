using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�̗̑͂𐧌䂷��N���X
/// </summary>
public class PlayerHpManager : HpManager
{
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject homingIcon;
    [SerializeField] public GameObject nwayIcon;
    [SerializeField] private GameObject laserIcon;
    [SerializeField] private PlayerItemManager itemManager;
    [SerializeField] private Transform itemCanvas;
    [SerializeField] private AudioSource audioSource;
    private ResultJudgment resultJudgment;
    private ItemCreator itemCreator;
    public bool isActive { get; set; } = true;
    public Stack<GameObject> iconStack = new Stack<GameObject>();
    public int offset {get;}= 110;
    public Vector3 basePosition { get; } = new Vector3(-283f, 18f, 0);
    public Vector3 baseScale { get; } = new Vector3(0.3f, 0.3f, 1);

    public virtual void Start()
    {
        resultJudgment = sceneManager.GetComponent<ResultJudgment>();
        itemCreator = sceneManager.GetComponent<ItemCreator>();

        //nway��1��������
        var nwayInstance = (GameObject)Instantiate(nwayIcon);
        SetIcon(nwayInstance);
        iconStack.Push(nwayInstance);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.CompareTag("EnemyBullet"))
            {
                Instantiate(damageEffect, this.transform.position, Quaternion.identity);
                Damage(1);
            }
            else if (other.CompareTag("Enemy"))
            {
                Instantiate(damageEffect, this.transform.position, Quaternion.identity);
                Damage(2);
            }
        }
    }

    public override void Damage(float damage)
    {
        audioSource.PlayOneShot(clip);
        if (damage == 1)
        {
            //������1����
            if (iconStack.Count == 0)
            {
                Die();
            }
            else
            {
                LostItem();
            }
        }
        else
        {
            //������2����
            if (iconStack.Count == 1)
            {
                Die();
            }
            else
            {
                for(int i = 0; i < 2; i++)
                {
                    LostItem();
                }
                
            }
        }
        
    }

    public override void Die()
    {
        Instantiate(effect, this.transform.position, Quaternion.identity);
        resultJudgment.PlayerLose();
        Destroy(this.gameObject);
    }

    /**
     * <summary>
     * �A�C�e�����l�������ۂ̏���(�����PlayerItemManager���S��)
     * </summary>
     * <param name="itemName"> �A�C�e����</param>
     * */
    public void GetItem(string itemName)
    {
        switch (itemName)
        {
            case "nway":
                var nwayInstance = (GameObject)Instantiate(nwayIcon);
                SetIcon(nwayInstance);
                iconStack.Push(nwayInstance);
                break;
            case "homing":
                var homingInstance = (GameObject)Instantiate(homingIcon);
                SetIcon(homingInstance);
                iconStack.Push(homingInstance);
                break;
            case "laser":
                var laserInstance = (GameObject)Instantiate(laserIcon);
                SetIcon(laserInstance);
                iconStack.Push(laserInstance);
                break;
        }
    }

    /**
     * <summary>
     * �A�C�e������������
     * </summary>
     * */
    public void LostItem()
    {
        var instance = iconStack.Pop();
        string itemName = instance.tag;
        itemManager.itemList[itemName]--;
        itemCreator.AddItem(itemName);
        Destroy(instance);
    }

    /**
     * <summary>
     * �l�������A�C�e���̃A�C�R����\��
     * </summary>
     * <param name="icon"> ���Y�A�C�e���̃A�C�R��</param>
     * */
    public void SetIcon(GameObject icon)
    {
        icon.transform.SetParent(itemCanvas);
        RectTransform iconTrans = icon.transform as RectTransform;
        iconTrans.anchoredPosition = basePosition + new Vector3(offset * iconStack.Count, 0, 0);
        iconTrans.localScale = baseScale;
    }
}
