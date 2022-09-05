using System;
using UnityEngine;

namespace Animation
{
    public interface IAnimationComplete
    {
        public string ValueName
        {
            set => ValueHash = Animator.StringToHash(value);
        }

        public int ValueHash { get; set; }
        public EventHandler<AnimatorStateEventArgs> OnComplete { get; set; }

    }
}