using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DG.Tweening;

namespace Enemies.States
{
    [CreateAssetMenu(menuName = "TargetState/Destoyed")]
    public class Destroyed : EnemyState
    {
        [SerializeField] private float uiFadeDuration = 1f;

        public override void Action()
        {
            
        }

        public override void Enter()
        {
            // Disable UI
            // Activate destroying effects
            //enemy.OnDestroyed();
            enemy.Destroyed.Raise();
            enemy.destroyDirector.Play();
            enemy.altarInfo.DOFade(0, uiFadeDuration);
        }

        public override void Exit()
        {
            Debug.Log("Destroyed");
        }
    }
}

