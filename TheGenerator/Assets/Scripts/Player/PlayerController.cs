using UnityEngine;

/// <summary>
/// Extremely Basic character controller for the player with new Unity Input System reference
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Example : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    private InputManager inputManager;

    [Header("PlayerData")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    //# Change stats to scriptable object stats for powerups?
    [Header("PlayerStats")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        
        Vector2 moveValue = inputManager.GetMoveVector();
        Vector3 moveVector = new Vector3(moveValue.x, 0f, moveValue.y);
        controller.Move(moveVector * Time.deltaTime * playerSpeed);

        if (moveVector != Vector3.zero)
        {
            gameObject.transform.forward = moveVector;
        }

        // Changes the height position of the player..
        if (inputManager.IsJumpPressed() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
