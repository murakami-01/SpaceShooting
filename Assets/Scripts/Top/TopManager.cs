using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �g�b�v��ʂ̏����̕���𐧌䂷��N���X
/// </summary>
public class TopManager : MonoBehaviour
{
    [SerializeField] private FirstTopManager FirstTopManager;
    [SerializeField] private GameObject buttonCanvas;

    void Awake()
    {

        int status = PlayerPrefs.GetInt("Status", 0);
        if (status == 0)
        {
            //���񎞂̏���
            FirstTopManager.enabled = true;
        }
        else
        {
            //�`���[�g���A���ς݂̏���
            buttonCanvas.SetActive(true);
        }

    }

}
