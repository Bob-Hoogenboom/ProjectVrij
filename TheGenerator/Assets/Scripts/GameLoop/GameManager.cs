using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float introDelay = 10f;

    public UnityEvent onBeginStart;
    public UnityEvent onBeginEnd;

    private void Start()
    {
        BeginGame();
    }

    private IEnumerator BeginGame()
    {
        onBeginStart.Invoke();

        yield return new WaitForSeconds(introDelay);

        onBeginEnd.Invoke();

        yield return null;
    }
}
