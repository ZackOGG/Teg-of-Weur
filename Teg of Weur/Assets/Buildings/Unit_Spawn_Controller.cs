using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToW.Character;
using ToW.Core;

namespace ToW.Buildings
{
    public class Unit_Spawn_Controller : MonoBehaviour
    {
        [SerializeField] GameObject unitObject;
        [SerializeField] float spawnRate;
        [SerializeField] Vector3 spawnLocation;
        private float spawnRateTimer;

        public GameObject enemyKeep;
        public PlayerNum playerNum;
        public PlayerNum GetPlayerNum() { return playerNum; }
        public void SetPlayerNum(PlayerNum newPlayerNum) { playerNum = newPlayerNum; }

        // Use this for initialization
        void Start()
        {
            playerNum = this.GetComponent<Character_Stats>().GetPlayerNum();
            enemyKeep = FindKeep(playerNum);
            InvokeRepeating("SpawnUnit", spawnRate, spawnRate);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SpawnUnit()
        {
            GameObject theUnit = Instantiate(unitObject, this.transform).gameObject;
            theUnit.transform.position = (this.transform.position + spawnLocation);
            //Set move Location
            theUnit.GetComponent<PAC_Movement_Controller>().SetCharacterTarget(enemyKeep);
            theUnit.GetComponent<Character_Stats>().SetPlayerNum(playerNum);
        }
        private GameObject FindKeep(PlayerNum thePlayerNumber) // ONLY WORKS WITH TWO PLAYERS
        {

            Keep[] keeps = GameObject.FindObjectsOfType<Keep>();
            foreach(Keep theKeep in keeps)
            {
                print("Hello");
                if (theKeep.GetComponent<Character_Stats>().GetPlayerNum() != playerNum)
                {
                    return theKeep.gameObject;
                }
            }
            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere((this.transform.position + spawnLocation), 0.2f);
        }
    }
}