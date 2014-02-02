using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Logica.otros;


namespace Logica.Objetivos {
public class llegarAUnArea : Objetivo  {

		targetArea[] areas;

	// Use this for initialization
	void Start () {
			areas = FindObjectsOfType<targetArea>();
			foreach(targetArea area in this.areas){
				if (area!=null){
					area.TriggerEnter2D+=RemoteOnTriggerEnter2D;
				}
			}
	}

	void RemoteOnTriggerEnter2D(Collider2D coll){
			if (coll.gameObject.CompareTag("Player")){
				base.OnHaCumplidoElObjetivo(this);
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		#region implemented abstract members of Objetivo
		public override bool ObjetivoCumplido ()
		{
			return false;
		}
		#endregion
}
}