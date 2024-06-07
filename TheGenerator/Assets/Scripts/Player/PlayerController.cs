using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Extremely Basic character controller for the player with new Unity Input System reference
/// source: https://www.youtube.com/watch?v=5n_hmqHdijM 
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
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
    private float _currentSpeed;
    [Space]
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [Space]
    [SerializeField] private float crouchHeight = .5f;
    [SerializeField] private float normalHeight = 1f;

    [HideInInspector] public bool _isChrouching;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;
        _cameraTransform = Camera.main.transform;

        _currentSpeed = playerSpeed;
    }

    private void Update()
    {
        _groundedPlayer = _controller.isGrounded;

        Move();
        Hop();

        if (_inputManager.IsCrouchedPressed() && _groundedPlayer)
        {
            Crouch();
        }
    }

    //Handles the movement of the player
    private void Move()
    {
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector2 moveValue = _inputManager.GetMoveVector();
        Vector3 moveVector = new Vector3(moveValue.x, 0f, moveValue.y);

        moveVector = _cameraTransform.forward * moveVector.z + _cameraTransform.right * moveVector.x;
        moveVector.y = 0f;

        _controller.Move(moveVector * Time.deltaTime * _currentSpeed);
    }

    //Makes the player do a little 'hop' when space bar is pressed
    private void Hop()
    {
        if (_inputManager.IsJumpPressed() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void Crouch()
    {
        _isChrouching = !_isChrouching;

        float playerHeight = _isChrouching ? crouchHeight : normalHeight;
        _currentSpeed = _isChrouching ? playerSpeed / 2 : playerSpeed;
        
        if (!_isChrouching)
        {
            Vector3 playerPos = transform.position; //Makes it more tidy
            gameObject.transform.position = new Vector3(playerPos.x, playerPos.y + normalHeight, playerPos.z) ;
        }

        transform.localScale = new Vector3(transform.localScale.x, playerHeight, transform.localScale.z);
    }
}
