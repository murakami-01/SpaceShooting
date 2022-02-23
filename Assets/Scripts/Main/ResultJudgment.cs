using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 勝敗の判定を制御するクラス
/// </summary>
public class ResultJudgment : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    private PlayerAttack playerAttack;
    private PlayerMove playerMove;
    private PlayerHpManager playerHpManager;
    private CreateMainWave createMainWave;
    private AudioSource[] audioSources;

    public int num { get; set; } = 0;//倒した敵機の数
    public int maxNum { get; set; } = 41;//敵機の出現した数

    void Start()
    {
        audioSources = Camera.main.GetComponents<AudioSource>();
        createMainWave = this.gameObject.GetComponent<CreateMainWave>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerMove = player.GetComponent<PlayerMove>();
        playerHpManager = player.GetComponent<PlayerHpManager>();
    }

    /**
     * <summary>
     * プレイヤーが勝利した際の処理
     * </summary>
     * */
    public IEnumerator PlayerWin()
    {
        //プレイヤーとウェーブの処理を止めたうえでbossの演出待ち
        createMainWave.isFinish = true;
        playerAttack.isActive = false;
        playerMove.isActive = false;
        playerHpManager.isActive = false;
        yield return new WaitForSeconds(1);

        audioSources[1].Stop();
        audioSources[1].PlayOneShot(winClip);
        GameObject canvas = (GameObject) Instantiate(winCanvas);
        GameObject detail = canvas.transform.Find("Detail").gameObject;
        string value;
        if (num * 100 / maxNum >= 100)
        {
            value = $"討伐率 {num * 100 / maxNum}%  Perfect !!";
        }else if(num * 100 / maxNum >= 75)
        {
            value = $"討伐率 {num * 100 / maxNum}%  Great !";
        }else
        {
            value = $"討伐率 {num * 100 / maxNum}%  Good ";
        }
        detail.GetComponent<Text>().text = value;
        Debug.Log($"{num},{maxNum}");
    }


    /**
     * <summary>
     * プレイヤーが敗北した際の処理
     * </summary>
     * */
    public void PlayerLose()
    {
        audioSources[0].Stop();
        audioSources[1].Stop();
        audioSources[1].PlayOneShot(loseClip);
        createMainWave.isFinish = true;
        GameObject canvas = (GameObject)Instantiate(loseCanvas);
        GameObject detail = canvas.transform.Find("Detail").gameObject;
        string value= $"討伐率 {num * 100 / maxNum}%";
        detail.GetComponent<Text>().text = value;
        Debug.Log($"{num},{maxNum}");
    }
}
