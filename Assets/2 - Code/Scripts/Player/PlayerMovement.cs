using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _defoaultSpeed = 5;
    [SerializeField] private float _defoaultlookSensitivity = 100;

    private CharacterController _characterController;
    private Vector2 _moveDirection;
    private Vector2 _lookDirection;

    #region Unity-API
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(TestManager.Instance != null && TestManager.Instance.Stage != TestManager.TestStage.Running) return;

        RotatePlayer();
        MovePlayer();
    }

    private void Start()
    {
        InputManager.Instance.move.performed += GetMoveDirection;
        InputManager.Instance.move.canceled += _ => _moveDirection = Vector2.zero;

        InputManager.Instance.look.performed += GetLookDirection;
        InputManager.Instance.look.canceled += _ => _lookDirection = Vector2.zero;
    }

    private void OnDisable()
    {
        InputManager.Instance.move.performed -= GetMoveDirection;
        InputManager.Instance.move.canceled -= _ => _moveDirection = Vector2.zero;

        InputManager.Instance.look.performed -= GetLookDirection;
        InputManager.Instance.look.canceled -= _ => _lookDirection = Vector2.zero;
    }
    #endregion

    #region Logic
    private void GetMoveDirection(InputAction.CallbackContext context) => _moveDirection = context.ReadValue<Vector2>();
    private void GetLookDirection(InputAction.CallbackContext context) => _lookDirection = context.ReadValue<Vector2>();

    private void MovePlayer()
    {
        Vector3 moveVector = transform.right * _moveDirection.x + transform.forward * _moveDirection.y;
        _characterController.Move(moveVector * Time.deltaTime * _defoaultSpeed);
    }

    private void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _lookDirection.x * _defoaultlookSensitivity * Time.deltaTime);
    }

    #endregion
}