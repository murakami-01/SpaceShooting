using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// �`���[�g���A���S�̗̂���𐧌䂷��N���X
/// </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private TutorialPlayerAttack playerAttack;
    [SerializeField] private TutorialPlayerMove playerMove;
    [SerializeField] private GameObject mob;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject goal;
    [SerializeField] private SceneTransition sceneTransition;
    private int stageNum = 0;
    private int status = 0;

    void Start()
    {
        status = PlayerPrefs.GetInt("Status", 0);

        NonActivatePlayer();
        if (status == 0)
        {
            dialogManager.StartDialog(new string[]
            {"�͂��߂܂��āB\n���Ȃ�������P�����󂯂�p�C���b�g�ł��ˁB",
            "����A�P����S������i�r�Q�[�^�[�ł��B\n��낵�����肢���܂��B",
            "����ł͑����P�����͂��߂Ă����܂��傤�B",
            "�܂��͋@�̂̑��c���@�ɂ��Ăł��B",
            "��ʂ��^�b�v���A���̂܂܃X���C�v����ƁA���̕����ɋ@�̂������܂��B",
            "�����ɋ@�̂𓮂����āA�����~�ɐG�ꂳ���Ă݂Ă��������B"});
        }
        else
        {
            dialogManager.StartDialog(new string[]
            {"���v���Ԃ�ł��B\n���Ȃ�������P�����󂯂�p�C���b�g�ł��ˁB",
            "������P����S������i�r�Q�[�^�[�ł��B\n��낵�����肢���܂��B",
            "����ł͑����ČP�����͂��߂Ă����܂��傤�B",
            "�܂��͋@�̂̑��c���@�ɂ��Ăł��B",
            "��ʂ��^�b�v���A���̂܂܃X���C�v����ƁA���̕����ɋ@�̂������܂��B",
            "�����ɋ@�̂𓮂����āA�����~�ɐG�ꂳ���Ă݂Ă��������B"});
        }
        
    }


    /**
     * <summary>
     * �`���[�g���A���i�s�̏���
     * </summary>
     */
    public void NextStep()
    {
        stageNum++;
        switch (stageNum)
        {
            case 1:
                goal.SetActive(true);
                ActivatePlayer();
                break;
            case 2:
                NonActivatePlayer();
                dialogManager.StartDialog(new string[] 
                {"�@�̂̑��c�͖��Ȃ������ł��ˁB",
                "���Ȃ݂ɁA�^�b�v����ꏊ�͎��@�łȂ��Ă��\���܂���̂ŁA",
                "���������삵�₷���ꏊ��T���Ă݂Ă��������B",
                "�܂��A�C�Â��ꂽ�Ǝv���܂����A�ړ����͎����ōU�����s���܂��B",
                "����ł́A�U���̌P�������˂Ė͋[����s���܂��傤�B",
                "�G�̍U����G�@�ɓ�����Ȃ��悤�ɋC��t���Ă��������B"
                });
                break;
            case 3:
                ActivatePlayer();
                mob.SetActive(true);
                break;
            case 4:
                NonActivatePlayer();
                dialogManager.StartDialog(new string[]
                {"�������ł��B",
                "����A�ΐ푊�肪�Ȃɂ����Ƃ��Ă������悤�ł��ˁB",
                "������Ă݂Ă��������B"});
                item.SetActive(true);
                break;
            case 5:
                ActivatePlayer();
                break;
            case 6:
                NonActivatePlayer();
                playerAttack.FinishAttack();
                dialogManager.StartDialog(new string[]
                {"�ǂ����U�������𑝂₷���������������悤�ł��ˁB",
                "���̂悤�ɓG�����������𗎂Ƃ��Ă������Ƃ�����̂ŁA",
                "�������ꍇ�͂��Љ�����Ă݂Ă��������B",
                "�������l�����������͉�ʉ��Ŋm�F�ł��܂�",
                "�܂��A�G�̍U���Ȃǂɂ�����Ƌ������͂���A",
                "�ŏI�I�ɂ͎��@����ł��Ă��܂��̂ŋC��t���Ă��������B",
                "����ł́A�ȏ�ŌP�����I�����܂��B",
                "����ł̂���������F�肵�Ă��܂��B"});
                break;
            case 7:
                if (status == 0)
                {
                    PlayerPrefs.SetInt("Status", 1);
                    PlayerPrefs.Save();
                }
                playerMove.Finish();
                sceneTransition.enabled = true;
                break;
        }
    }

    /**
     * <summary>
     * �v���C���[�̍U���ƈړ����\�ɂ��鏈��
     * </summary>
     * */
    private void ActivatePlayer()
    {
        playerAttack.isActive = true;
        playerMove.StartMove();
    }

    /**
     * <summary>
     * �v���C���[�̍U���ƈړ���s�\�ɂ��鏈��
     * </summary>
     * */
    private void NonActivatePlayer()
    {
        playerAttack.isActive = false;
        playerMove.StopMove();
    }

}
