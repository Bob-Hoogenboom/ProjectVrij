using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TowerActivation : MonoBehaviour
{
    public UnityEvent OnExitTrigger;

    private void OnTriggerExit(Collider other)
    {
        //if its not the player exiting the trigger, do nothing
        if (!other.gameObject.CompareTag("Player")) return;


        OnExitTrigger.Invoke();
    }
}
 