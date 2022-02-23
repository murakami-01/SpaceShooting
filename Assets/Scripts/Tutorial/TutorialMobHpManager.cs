using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �`���[�g���A����mob��hp�𐧌䂷��N���X
/// </summary>
public class TutorialMobHpManager : MobHpManager
{
    private TutorialManager tutorialManager;

    public override void Die()
    {
        //���S����TutorialManager���Ă�
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
        tutorialManager = sceneManager.GetComponent<TutorialManager>();
        tutorialManager.NextStep();
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
