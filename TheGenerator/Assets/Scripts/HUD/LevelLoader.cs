using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private int _crossfadeOut = Animator.StringToHash("CrossfadeOut");
    public void LoadNextLevel(int crossfadeTimer)
    {
        StartCoroutine(Crossfade(crossfadeTimer, SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartLevel(int crossfadeTimer)
    {
        StartCoroutine(Crossfade(crossfadeTimer, SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadMainMenu(int crossfadeTimer)
    {
        StartCoroutine(Crossfade(crossfadeTimer, 0));
    }

    IEnumerator Crossfade(int crossfadeTimer, int sceneIndex)
    {
        //play crossfade animation
        if(anim != null)
        {
            anim.SetTrigger(_crossfadeOut);
        }
        //wait
        Debug.Log("Wait");
        yield return new WaitForSeconds(crossfadeTimer);
        Debug.Log("Switch");
        SceneManager.LoadScene(sceneIndex);
    }
}
