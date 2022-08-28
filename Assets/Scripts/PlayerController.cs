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
    [SerializeField] private float jumpForce;

    #endregion


    #region Unity Cache

    private PlayerInputControls _controls;
    private InputAction _move;
    private InputAction _fire;
    private Animator _animator;
    private Rigidbody2D _playerBody;

    #endregion

    private Vector2 _currentMoveInputVector;
    private InputAction _moveAction;
    private InputAction _jumpAction;


    private void Awake()
    {
        _controls = new PlayerInputControls();
        _animator = GetComponent<Animator>();
        _playerBody = GetComponent<Rigidbody2D>();

        SetControlCallbacks();

    }

    private void SetControlCallbacks()
    {
        _moveAction = _controls.Player.Move;
        _moveAction.started += OnMove;
        _moveAction.canceled += OnMove;

        _jumpAction = _controls.Player.Jump;
        _jumpAction.canceled += OnJump;
        //_jumpAction.performed += OnJump;

    }

    private void OnEnable()
    {
        EnableControls();
    }

    private void EnableControls()
    {
        _controls.Enable();
        _controls.Player.Enable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float movementVectorX = _currentMoveInputVector.x * movementFactor;

        var playerXMovementAbs = Mathf.Abs(movementVectorX);

        var velTemp = _playerBody.velocity;

        velTemp.x = movementVectorX;
        _playerBody.velocity = velTemp;

        _animator.SetFloat(PlayerMoveX, playerXMovementAbs);
        _animator.SetFloat(PlayerMoveY, _playerBody.velocity.y);

        if (playerXMovementAbs > Mathf.Epsilon)
        {
            var temp = transform.localScale;
            temp.x = movementVectorX > 0 ? 1f : -1f;
            transform.localScale = temp;
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMoveInputVector = !context.canceled ? context.ReadValue<Vector2>() : Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _playerBody.velocity += new Vector2(_playerBody.velocity.x, jumpForce);
    }

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
