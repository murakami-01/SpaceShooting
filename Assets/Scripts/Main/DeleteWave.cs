using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss�o��܂ŃE�F�[�u�𐧌䂷��N���X
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
        //�S�ł�����e�ł��鎩�����j��
        int childrenNum = this.transform.childCount;
        if (childrenNum == 0)
        {
            createMainWave.CreateWave();
            Destroy(this.gameObject);
        }
    }
}
