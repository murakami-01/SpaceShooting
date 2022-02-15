using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneClickTransition : MonoBehaviour
{
    private GameObject player;
    private bool isActivate = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SceneLoad(string name)
    {
        if (!isActivate)
        {
            StartCoroutine(PlayerTransition(name));
            isActivate = true;
        }
    }

    

    public IEnumerator PlayerTransition(string name)
    {
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, 500, 0));
            yield return new WaitForSeconds(1);
        }
        
        SceneManager.LoadScene(name);
    }
}
