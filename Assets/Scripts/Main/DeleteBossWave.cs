using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss�o���E�F�[�u�𐧌䂷��N���X
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
        //�S�ł����玟�̃E�F�[�u���Ă�
        int childrenNum = this.transform.childCount;
        if (childrenNum == 0)
        {
            CreateMainWave createMainWave = sceneManager.GetComponent<CreateMainWave>();
            createMainWave.CreateBossWave();
            Destroy(this.gameObject);
        }
    }
}
