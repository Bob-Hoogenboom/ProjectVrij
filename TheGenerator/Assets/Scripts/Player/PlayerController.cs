using UnityEngine;

/// <summary>
/// Extremely Basic character controller for the player with new Unity Input System reference
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Example : MonoBehaviour
{
    [Header("References")]
    private CharacterController _controller;
    private InputManager _inputManager;
    private Transform _cameraTransform;

    [Header("PlayerData")]
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    //# Change stats to scriptable object stats for powerups?
    [Header("PlayerStats")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
        
        Vector2 moveValue = _inputManager.GetMoveVector();
        Vector3 moveVector = new Vector3(moveValue.x, 0f, moveValue.y);
        moveVector = _cameraTransform.forward * moveVector.z + _cameraTransform.right * moveVector.x;
        moveVector.y = 0f;

        _controller.Move(moveVector * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (_inputManager.IsJumpPressed() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
