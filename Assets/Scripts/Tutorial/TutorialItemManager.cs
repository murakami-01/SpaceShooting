using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルでアイテムの挙動を制御するクラス
/// </summary>
public class TutorialItemManager : ItemManager
{
    [SerializeField] private TutorialManager tutorialManager;

    public override void Start()
    {
        return;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.NextStep();
            Destroy(this.gameObject);
        }
    }
}
