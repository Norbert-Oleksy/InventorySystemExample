using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private Inputs _inputs;

    #region Actions
    public InputAction attack {  get; private set; }
    public InputAction move { get; private set; }
    public InputAction look { get; private set; }

    #endregion

    #region Unity-Api
    private void Awake()
    {
        Instance = this;
        _inputs = new Inputs();

        attack = _inputs.Player.Attack;
        move = _inputs.Player.Move;
        look = _inputs.Player.Look;
    }

    private void OnEnable()
    {
        attack.Enable();
        move.Enable();
        look.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
        move.Disable();
        look.Disable();
    }
    #endregion
}