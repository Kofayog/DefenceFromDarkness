using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States
{
    [CreateAssetMenu(menuName = "TargetState/Alive")]
    public class Alive : EnemyState
    {
        [SerializeField] private float uiFadeDuration = 1f;
        public override void Action()
        {
            
        }

        public override void Enter()
        {
            // Enable UI
            enemy.altarInfo.DOFade(1, uiFadeDuration);
        }

        public override void Exit()
        {
            Debug.Log("Alive");
        }


    }
}

