using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adkasodka
namespace ToW.Character
{
    public class PAC_Attack_Controller : MonoBehaviour
    {
        [SerializeField] float attackRange;
        [SerializeField] float agroRange;

        public GameObject attackTarget;
        private Character_Stats enemyStats;
        private Character_Stats characterStats;
        private float attackSpeed;
        private float attackTimer;
        private PAC_Movement_Controller characterMove;

        private int damage;

        // Use this for initialization
        void Start()
        {
            SetRefrences();
            SetStats();
        }
        private void SetRefrences()
        {
            characterStats = this.GetComponent<Character_Stats>();
            characterMove = this.GetComponent<PAC_Movement_Controller>();
        }
        private void SetStats()
        {
            damage = characterStats.CalculateDamage();
            attackSpeed = characterStats.GetAttackSpeed();
            attackTimer = attackSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if(attackTarget)
            {
                
                if (GetDistanceFromTarget() <= attackRange)
                {
                    Attack();
                }
                else
                {
                    attackTimer = attackSpeed; // This is to create a wind up for each attack with the potential of kiting 
                }
                
                
            }
            if(attackTarget == null)
            {
                
                DetectEnemy();
            }
        }

        private void DetectEnemy()
        {
            Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(this.transform.position, agroRange);
            foreach (Collider hit in hitColliders)
            {
                if (hit.tag == "Character" || hit.tag == "Building")
                {
                    if (hit.GetComponent<Character_Stats>().GetPlayerNum() != characterStats.GetPlayerNum())
                    {
                        print(hit.gameObject);
                        SetAttackTarget(hit.gameObject);
                        characterMove.SetCharacterTarget(hit.gameObject);
                    }
                }
            }
        }

        public void SetAttackTarget(GameObject gameObj)
        {
            attackTarget = gameObj;
            enemyStats = gameObj.GetComponent<Character_Stats>();
        }

        private Vector3 GetDirectionFromTarget()
        {            
            var Direction = attackTarget.transform.position - this.transform.position;
            
           
            return Direction.normalized;
        }
        private float GetDistanceFromTarget()
        {
            float Distance;            
            Distance = Vector3.Distance(attackTarget.transform.position, this.transform.position);
             
            return Distance;
        }

        private void Attack()
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                enemyStats.TakeDamage(damage);
                attackTimer = attackSpeed;
            }
        }

        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, agroRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, attackRange);
        }
    }
}