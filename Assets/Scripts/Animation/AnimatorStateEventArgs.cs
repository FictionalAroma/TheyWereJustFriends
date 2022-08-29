using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Animation
{
    public class AnimatorStateEventArgs : EventArgs
    {
        public AnimatorStateEventArgs(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Animator = animator;
            StateInfo = stateInfo;
            LayerIndex = layerIndex;
        }
        public Animator Animator { get; }
        public AnimatorStateInfo StateInfo { get; }
        public int LayerIndex { get; }
    }
}
