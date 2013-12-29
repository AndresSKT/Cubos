using UnityEngine;
using System.Collections;

public class logicaJugadorGeneral : logicaJugador {

	public GameObject logicaDelNivel;


	int levelScore=0;
	nivel logica;

	// Use this for initialization
	void Start () {
		logica = logicaDelNivel.GetComponent<nivel> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void morir(){
		logica.jugadorMurio ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("moneda")) {
			sumarPuntaje(coll.gameObject.GetComponent<item>().puntos);
			Destroy(coll.gameObject);
		}
	}

	void sumarPuntaje(int puntaje){
		levelScore += puntaje;
		logica.Puntaje = levelScore;
	}
}
