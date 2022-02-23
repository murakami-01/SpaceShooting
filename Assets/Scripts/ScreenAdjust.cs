using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面サイズに対応した座標の調整を制御するクラス
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
        //カメラの調整
        float defaultAspect = defaultWigth / defaultHeigth;
        float actualAspect = (float)Screen.width / Screen.height;
        float ratio = actualAspect / defaultAspect;
        Camera.main.orthographicSize /= ratio;

        //縦横比の変動をstatic変数に格納
        Vector3 edgePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        widthRatio = edgePosition.x / defaultWidthEdge;
        heightRatio = edgePosition.y / defaultHeightEdge;

    }

}
