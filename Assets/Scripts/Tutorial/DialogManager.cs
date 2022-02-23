using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文字送りを制御するクラス
/// </summary>
public class DialogManager : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] [Range(0.001f, 0.3f)] private float textSpeed;
    private Queue<string> textList = new Queue<string>();
    private bool isFinished = true;
    private bool isShowAll = false;
    private string text;
    private float startTime;
    private float finishedTime;
    private float showAllTime;
    private int charNum;

    void Update()
    {
        if (!isFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //タップされたら文字列更新or全て表示
                if (isShowAll)
                {
                    SetNextText();
                }
                else
                {
                    finishedTime = 0;
                }
            }

            int nextNum = (int)(Mathf.Clamp01((Time.time - startTime) / finishedTime) * text.Length);
            nextNum = nextNum < 0 ? 0 : nextNum;//バグ対策
            if (nextNum != charNum)
            {
                //文字増える場合のみ更新
                charNum = nextNum;
                uiText.text = text.Substring(0, charNum);
                if (charNum == text.Length) isShowAll = true;
            }

            if (isShowAll)
            {
                //一定時間経過後文字列更新
                showAllTime += Time.deltaTime;
                if (showAllTime >= 1.2f) SetNextText();
            }
            
        }
    }

    /**
     * <summary>
     * 文字列を更新する処理
     * </summary>
     * */
    void SetNextText()
    {
        showAllTime = 0;
        isShowAll = false;
        if (textList.Count == 0)
        {
            //全て表示済み
            isFinished = true;
            tutorialManager.NextStep();
        }
        else
        {
            //まだ表示していない文章がある
            text = textList.Dequeue();
            startTime = Time.time;
            finishedTime = textSpeed * text.Length;
            charNum = 0;
        }
    }

    /**
     * <summary>
     * 文字列の表示を開始する
     * </summary>
     * <param name="messages"> 表示する文章</param>
     * */
    public void StartDialog(string[] messages)
    {
        isFinished = false;
        foreach (var s in messages) textList.Enqueue(s);
        SetNextText();
    }
}
