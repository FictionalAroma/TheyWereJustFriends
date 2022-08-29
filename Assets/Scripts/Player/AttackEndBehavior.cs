using System;
using Animation;
using Assets.Scripts.Animation;
using UnityEngine;

namespace Player
{
    public class AttackEndBehavior : StateMachineBehaviour, IAnimationComplete
    {
        public int ValueHash { get; set; }
        public EventHandler<AnimatorStateEventArgs> OnComplete { get; set; }

        private void Awake()
        {
            OnComplete += TurnOffValue;
        }

        private void OnDisable()
        {
            OnComplete -= TurnOffValue;
        }

        private void TurnOffValue(object _, AnimatorStateEventArgs args)
        {
            args.Animator.SetBool(ValueHash, false);
        }

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnComplete.Invoke(this, new AnimatorStateEventArgs(animator, stateInfo, layerIndex));
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
