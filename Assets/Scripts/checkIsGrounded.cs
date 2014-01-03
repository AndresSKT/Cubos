using UnityEngine;
using System.Collections;

public class checkIsGrounded : MonoBehaviour {

	public GameObject[] puntos;
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
			for (int i=0; i<puntos.Length; i++) {
			Transform tmp = Physics2D.Linecast(transform.position,puntos[i].transform.position,capa).transform;
				if (tmp!=null){
					upward=tmp.up;
					tocando=true;
				}			
			}
			controlador.pisandoElSuelo = tocando;
			controlador.upVector=upward;
			
	}

}