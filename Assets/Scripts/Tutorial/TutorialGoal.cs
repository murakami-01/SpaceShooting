using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �`���[�g���A���ňړ��e�X�g�p�̃S�[���𐧌䂷��N���X
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
