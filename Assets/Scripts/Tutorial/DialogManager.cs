using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������𐧌䂷��N���X
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
                //�^�b�v���ꂽ�當����X�Vor�S�ĕ\��
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
            nextNum = nextNum < 0 ? 0 : nextNum;//�o�O�΍�
            if (nextNum != charNum)
            {
                //����������ꍇ�̂ݍX�V
                charNum = nextNum;
                uiText.text = text.Substring(0, charNum);
                if (charNum == text.Length) isShowAll = true;
            }

            if (isShowAll)
            {
                //��莞�Ԍo�ߌ㕶����X�V
                showAllTime += Time.deltaTime;
                if (showAllTime >= 1.2f) SetNextText();
            }
            
        }
    }

    /**
     * <summary>
     * ��������X�V���鏈��
     * </summary>
     * */
    void SetNextText()
    {
        showAllTime = 0;
        isShowAll = false;
        if (textList.Count == 0)
        {
            //�S�ĕ\���ς�
            isFinished = true;
            tutorialManager.NextStep();
        }
        else
        {
            //�܂��\�����Ă��Ȃ����͂�����
            text = textList.Dequeue();
            startTime = Time.time;
            finishedTime = textSpeed * text.Length;
            charNum = 0;
        }
    }

    /**
     * <summary>
     * ������̕\�����J�n����
     * </summary>
     * <param name="messages"> �\�����镶��</param>
     * */
    public void StartDialog(string[] messages)
    {
        isFinished = false;
        foreach (var s in messages) textList.Enqueue(s);
        SetNextText();
    }
}
