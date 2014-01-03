using UnityEngine;
using System.Collections;
using vida;

namespace bala{

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Vida))]
public class logicaBala : MonoBehaviour {


	public int daño=10;
	[HideInInspector]
	public float velocidad = 0;
	public float tiempoDeVida=2f;
	
	protected Vida instVida;

	// Use this for initialization
	void Start () {
		instVida=GetComponent<Vida>();
		morir(tiempoDeVida);
	}
	
	// Update is called once per frame
	void Update () {

	}

	protected void OnCollisionEnter2D(Collision2D coll){
			if (coll.gameObject==null){return;}
		if (coll.gameObject.CompareTag ("enemigo")) {
			(coll.gameObject.GetComponent<Vida>()).aplicarDaño(daño);
		}
		if (!coll.gameObject.CompareTag("Player")){
			instVida.motivoAlMorir= motivoDeMuerte.SinVida;
			morir (0);
		}
	}

	protected void morir(float tiempo){
		Destroy (this.gameObject,tiempo);
	}

	public void iniciar(Vector2 direccion,float velocidad){
		this.velocidad=velocidad;
		rigidbody2D.velocity = velocidad*direccion;

	}
}
}