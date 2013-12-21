using UnityEngine;
using System.Collections;

public class controlador_jugador : MonoBehaviour {

	public float VelocidadMaxima=10f;
	Rigidbody2D fisicas;
	Animator animacion;
	bool mirandoALaDerecha=true;
	bool pisandoElSuelo=false;

	// Use this for initialization
	void Start () {
		fisicas = this.GetComponent<Rigidbody2D> ();
		animacion = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pisandoElSuelo) {
			return;		
		}

		float velRel = Input.GetAxis ("Horizontal");
		fisicas.velocity = new Vector2 (VelocidadMaxima * velRel, fisicas.velocity.y);
		animacion.SetFloat ("velocidad", Mathf.Abs(VelocidadMaxima*velRel));
		Debug.Log (Mathf.Abs (VelocidadMaxima * velRel));
		if (mirandoALaDerecha && velRel < 0) {
				flip ();
			mirandoALaDerecha=false;
				} else if (!mirandoALaDerecha && velRel > 0) {
			flip();		
			mirandoALaDerecha=true;
		}

	}

	void flip(){
		transform.localScale = new Vector3 (transform.localScale.x*-1,transform.localScale.y, transform.localScale.z);
	}

	void OnCollisionEnter2D(Collision2D coll){
		pisandoElSuelo =coll.gameObject.CompareTag ("suelo")?true:pisandoElSuelo;
	}

	void OnCollisionStay2D(Collision2D coll){
		pisandoElSuelo =coll.gameObject.CompareTag ("suelo")?true:pisandoElSuelo;
	}

	void OnCollisionExit2D(Collision2D coll){
		pisandoElSuelo =coll.gameObject.CompareTag ("suelo")?false:pisandoElSuelo;
	}
		
}
