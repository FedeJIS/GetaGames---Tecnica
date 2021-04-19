using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This class is used as a Loading Screen manager.
public class SceneLoading : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(1);
        StartCoroutine(Loading(scene));
    }
    IEnumerator Loading(int scene)
    {

        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(scene);
    }

    private void Awake() {
          DontDestroyOnLoad(this.gameObject);
    }
}
