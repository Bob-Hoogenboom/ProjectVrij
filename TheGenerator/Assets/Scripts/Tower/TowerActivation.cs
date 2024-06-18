using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TowerActivation : MonoBehaviour
{
    public UnityEvent onExitTrigger;
    public UnityEvent onEnterTrigger;

    private void OnTriggerExit(Collider other)
    {
        //if its not the player exiting the trigger, do nothing
        if (!other.gameObject.CompareTag("Player")) return;


        onExitTrigger.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if its not the player exiting the trigger, do nothing
        if (!other.gameObject.CompareTag("Player")) return;


        onEnterTrigger.Invoke();
    }
}
 