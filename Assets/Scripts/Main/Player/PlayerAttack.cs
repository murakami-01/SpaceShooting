using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �v���C���[�̍U���𐧌䂷��N���X
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject nwayBullet;
    public GameObject homingBullet;
    public GameObject Laser;
    public int nwayCount = 4;
    public float nwayDegree = 90;
    public PlayerItemManager itemManager;
    public float coolTime = 0.13f;

    [System.NonSerialized]public List<GameObject> laserList = new List<GameObject>(2);
    [System.NonSerialized]public int laserNum = 0;
    public bool isActive { get; set; } = true;//�O������U���𐧌䂷�邽�߂̕ϐ�
    [System.NonSerialized]public bool isActivated = false;//�U�������ǂ���
    public Subject<int> Attack = new Subject<int>();

    /**
     * <summary>
     * ���ꂼ��̍U�������̍w��
     * </summary>
     * */
    public void Start()
    {
        Attack.Subscribe(n=>DefaultAttack());
        Attack.Where(n => itemManager.itemList.ContainsKey("nway")&&itemManager.itemList["nway"] > 0).Subscribe(n => NwayAttack());
        Attack.Where(n => itemManager.itemList.ContainsKey("homing") && itemManager.itemList["homing"] > 0 && n % 2 == 0).Subscribe(n => HomingAttack());
        Attack.Where(n => itemManager.itemList.ContainsKey("laser") && itemManager.itemList["laser"] > 0).Subscribe(n => LaserAttack());
    }

    public virtual void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButton(0) && !isActivated)
            {
                //��ʂ������ꂽ��U���J�n
                StartCoroutine(AttackCounter());
                isActivated = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                //��ʂ�������Ȃ��Ȃ�����U�����~
                StopAllCoroutines();
                isActivated = false;
                if (laserNum>0)
                {
                    laserList.ForEach(ins => Destroy(ins));
                    laserList.RemoveAll(i => i != null);
                    laserNum = 0;
                }
            }
        }
        else
        {
            StopAllCoroutines();
            isActivated = false;
            if (laserNum>0)
            {
                laserList.ForEach(ins => Destroy(ins));
                laserList.RemoveAll(i => i != null);
                laserNum = 0;
            }
        }
        
    }

    /**
     * <summary>
     * �U�����Ăё����鏈��
     * </summary>
     * */
    public IEnumerator AttackCounter()
    {
        int attackTime = 0;
        while (true)
        {
            Attack.OnNext(attackTime);
            yield return new WaitForSeconds(coolTime);
            attackTime++;
        }
    }
    
    /**
     * <summary>
     * �ʏ�U��
     * </summary>
     * */
    public void DefaultAttack()
    {
        Instantiate(normalBullet,this.transform.position,Quaternion.identity);
    }

    /**
     * <summary>
     * nway�U��
     * </summary>
     * */
    public void NwayAttack()
    {
        //�������₷���𔻒�
        if (itemManager.itemList["nway"] == 1) nwayCount = 2;
        else nwayCount = 4;

        //���ꂼ��̉�]�p�����߂ăC���X�^���X����
        float nwayRange = Mathf.PI * (nwayDegree / 180);
        for(int i = 0; i < nwayCount; i++)
        {
            float theta = (nwayRange / (nwayCount - 1)) * i + 0.5f * (Mathf.PI - nwayRange);
            GameObject nwayBulletObject = (GameObject)Instantiate(nwayBullet, this.transform.position, Quaternion.Euler(0, 0, theta / Mathf.PI * 180 - 90));
            PlayerNwayBullet nwayCs = nwayBulletObject.GetComponent < PlayerNwayBullet > ();
            nwayCs.theta = theta;
        }
    }


    /**
     * <summary>
     * �z�[�~���O�U��
     * </summary>
     * */
    public void HomingAttack()
    {
        //�G�̌���
        float distance = float.MaxValue;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject target = null;

        for(int i = 0; i < enemyList.Length; i++)
        {
            //�����������̂̒������ԋ߂����̂�T��
            float tempDistance = Vector3.Distance(this.transform.position, enemyList[i].transform.position);
            if (distance > tempDistance)
            {
                target = enemyList[i];
                distance = tempDistance;
            }
        }

        if (target != null)
        {
            for (int i = 0; i < itemManager.itemList["homing"]; i++)
            {
                //�C���X�^���X�������A�C���X�^���X�ɓG�̏���n��
                Vector3 position = this.transform.position + new Vector3(0.5f * (1 - 2 * i), 0.5f, 0);
                GameObject homingBulletObject = (GameObject)Instantiate(homingBullet, position, Quaternion.identity);
                var homingCs = homingBulletObject.GetComponent<PlayerHomingBullet>();
                homingCs.target = target;
            }
        }
        
    }

    /**
     * <summary>
     * ���[�U�[�U��
     * </summary>
     * */
    public void LaserAttack()
    {
        if (laserNum<itemManager.itemList["laser"])
        {
            //���[�U�[�̖{��������Ă��Ȃ��ꍇ�Ƀ��[�U�[�𐶐�
            for(int i=laserNum;i<itemManager.itemList["laser"]; i++)
            {
                ActivateLaser(i);
            }
            laserNum=itemManager.itemList["laser"];
        }

        if (itemManager.itemList["laser"] < laserNum)
        {
            //���[�U�[�̖{���������ꍇ�ɔj��
            Destroy(laserList[laserNum - 1]);
            laserList.RemoveAt(laserNum - 1);
            laserNum--;
        }

    }

    /**
     * <summary>
     * ���[�U�[�̐���
     * </summary>
     * <param name="num"> ���[�U�[�̖{�� </param>
     * */
    public void ActivateLaser(int num)
    {
        Vector3 position = this.gameObject.transform.position + new Vector3(0.25f * (1 - 2 * num), 0.5f, 0);
        GameObject instance = (GameObject)Instantiate(Laser, position, Quaternion.identity);
        instance.transform.parent = this.gameObject.transform;
        laserList.Add(instance);
    }

    /**
     * <summary>
     * �U�����~
     * </summary>
     * */
    public void StopAttack()
    {
        StopAllCoroutines();
    }
}
