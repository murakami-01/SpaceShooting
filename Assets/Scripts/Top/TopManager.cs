using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// トップ画面の処理の分岐を制御するクラス
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
            //初回時の処理
            FirstTopManager.enabled = true;
        }
        else
        {
            //チュートリアル済みの処理
            buttonCanvas.SetActive(true);
        }

    }

}
