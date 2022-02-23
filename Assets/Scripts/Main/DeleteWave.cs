using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss登場までウェーブを制御するクラス
/// </summary>
public class DeleteWave : MonoBehaviour
{
    private GameObject sceneManager;
    private CreateMainWave createMainWave;
    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        createMainWave = sceneManager.GetComponent<CreateMainWave>();
    }

    private void Update()
    {
        //全滅したら親である自分も破壊
        int childrenNum = this.transform.childCount;
        if (childrenNum == 0)
        {
            createMainWave.CreateWave();
            Destroy(this.gameObject);
        }
    }
}
