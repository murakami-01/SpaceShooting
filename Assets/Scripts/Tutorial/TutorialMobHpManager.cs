using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルでmobのhpを制御するクラス
/// </summary>
public class TutorialMobHpManager : MobHpManager
{
    private TutorialManager tutorialManager;

    public override void Die()
    {
        //死亡時にTutorialManagerを呼ぶ
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
        tutorialManager = sceneManager.GetComponent<TutorialManager>();
        tutorialManager.NextStep();
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
