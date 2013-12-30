using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class logicaBala : MonoBehaviour {


	public int daño=10;
	public float velocidad = 0;
	public float tiempoDeVida=2f;


	public Vector2 Direccion=Vector2.right;


	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = velocidad*Direccion;
		morir(tiempoDeVida);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("enemigo")) {
			(coll.gameObject.GetComponent<Vida>()).aplicarDaño(daño);
		}
		morir (0);
	}

	void morir(float tiempo){
		Destroy (this.gameObject,tiempo);
	}
}
