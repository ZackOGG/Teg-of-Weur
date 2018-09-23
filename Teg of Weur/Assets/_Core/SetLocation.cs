using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

public class SetLocation : MonoBehaviour {
    //This script tells the builder where to walk to


    [SerializeField] Transform target;
    private PolyNavAgent navAgent;

	// Use this for initialization
	void Start ()
    {
        navAgent = this.GetComponent<PolyNavAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        navAgent.SetDestination(target.position);
	}
}
