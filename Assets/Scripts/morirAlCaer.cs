using UnityEngine;
using System.Collections;

public class morirAlCaer : MonoBehaviour {

	public int finDelMundo=-100;
	public GameObject objPos;
	public float tiempoParaVolver=3f;

	float tiempoCayendo=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit2D res = Physics2D.Raycast(objPos.transform.position, Vector2.up*-1);			
		if (res.transform == null) {
			tiempoCayendo += Time.deltaTime;
				} else if (res.transform != null) {
			tiempoCayendo=0;		
		}

		if (tiempoCayendo >= tiempoParaVolver) {
			morir();		
		}

	}

	void morir(){
		Destroy(this.gameObject);
	}
}
