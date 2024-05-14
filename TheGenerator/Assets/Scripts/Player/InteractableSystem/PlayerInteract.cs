using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private InputManager _inputManager;

    public RaycastHit hit;
    [SerializeField] private float raycastLength = 4f;

    private void Start()
    {
        _inputManager = InputManager.Instance;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastLength))
        {
            //search for Iinteractable
            Iinteractable interactOBJ = hit.collider.GetComponent<Iinteractable>();
            if (interactOBJ != null)
            {   
                //Enable Renderer Component
                ParticleSystem particleSystem = hit.collider.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    var particleRenderer = particleSystem.GetComponent<Renderer>();
                    if (particleRenderer != null)
                    {
                        particleRenderer.enabled = true;
                    }
                }
                Debug.Log("YOU FOUND ME!");
                //Found! script within the object with Iinteractable present
                if (_inputManager.IsPickUpPressed())
                {
                    Debug.Log("POKE! >w<");
                    //Input from input manager
                    //Input? Yes, fire event : No, return
                    interactOBJ.InteractEvent();
                }
            }
        }
    }
}
