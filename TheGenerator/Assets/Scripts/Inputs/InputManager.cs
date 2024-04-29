using UnityEngine;

/// <summary>
/// Singleton InputManager used to get a reference to the inputactions
/// </summary>
/// 
public class InputManager : MonoBehaviour
{
    private PlayerActions _playerActions;

    private static InputManager _instance;   
    public static InputManager Instance 
    {  
        get 
        { 
            return _instance; 
        } 
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        _playerActions = new PlayerActions();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();   
    }

    #region HelperFunctions
    public Vector2 GetMoveVector()
    {
        return _playerActions.Movement.Move.ReadValue<Vector2>();
    }
    public bool IsJumpPressed()
    {
        return _playerActions.Movement.Jump.triggered;
    }

    public Vector2 GetMouseDelta()
    {
        return _playerActions.Movement.Look.ReadValue<Vector2>();
    }
    #endregion
}
