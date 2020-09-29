using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States
{
    public abstract class EnemyState : ScriptableObject
    {
        protected Enemy enemy;

        public virtual void Init(Enemy enemy)
        {
            this.enemy = enemy;
        }
        public abstract void Enter();
        public abstract void Exit();

        public abstract void Action();

    }
}

