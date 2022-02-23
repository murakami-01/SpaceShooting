using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// チュートリアル全体の流れを制御するクラス
/// </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private TutorialPlayerAttack playerAttack;
    [SerializeField] private TutorialPlayerMove playerMove;
    [SerializeField] private GameObject mob;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject goal;
    [SerializeField] private SceneTransition sceneTransition;
    private int stageNum = 0;
    private int status = 0;

    void Start()
    {
        status = PlayerPrefs.GetInt("Status", 0);

        NonActivatePlayer();
        if (status == 0)
        {
            dialogManager.StartDialog(new string[]
            {"はじめまして。\nあなたが今回訓練を受けるパイロットですね。",
            "今回、訓練を担当するナビゲーターです。\nよろしくお願いします。",
            "それでは早速訓練をはじめていきましょう。",
            "まずは機体の操縦方法についてです。",
            "画面をタップし、そのままスワイプすると、その方向に機体も動きます。",
            "試しに機体を動かして、白い円に触れさせてみてください。"});
        }
        else
        {
            dialogManager.StartDialog(new string[]
            {"お久しぶりです。\nあなたが今回訓練を受けるパイロットですね。",
            "今回も訓練を担当するナビゲーターです。\nよろしくお願いします。",
            "それでは早速再訓練をはじめていきましょう。",
            "まずは機体の操縦方法についてです。",
            "画面をタップし、そのままスワイプすると、その方向に機体も動きます。",
            "試しに機体を動かして、白い円に触れさせてみてください。"});
        }
        
    }


    /**
     * <summary>
     * チュートリアル進行の処理
     * </summary>
     */
    public void NextStep()
    {
        stageNum++;
        switch (stageNum)
        {
            case 1:
                goal.SetActive(true);
                ActivatePlayer();
                break;
            case 2:
                NonActivatePlayer();
                dialogManager.StartDialog(new string[] 
                {"機体の操縦は問題なさそうですね。",
                "ちなみに、タップする場所は自機でなくても構いませんので、",
                "自分が操作しやすい場所を探してみてください。",
                "また、気づかれたと思いますが、移動中は自動で攻撃を行います。",
                "それでは、攻撃の訓練もかねて模擬戦を行いましょう。",
                "敵の攻撃や敵機に当たらないように気を付けてください。"
                });
                break;
            case 3:
                ActivatePlayer();
                mob.SetActive(true);
                break;
            case 4:
                NonActivatePlayer();
                dialogManager.StartDialog(new string[]
                {"お見事です。",
                "おや、対戦相手がなにか落としていったようですね。",
                "回収してみてください。"});
                item.SetActive(true);
                break;
            case 5:
                ActivatePlayer();
                break;
            case 6:
                NonActivatePlayer();
                playerAttack.FinishAttack();
                dialogManager.StartDialog(new string[]
                {"どうやら攻撃方向を増やす強化物資だったようですね。",
                "このように敵が強化物資を落としていくことがあるので、",
                "見つけた場合はぜひ回収してみてください。",
                "自分が獲得した物資は画面下で確認できます",
                "また、敵の攻撃などにあたると強化がはがれ、",
                "最終的には自機が壊滅してしまうので気を付けてください。",
                "それでは、以上で訓練を終了します。",
                "実戦でのご活躍をお祈りしています。"});
                break;
            case 7:
                if (status == 0)
                {
                    PlayerPrefs.SetInt("Status", 1);
                    PlayerPrefs.Save();
                }
                playerMove.Finish();
                sceneTransition.enabled = true;
                break;
        }
    }

    /**
     * <summary>
     * プレイヤーの攻撃と移動を可能にする処理
     * </summary>
     * */
    private void ActivatePlayer()
    {
        playerAttack.isActive = true;
        playerMove.StartMove();
    }

    /**
     * <summary>
     * プレイヤーの攻撃と移動を不可能にする処理
     * </summary>
     * */
    private void NonActivatePlayer()
    {
        playerAttack.isActive = false;
        playerMove.StopMove();
    }

}
