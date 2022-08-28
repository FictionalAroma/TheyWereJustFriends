using System;
using RainbowJam.Controls;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputControls.IPlayerActions
{
    #region Consts
    private static readonly int PlayerMoveY = Animator.StringToHash("PlayerMoveY");
    private static readonly int PlayerMoveX = Animator.StringToHash("PlayerMoveX");

    #endregion

    #region UnityFields

    [SerializeField] private float movementFactor;

    #endregion


    #region Unity Cache

    private PlayerInputControls _controls;
    private InputAction _move;
    private InputAction _fire;
    private Animator _animator;

    #endregion

    private Vector2 _currentMoveInputVector;


    private void Awake()
    {
        _controls = new PlayerInputControls();
        _animator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        EnableControls();
    }

    private void EnableControls()
    {
        _controls.Player.SetCallbacks(this);
        _controls.Enable();
        _controls.Player.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = _currentMoveInputVector * (Time.deltaTime * movementFactor);

        var playerXMovementAbs = Mathf.Abs(movementVector.x);

        transform.Translate(movementVector);
        _animator.SetFloat(PlayerMoveX, playerXMovementAbs);
        _animator.SetFloat(PlayerMoveY, movementVector.y);

        if (playerXMovementAbs > Mathf.Epsilon)
        {
            var temp = transform.localScale;
            temp.x = movementVector.x > 0 ? 1f : -1f;
            transform.localScale = temp;
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMoveInputVector = !context.canceled ? context.ReadValue<Vector2>() : Vector2.zero;
    }

    public void OnLook(InputAction.CallbackContext context) {  }

    public void OnFire(InputAction.CallbackContext context) { }


    private void OnDisable()
    {
        DisableControls();
    }

    private void OnDestroy()
    {
        DisableControls();
    }

    private void DisableControls()
    {
        _controls.Disable();
        _controls.Player.Disable();
    }

}
