using Enemies.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class DarkAltar : Enemy
    {
        public GameEvent OnRecieveDamage;

        public TriggerFieldsPool triggerField;

        #region Health
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float health;

        public float MaxHealth => maxHealth;
        public override float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = Mathf.Clamp(value, 0, maxHealth);

                for (int i = 0; i < healthBarParts.Length; i++)
                {
                    healthBarParts[i].fillAmount = health / maxHealth;
                }
            }
        }
        #endregion

        [SerializeField] private List<Transform> spawnPositions = new List<Transform>();

        void Start()
        {
            BoundsInLocalSpace = GetComponent<MeshFilter>().mesh.bounds;

            spawnDirector.RebuildGraph();
            destroyDirector.RebuildGraph();

            foreach (KeyValuePair<string, EnemyState> state in states)
            {
                state.Value.Init(this);
            }


        }

        public void ChangeState(string stateID)
        {
            if (states.TryGetValue(stateID, out EnemyState state))
            {
                if (currentState != state)
                {
                    currentState?.Exit();
                    currentState = state;
                    currentState.Enter();
                }
            }

        }

        public override void RecieveDamage(float damageAmount)
        {
            OnRecieveDamage.Raise();

            Health -= damageAmount;
            Debug.Log(name + " current health: " + Health);
            Hit.Raise();

            if (Health <= 0)
            {
                ChangeState("Destroyed");
            }
        }

        public override void RestoreHealth(float healthAmount)
        {
            Health += healthAmount;
        }

        public void SpawnAltar()
        {
            int randomPosition = Random.Range(0, spawnPositions.Count);

            m_Transform.position = spawnPositions[randomPosition].position;

            var field = triggerField.GetObjectFromPool();
            field.Pool = triggerField;
            field.Activate();
            field.transform.position = m_Transform.position;
            
            //Rune rune;
            //Vector3 prevPosition = Vector3.zero;

            //for (int i = 0; i < numberOfPlatforms; i++)
            //{
            //    rune = runesPool.GetObjectFromPool();

            //    Vector3 horizontal = Vector3.zero;
            //    horizontal.x = Random.Range(-10, 10);
            //    horizontal.z = Random.Range(-10, 10);

            //    Vector3 position = m_Transform.position + Vector3.up * (prevPosition.y + 3f);
            //    position = new Vector3(m_Transform.position.x + horizontal.x, position.y, m_Transform.position.z + horizontal.z);

            //    rune.transform.position = position;

            //    prevPosition = position;

            //    rune.Activate();
            //    activePlatforms.Add(rune);
            //}

            spawnDirector.Play();
            Health = MaxHealth;

            ChangeState("Alive");
        }


    }
}

