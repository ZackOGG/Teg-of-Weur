using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ToW.Character;

namespace ToW.Systems
{
    public class Selection_Controller : MonoBehaviour
    {
        [SerializeField] GameObject selected;
        private Character_Stats selectedStats;
        private PAC_Movement_Controller selectedMove;
        private PAC_Attack_Controller selectedAttackCon;
        [SerializeField] float rDistance;
        private EventSystem eventSystem;

        //=====Raycasting=====
        private Ray mouseRay;
        //====================

        // Use this for initialization
        void Start()
        {
            SetRefrences();
        }

        private void SetRefrences()
        {
            eventSystem = this.GetComponent<EventSystem>();
            
        }

        // Update is called once per frame
        void Update()
        {
            
            if(Input.GetMouseButtonDown(0))
            {   if(ShootRayHitCol().tag == "Character" || ShootRayHitCol().tag == "Building")
                {
                    Select(ShootRayHitCol().gameObject);
                }
                
            }
            if(Input.GetMouseButtonDown(1))
            {
                if (selected)
                {
                    if (ShootRayHitCol().tag == "Ground" && selected.GetComponent<PAC_Movement_Controller>().GetMoveable())
                    {
                        selectedMove.SetCharacterTarget(null);
                        Vector3 mousePoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                        selectedMove.SetVectorTarget(mousePoint);
                    }
                    else
                    {
                        Debug.LogWarning("Selected unit is not movable");
                    }
                    if (ShootRayHitCol().tag == "Character")
                    {
                        Character_Stats rightClickCharacterStats = ShootRayHitCol().GetComponent<Character_Stats>();
                        if (rightClickCharacterStats.GetPlayerNum() != selectedStats.GetPlayerNum())
                        {
                            print("Attack");
                            selectedAttackCon.SetAttackTarget(ShootRayHitCol().gameObject);
                            selectedMove.SetCharacterTarget(ShootRayHitCol().gameObject);
                        }
                        else
                        {
                            selectedMove.SetCharacterTarget(ShootRayHitCol().gameObject);
                        }
                    }

                }
                else { Debug.LogWarning("No unit selected"); }
            }
        }
        private Collider ShootRayHitCol()
        {
            RaycastHit hit;
            mouseRay = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, rDistance);
            return hit.collider;
        }

        private void Select(GameObject gameObj)
        {
            selected = gameObj;
            selectedStats = gameObj.GetComponent<Character_Stats>();
            selectedMove = gameObj.GetComponent<PAC_Movement_Controller>();
            selectedAttackCon = gameObj.GetComponent<PAC_Attack_Controller>();
        }
        
    }
}