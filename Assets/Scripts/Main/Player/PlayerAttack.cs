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

    protected List<GameObject> laserList = new List<GameObject>(2);
    protected int laserNum = 0;
    protected int drawnLaser = 0;
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
    }

    public virtual void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0) && !isActivated)
            {
                //��ʂ������ꂽ��U���J�n
                StartCoroutine(AttackCounter());
                isActivated = true;
                for(int i =1; i <= laserNum; i++)
                {
                    ActivateLaser(i);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                //��ʂ�������Ȃ��Ȃ�����U�����~
                StopAllCoroutines();
                isActivated = false;
                laserNum = drawnLaser;
                DeactivateLaser(drawnLaser);
            }
        }
        else
        {
            StopAllCoroutines();
            isActivated = false;
            laserNum = drawnLaser;
            DeactivateLaser(drawnLaser);
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
    public void NwayAttack(int num)
    {
        //���ꂼ��̉�]�p�����߂ăC���X�^���X����
        float nwayRange = Mathf.PI * (nwayDegree / 180);
        for(int i = 0; i < num*2; i++)
        {
            float theta = (nwayRange / (num*2-1)) * i + 0.5f * (Mathf.PI - nwayRange);
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
    public void HomingAttack(int num)
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
            for (int i = 0; i < num; i++)
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
     * ���[�U�[���w�肳�ꂽ�{������
     * </summary>
     * <param name="num">���{�������邩 </param>
     * */
    public void DeactivateLaser(int num)
    {
        num = Mathf.Min(num, drawnLaser);
        for(int i = 0; i < num; i++)
        {
            Destroy(laserList[drawnLaser - 1]);
            laserList.RemoveAt(drawnLaser - 1);
            drawnLaser--;
        }
        
    }

    /**
     * <summary>
     * ���[�U�[�̐���
     * </summary>
     * <param name="num"> ���[�U�[�����{�ڂ� </param>
     * */
    public void ActivateLaser(int num)
    {

        if (Input.GetMouseButton(0))
        {
            drawnLaser++;
            Vector3 position = this.gameObject.transform.position + new Vector3(0.25f * (1 - 2 * (num - 1)), 0.5f, 0);
            GameObject instance = (GameObject)Instantiate(Laser, position, Quaternion.identity);
            instance.transform.parent = this.gameObject.transform;
            PlayerLaser playerLaser = instance.GetComponent<PlayerLaser>();
            playerLaser.playerTrans = this.gameObject.transform;
            laserList.Add(instance);
        }
        else
        {
            laserNum++;
        }
        
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
