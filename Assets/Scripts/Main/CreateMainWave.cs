using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mob�̃E�F�[�u�𐶐�����N���X
/// </summary>
public class CreateMainWave : MonoBehaviour
{
    [SerializeField] List<GameObject> waveList = new List<GameObject>();
    private int waveCount = 0;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject bossWave;
    [SerializeField] ResultJudgment resultJudgment;
    [SerializeField] private GameObject boss;
    private AudioSource[] audioSources;
    public bool isFinish { get; set; } = false;//���s�����Ă��邩

    private void Start()
    {
        audioSources = Camera.main.GetComponents<AudioSource>();
        CreateWave();
    }

    /**
     * <summary>
     * boss�o��܂ŃE�F�[�u���Ăԏ���
     * </summary>
     * */
    public void CreateWave()
    {

        if (waveCount < waveList.Count)
        {
            if (!isFinish)
            {
                StartCoroutine(InstantiateWave(waveCount));
            }
        }else if (waveCount == waveList.Count)
        {
            if (!isFinish)
            {
                StartCoroutine(ShowWrning());
            }
        }

        waveCount++;
    }

    /**
     * <summary>
     * ���ۂɎ��Ԃ������ăE�F�[�u�𐶐����鏈��
     * </summary>
     * */
    IEnumerator InstantiateWave(int count)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(waveList[count]);
    }

    /**
     * <summary>
     * boss�o���E�F�[�u���Ăԏ���(�E�F�[�u���S�ł����Ƃ��Ɏ��̃E�F�[�u���Ă΂��)
     * </summary>
     * */
    public void CreateBossWave()
    {
        if (!isFinish)
        {
            resultJudgment.maxNum += 4;
            Instantiate(bossWave);
        }
        
    }

    /**
     * <summary>
     * boss�o��܂ł̉��o�̏���
     * </summary>
     * */
    private IEnumerator ShowWrning()
    {
        yield return new WaitForSeconds(2);
        warning.SetActive(true);
        audioSources[0].Stop();
        yield return new WaitForSeconds(5);
        audioSources[1].Play();
        Destroy(warning);
        yield return new WaitForSeconds(2);
        Instantiate(boss);
        CreateBossWave();
    }
}
