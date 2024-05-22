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
        if (anim != null)
        {
            StartCoroutine(Crossfade(SceneManager.GetActiveScene().buildIndex +1));
        }
    }

    public void StartCrossFade(int sceneIndex)
    {
        if (anim != null)
        {
            StartCoroutine(Crossfade(sceneIndex));
        }
    }

    IEnumerator Crossfade(int sceneIndex)
    {
        //play crossfade animation
        anim.SetTrigger(_crossfadeTrigger);
        //wait
        yield return new WaitForSeconds(crossfadeTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
