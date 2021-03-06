using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss登場後ウェーブを制御するクラス
/// </summary>
public class DeleteBossWave : MonoBehaviour
{
    private GameObject sceneManager;
    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
    }

    private void Update()
    {
        //全滅したら次のウェーブを呼ぶ
        int childrenNum = this.transform.childCount;
        if (childrenNum == 0)
        {
            CreateMainWave createMainWave = sceneManager.GetComponent<CreateMainWave>();
            createMainWave.CreateBossWave();
            Destroy(this.gameObject);
        }
    }
}
