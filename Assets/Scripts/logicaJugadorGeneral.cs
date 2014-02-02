using UnityEngine;
using System.Collections;
using vida;

[RequireComponent(typeof(vida.Vida),typeof(mana))]
public class logicaJugadorGeneral : logicaJugador {

	public GameObject logicaDelNivel;


	nivel logica;
	Vida intVida;
	mana instMana;

	// Use this for initialization
	void Start () {
		if (logicaDelNivel!=null){
		logica = logicaDelNivel.GetComponent<nivel> ();
		}
		intVida = this.GetComponent<Vida>();
		instMana = this.GetComponent<mana>();
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
		}else if (coll.gameObject.CompareTag("item_vida")){
			item itemtmp =coll.gameObject.GetComponent<item>();
			anadirVida(itemtmp.puntos);
			itemtmp.isPicked=true;
			Destroy(coll.gameObject);
		}else if (coll.gameObject.CompareTag("item_mana")){
			item itemtmp =coll.gameObject.GetComponent<item>();
			anadirMana(itemtmp.puntos);
			itemtmp.isPicked=true;
			Destroy(coll.gameObject);
		}
	}

	void sumarPuntaje(int puntaje){
		if (logica!=null){
			logica.anadirPuntos(puntaje);
		}
	}

	void anadirVida(int vida){
		intVida.anadirVida(vida);
	}

	void anadirMana(int Mana){
		instMana.AnadirMana(Mana);
	}
}
