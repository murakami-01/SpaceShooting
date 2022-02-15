using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWarning : MonoBehaviour
{
    public GameObject warning { get; set; }
    [SerializeField] private GameObject boss;
    private CreateMainWave createMainWave;
    private float time = 0f;

    void Start()
    {
        createMainWave = this.gameObject.GetComponent<CreateMainWave>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5f)
        {
            Destroy(warning);
            Instantiate(boss);
            createMainWave.CreateBossWave();
            Destroy(this);
        }
    }
}
