using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʃT�C�Y�ɑΉ��������W�̒����𐧌䂷��N���X
/// </summary>
public class ScreenAdjust : MonoBehaviour
{
    private const float defaultWigth = 9.0f;
    private const float defaultHeigth = 16.0f;
    public static float widthRatio;
    public static float heightRatio;
    [System.NonSerialized] public const float defaultWidthEdge = 2.8f;
    [System.NonSerialized] public const float defaultHeightEdge = 5.1f;

    void Awake()
    {
        //�J�����̒���
        float defaultAspect = defaultWigth / defaultHeigth;
        float actualAspect = (float)Screen.width / Screen.height;
        float ratio = actualAspect / defaultAspect;
        Camera.main.orthographicSize /= ratio;

        //�c����̕ϓ���static�ϐ��Ɋi�[
        Vector3 edgePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        widthRatio = edgePosition.x / defaultWidthEdge;
        heightRatio = edgePosition.y / defaultHeightEdge;

    }

}
