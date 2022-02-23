using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �����V�[���J�ڂ𐧌䂷��N���X
/// </summary>
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1.5f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }
}
