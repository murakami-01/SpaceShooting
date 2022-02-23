using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// ‰‰ñ‹N“®‚Ì‹““®‚ğ§Œä‚·‚éƒNƒ‰ƒX
/// </summary>
public class FirstTopManager : MonoBehaviour
{
    [SerializeField] private SceneClickTransition clickTransition;
    [SerializeField] private GameObject textCanvas;

    private void Start()
    {
        textCanvas.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            textCanvas.SetActive(false);
            clickTransition.StartCoroutine(clickTransition.PlayerTransition("DarkToTutorial"));
        }
    }
}
