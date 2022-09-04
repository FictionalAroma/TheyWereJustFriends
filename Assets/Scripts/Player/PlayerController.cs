using System;
using System.Collections;
using Assets.Scripts.Animation;
using Assets.Scripts.Behaviors;
using Enemy;
using Environment;
using RainbowJam.Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour, PlayerInputControls.IPlayerActions, IRespawnClient
    {
        #region Consts
        private static readonly int PlayerMoveY = Animator.StringToHash("PlayerMoveY");
        private static readonly int PlayerMoveX = Animator.StringToHash("PlayerMoveX");
        private static readonly int Attacking = Animator.StringToHash("Attacking");


        #endregion

        #region UnityFields

        [SerializeField] private float movementFactor;
        [SerializeField] private float jumpForce;

        #endregion


        #region Unity Cache

        private PlayerInputControls _controls;
        private Animator _animator;
        private Rigidbody2D _playerBody;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;


        #endregion


        private Vector2 _currentMoveInputVector;
        private bool _canAttack = true;

        public bool InRespawnProcess { get; set; }
        public bool CanActivateSpawner { get; set; } = true;
        [field:SerializeField] public float RespawnTimer { get; set; } = 1f;

        private float weaponAnimTime = 0.5f;


        private void Awake()
        {
            CacheControls();
            _playerBody = GetComponent<Rigidbody2D>();

            SetControlCallbacks();

            ConfigureAnimator();
        }
        private void CacheControls()
        {
            _controls = new PlayerInputControls();
            _moveAction = _controls.Player.Move;
            _jumpAction = _controls.Player.Jump;
            _attackAction = _controls.Player.Fire;
        }

        private void ConfigureAnimator()
        {
            _animator = GetComponent<Animator>();

            _animator.keepAnimatorControllerStateOnDisable = true;
            var attackEndBehavior = _animator.GetBehaviour<AttackEndBehavior>();
            attackEndBehavior.ValueHash = Attacking;
            attackEndBehavior.OnComplete += OnAttackCompleted;
        }
        private void SetControlCallbacks()
        {
            _moveAction.started += OnMove;
            _moveAction.canceled += OnMove;

            _jumpAction.canceled += OnJump;
            //_jumpAction.performed += OnJump;
            _attackAction.performed += OnFire;
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

            var velocity = _playerBody.velocity;
            var velTemp = velocity;

            velTemp.x = movementVectorX;
            _playerBody.velocity += (velTemp - velocity);

            _animator.SetFloat(PlayerMoveX, playerXMovementAbs);
            _animator.SetFloat(PlayerMoveY, _playerBody.velocity.y);

            if (playerXMovementAbs > Mathf.Epsilon)
            {
                transform.localScale = new Vector2(Mathf.Sign(movementVectorX), 1f);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _currentMoveInputVector = !context.canceled ? context.ReadValue<Vector2>() : Vector2.zero;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, jumpForce);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (_canAttack)
            {
                _animator.SetBool(Attacking, true);
                _canAttack = false;
                StartCoroutine(AttackTimer(weaponAnimTime));
            }
        }

        private IEnumerator AttackTimer(float f)
        {
            yield return new WaitForSeconds(f);
            _canAttack = true;

        }

        private void OnAttackCompleted(object sender, AnimatorStateEventArgs args)
        {

        }


        private void OnDestroy()
        {
            UnhookAnimator();
        }
        private void OnDisable()
        {
            DisableControls();
        }

        private void DisableControls()
        {
            _controls.Disable();
            _controls.Player.Disable();
        }


        private void UnhookAnimator()
        {
            var attackEndBehavior = _animator.GetBehaviour<AttackEndBehavior>();
            attackEndBehavior.OnComplete -= OnAttackCompleted;
        }


        public void RespawnActivated(RespawnPoint currentPoint)
        {
            StartCoroutine(DoRespawn(currentPoint));
        }

        private IEnumerator DoRespawn(RespawnPoint currentPoint)
        {
            _animator.GetBool("Respawning");
            DisableControls();
            _playerBody.Sleep();
            transform.position = currentPoint.transform.position;
            yield return new WaitForSeconds(RespawnTimer);

            //yield return new WaitUntil(() => !_animator.GetBool("Respawning"));

            EnableControls();
            _playerBody.WakeUp();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {

            if (col.gameObject.TryGetComponent<EnemyController>(out var enemy))
            {
                TakeDamage(col.GetContact(0), enemy);
            }
        }

        private void TakeDamage(ContactPoint2D getContact, EnemyController enemyController)
        {
            
        }
    }
}
