using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultJudgment : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject winBackground;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private GameObject loseBackground;
    [SerializeField] private GameObject player;
    private PlayerAttack playerAttack;
    private PlayerMove playerMove;
    private PlayerHpManager playerHpManager;
    private CreateMainWave createMainWave;

    public int num { get; set; }
    public int maxNum { get; set; } = 25;

    void Start()
    {
        createMainWave = this.gameObject.GetComponent<CreateMainWave>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerMove = player.GetComponent<PlayerMove>();
        playerHpManager = player.GetComponent<PlayerHpManager>();
    }

    public IEnumerator PlayerWin()
    {
        createMainWave.isFinish = true;
        playerAttack.isActive = false;
        playerMove.isActive = false;
        playerHpManager.isActive = false;
        yield return new WaitForSeconds(1);

        Instantiate(winBackground);
        GameObject canvas = (GameObject) Instantiate(winCanvas);
        GameObject detail = canvas.transform.Find("Detail").gameObject;
        string value;
        if (num * 100 / maxNum >= 95)
        {
            value = $"“¢”°—¦ {num * 100 / maxNum}%  Perfect !!";
        }else if(num * 100 / maxNum >= 95)
        {
            value = $"“¢”°—¦ {num * 100 / maxNum}%  Greate !";
        }else
        {
            value = $"“¢”°—¦ {num * 100 / maxNum}%  Good ";
        }
        detail.GetComponent<Text>().text = value;
    }

    public void PlayerLose()
    {
        createMainWave.isFinish = true;
        Instantiate(loseBackground);
        GameObject canvas = (GameObject)Instantiate(loseCanvas);
        GameObject detail = canvas.transform.Find("Detail").gameObject;
        string value= $"“¢”°—¦ {num * 100 / maxNum}%";
        detail.GetComponent<Text>().text = value;
    }
}
