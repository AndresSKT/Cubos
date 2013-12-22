using UnityEngine;
using System.Collections;

public class checkIsGrounded : MonoBehaviour {

	public GameObject[] puntos;
	public string CapaDelSuelo="suelo";
	controlador_jugador controlador;

		int capa;
	
	void Start () {
		controlador = gameObject.GetComponent<controlador_jugador> ();
		capa=1<<LayerMask.NameToLayer (CapaDelSuelo);
		}
	
	// Update is called once per frame
	void FixedUpdate () {
			bool tocando = false;
			for (int i=0; i<puntos.Length; i++) {
				if (Physics2D.Linecast(transform.position,puntos[i].transform.position,capa).transform!=null){
					tocando=true;
				}			
			}
			controlador.pisandoElSuelo = tocando;
	}

}