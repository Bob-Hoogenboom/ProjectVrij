using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Add this to a Object the player can interact with
/// </summary>
public class InteractTest : MonoBehaviour, Iinteractable
{
    public UnityEvent hasInteracted;

    public void InteractEvent()
    {
        hasInteracted.Invoke();
    }

    public void PickUp()
    {
        //Debug.Log("Pick up half Life 2 style");
        transform.position = Vector3.zero;
    }
}
