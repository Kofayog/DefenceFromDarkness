using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemies.States
{
    [CreateAssetMenu(menuName = "TargetState/Respawn")]
    public class Respawn : EnemyState
    {
        [SerializeField] private float respawnTime;
        [SerializeField] private float uiFadeDuration;

        private WaitForSeconds waitRespawnCount;

        public override void Init(Enemy enemy)
        {
            base.Init(enemy);
            waitRespawnCount = new WaitForSeconds(1);
        }
        public override void Action()
        {
           
        }

        public override void Enter()
        {
            // Enable respawn UI

            enemy.respawnInfo.DOFade(1, uiFadeDuration);
            enemy.StartCoroutine(SpawnDelay(respawnTime));
        }

        public override void Exit()
        {
            Debug.Log("Exit Respawn");
            enemy.DOPause();
            
            
        }

        private IEnumerator SpawnDelay(float respawnTime)
        {
            float timer = respawnTime;

            while (timer > 0)
            {
                timer--;
                enemy.respawnCountText.text = timer.ToString();

                yield return waitRespawnCount;
            }

            enemy.respawnInfo.DOFade(0, uiFadeDuration);
            //enemy.Spawn();
            enemy.Spawn.Raise();
        }
    }
}

