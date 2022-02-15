using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int childrenNum = this.transform.childCount;
        if (childrenNum == 0)
        {
            createMainWave.CreateWave();
            Destroy(this.gameObject);
        }
    }
}
