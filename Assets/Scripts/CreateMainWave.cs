using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CreateMainWave : MonoBehaviour
{
    [SerializeField] List<GameObject> waveList = new List<GameObject>();
    private int waveCount = 0;
    [SerializeField] private ShowWarning showWarning;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject bossWave;
    [SerializeField] ResultJudgment resultJudgment;
    public bool isFinish { get; set; } = false;

    private void Start()
    {
        CreateWave();
    }

    public void CreateWave()
    {

        if (waveCount < waveList.Count)
        {
            if (!isFinish)
            {
                Instantiate(waveList[waveCount]);
            }
        }else if (waveCount == waveList.Count)
        {
            if (!isFinish)
            {
                showWarning.enabled = true;
                warning.SetActive(true);
                showWarning.warning = warning;
            }
        }

        waveCount++;
    }

    public void CreateBossWave()
    {
        if (!isFinish)
        {
            resultJudgment.maxNum += 4;
            Instantiate(bossWave);
        }
        
    }
}
