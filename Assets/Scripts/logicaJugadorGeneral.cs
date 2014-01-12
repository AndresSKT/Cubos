using UnityEngine;
using System.Collections;

public class logicaJugadorGeneral : logicaJugador {

	public GameObject logicaDelNivel;


	nivel logica;

	// Use this for initialization
	void Start () {
		if (logicaDelNivel!=null){
		logica = logicaDelNivel.GetComponent<nivel> ();
		}
	}


	// Update is called once per frame
	void Update () {
	
	}

	public override void morir(){
		logica.jugadorMurio ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("moneda")) {
			item itemtmp =coll.gameObject.GetComponent<item>();
			sumarPuntaje(itemtmp.puntos);
			itemtmp.isPicked=true;
			Destroy(coll.gameObject);
		}
	}

	void sumarPuntaje(int puntaje){
		logica.anadirPuntos(puntaje);
	}
}
