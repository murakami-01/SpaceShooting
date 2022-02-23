using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mobのウェーブを生成するクラス
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
    public bool isFinish { get; set; } = false;//勝敗がついているか

    private void Start()
    {
        audioSources = Camera.main.GetComponents<AudioSource>();
        CreateWave();
    }

    /**
     * <summary>
     * boss登場までウェーブを呼ぶ処理
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
     * 実際に時間をおいてウェーブを生成する処理
     * </summary>
     * */
    IEnumerator InstantiateWave(int count)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(waveList[count]);
    }

    /**
     * <summary>
     * boss登場後ウェーブを呼ぶ処理(ウェーブが全滅したときに次のウェーブが呼ばれる)
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
     * boss登場までの演出の処理
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
