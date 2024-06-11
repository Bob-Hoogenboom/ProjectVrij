using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float crossfadeTime;

    private int _crossfadeTrigger = Animator.StringToHash("StartCrossfade");

    public void LoadNextLevel()
    {
        StartCoroutine(Crossfade(SceneManager.GetActiveScene().buildIndex +1));
    }

    public void RestartLevel()
    {
        StartCoroutine(Crossfade(SceneManager.GetActiveScene().buildIndex));  
    }

    public void LoadMainMenu()
    {
        StartCoroutine(Crossfade(0));
    }

    IEnumerator Crossfade(int sceneIndex)
    {
        //play crossfade animation
        if(anim != null)
        {
            anim.SetTrigger(_crossfadeTrigger);
        }
        //wait
        yield return new WaitForSeconds(crossfadeTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
