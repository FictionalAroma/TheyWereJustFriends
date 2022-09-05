using System;
using JetBrains.Annotations;
using UnityEditor.Build;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Animator))]
    [DisallowMultipleComponent]
    public class RespawnPoint : MonoBehaviour
    {
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inActiveSprite;

        

        private bool _active;
        private static readonly int Active = Animator.StringToHash("Active");

        private SpriteRenderer _renderer;
        private Animator _animator;
        private Collider2D _collider;

        private void Awake()
        {

            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            SetActive(false);
        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    //just sanity check our collision
        //    if (other.GetComponent<RespawnBehavior>() != null)
        //    {
        //        SetActive(true);
        //    }
        //}

        public void SetActive(bool active)
        {
            _active = active;
            _renderer.sprite = _active ? activeSprite : inActiveSprite;
            _animator.SetBool(Active, _active);
            _collider.enabled = !_active;
        }
    }
}