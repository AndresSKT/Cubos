using UnityEngine;
using System.Collections;

public class checkIsGrounded : MonoBehaviour
{

	public string[] CapasDelSuelo = {"suelo"};
	Vector3 upward = Vector3.up;
	controlador_jugador controlador;

	void Start ()
	{
		controlador = gameObject.GetComponent<controlador_jugador> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		bool tocando = false;
		upward = Vector3.up;
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject != null) {
			foreach (string capa in CapasDelSuelo) {
				if (capa.Equals (coll.gameObject.tag)) {
					foreach (ContactPoint2D p in coll.contacts) {
						if (p.normal.y > 0.5) {
							upward = p.normal;
							controlador.pisandoElSuelo = true;
							controlador.upVector = p.normal;
							break;
						}
					}
					break;
				}
			}
		}

	}

	void OnCollisionStay2D (Collision2D coll)
	{
		if (coll.gameObject != null) {
			foreach(string capa in CapasDelSuelo){
				if (capa.Equals(coll.gameObject.tag)){
				foreach (ContactPoint2D p in coll.contacts) {
				if (p.normal.y > 0.5) {
					upward = p.normal;
					controlador.pisandoElSuelo = true;
					controlador.upVector = p.normal;
					break;
				}
					}
					break;
				}
			}
		}
		
	}

	void OnCollisionExit2D (Collision2D coll)
	{
		if (coll.gameObject != null ) {
			foreach(string capa in CapasDelSuelo){
				if (capa.Equals(coll.gameObject.tag)){
				upward = Vector3.up;
				controlador.pisandoElSuelo = false;
				controlador.upVector = upward;
					break;
				}
			}
		}
	}

}