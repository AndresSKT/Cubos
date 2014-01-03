using UnityEngine;
using System.Collections;

public class checkIsGrounded : MonoBehaviour {

	public string CapaDelSuelo="suelo";

	Vector3 upward=Vector3.up;
	controlador_jugador controlador;


		int capa;
	
	void Start () {
		controlador = gameObject.GetComponent<controlador_jugador> ();
		capa=1<<LayerMask.NameToLayer (CapaDelSuelo);
		}
	
	// Update is called once per frame
	void FixedUpdate () {
			bool tocando = false;
			upward=Vector3.up;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject!=null && coll.gameObject.CompareTag(CapaDelSuelo)){
			foreach(ContactPoint2D p in coll.contacts){
				if (p.normal.y>0.5){
					upward=p.normal;
					controlador.pisandoElSuelo=true;
					controlador.upVector=p.normal;
					break;
				}
			}
		}

	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject!=null && coll.gameObject.CompareTag(CapaDelSuelo)){
			foreach(ContactPoint2D p in coll.contacts){
				if (p.normal.y>0.5){
					upward=p.normal;
					controlador.pisandoElSuelo=true;
					controlador.upVector=p.normal;
					break;
				}
			}
		}
		
	}

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject!=null && coll.gameObject.CompareTag(CapaDelSuelo)){
			upward=Vector3.up;
			controlador.pisandoElSuelo=false;
			controlador.upVector=upward;
		}
	}

}