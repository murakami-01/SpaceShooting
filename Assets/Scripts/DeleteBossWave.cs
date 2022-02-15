using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBossWave : MonoBehaviour
{
    private GameObject sceneManager;
    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
    }

    private void Update()
    {
        int childrenNum = this.transform.childCount;
        if (childrenNum == 0)
        {
            CreateMainWave createMainWave = sceneManager.GetComponent<CreateMainWave>();
            createMainWave.CreateBossWave();
            Destroy(this.gameObject);
        }
    }
}
