using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToW.Character
{
    public class PAC_Movement_Controller : MonoBehaviour {

        [SerializeField] float stopDistance = 0.5f;

        private Character_Stats characterStats;
        private float moveSpeed;
        private Rigidbody rb;
        private Vector3 vectorTarget;
        public void SetVectorTarget(Vector3 newTrans) { vectorTarget = newTrans; }
        private GameObject characterTarget;
        public void SetCharacterTarget(GameObject newObj) { characterTarget = newObj; }
        [SerializeField] bool moveable;
        public bool GetMoveable() { return moveable; }
        public void SetMoveable(bool newBool) { moveable = newBool; }
        // Use this for initialization
        void Start()
        {
            SetRefrences();
            SetStats();
        }

        private void SetRefrences()
        {
            rb = this.GetComponent<Rigidbody>();
            characterStats = this.GetComponent<Character_Stats>();
        }
        private void SetStats()
        {
            vectorTarget = this.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            moveSpeed = characterStats.GetMovementSpeed();
            if(GetDistanceFromTarget() > stopDistance && !Input.GetKey(KeyCode.Alpha1))
            {
                MoveToTarget();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

        }        

        private void MoveToTarget()
        {
            if (characterTarget)
            {
                rb.velocity = new Vector3(GetDirectionFromTarget().x * moveSpeed, GetDirectionFromTarget().y * moveSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector3(GetDirectionFromTarget().x * moveSpeed, GetDirectionFromTarget().y * moveSpeed, 0);
            }
            
        }

        private Vector3 GetDirectionFromTarget()
        {
            Vector3 Direction;
            if (characterTarget)
            {
                vectorTarget = this.transform.position;
                Direction = characterTarget.transform.position - this.transform.position;
            }
            else
            {
                Direction = vectorTarget - this.transform.position;
            }
            return Direction.normalized;
        }
        private float GetDistanceFromTarget()
        {
            float Distance;
            if (characterTarget)
            {
                Distance = Vector3.Distance(characterTarget.transform.position, this.transform.position);
                //Distance = characterTarget.transform.position - this.transform.position;
            }
            else
            {
                Distance = Vector3.Distance(vectorTarget, this.transform.position);
                //Distance = vectorTarget - this.transform.position;
            }
            //print(Distance);
            return Distance;
        }

    }
}