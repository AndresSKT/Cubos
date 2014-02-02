using UnityEngine;
using System.Collections;

[RequireComponent(typeof(controlador_jugador))]
public class checkIsGrounded : MonoBehaviour
{

	public string[] CapasDelSuelo = {"suelo"};
	Vector3 upward = Vector3.up;
	controlador_jugador controlador;
	private float lastCollisionTime=0;

	public int finDelMundo=-100;
	public GameObject objPos;
	public float tiempoParaVolver=3f;
	float tiempoCayendo=0;

	[HideInInspector]
	public bool isReallyGrounded=true;

	void Start ()
	{
		controlador = gameObject.GetComponent<controlador_jugador> ();
		lastCollisionTime=Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		CheckisReallyGrounded();
		if (!isReallyGrounded){
			controlador.pisandoElSuelo=false;
		}
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
							lastCollisionTime=Time.time;
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
					lastCollisionTime=Time.time;
					break;
				}
					}
					break;
				}
			}
		}
		
	}

	void CheckisReallyGrounded(){
		RaycastHit2D res = Physics2D.Raycast(objPos.transform.position, Vector2.up*-1);			
		if (res.transform == null && controlador.estaEnElAire()) {
			tiempoCayendo += Time.deltaTime;
		} 
		else if (res.transform != null || controlador.pisandoElSuelo) {
			tiempoCayendo=0;		
		}
		
		isReallyGrounded =!(tiempoCayendo >= tiempoParaVolver);
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