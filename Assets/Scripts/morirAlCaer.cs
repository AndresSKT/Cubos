using UnityEngine;
using System.Collections;


[RequireComponent(typeof(logicaJugador),typeof(checkIsGrounded))]
public class morirAlCaer : MonoBehaviour {



	logicaJugador logica;

	controlador_jugador controlador;
	checkIsGrounded checker;


	// Use this for initialization
	void Start () {
		logica = GetComponent<logicaJugador> (); 
		checker = GetComponent<checkIsGrounded>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!checker.isReallyGrounded){

			morir();
		}
	}

	void morir(){

		Destroy(this.gameObject);
		logica.morir ();
	}
}
