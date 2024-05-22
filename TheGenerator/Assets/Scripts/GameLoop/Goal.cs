using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public UnityEvent onEndingTriggered;
    public float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onEndingTriggered.Invoke();
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("MainMenu");
    }
}
