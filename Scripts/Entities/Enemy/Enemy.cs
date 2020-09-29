using Enemies.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IHealth
    {
        public GameEvent Hit;
        public GameEvent Spawn;
        public GameEvent Destroyed;

        public abstract float Health { get; set; }

        #region UI 
        public CanvasGroup altarInfo;
        [SerializeField] protected Image[] healthBarParts;

        public CanvasGroup respawnInfo;
        public Text respawnCountText;
        #endregion
        #region Timeline Directors 
        //public PlayableDirector attackDirector;
        public PlayableDirector spawnDirector;
        public PlayableDirector destroyDirector;
        #endregion

        #region States
        public TargetStatesDictionary states = new TargetStatesDictionary();

        protected EnemyState currentState;
        #endregion

        [SerializeField] private Transform m_transform;
        public Transform m_Transform => m_transform;
        public Bounds BoundsInLocalSpace { get; protected set; }

        public abstract void RecieveDamage(float damageAmount);
        public abstract void RestoreHealth(float healthAmount);

    }
}

