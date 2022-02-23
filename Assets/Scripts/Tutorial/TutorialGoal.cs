using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルで移動テスト用のゴールを制御するクラス
/// </summary>
public class TutorialGoal : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.NextStep();
            Destroy(this.gameObject);
        }
    }

}
