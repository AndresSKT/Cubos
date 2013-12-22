using UnityEngine;
using System.Collections;

public class controlador_jugador : MonoBehaviour {

	public float VelocidadMaxima=10f;
	public Vector2 fuerzaSalto = new Vector2 (0,200);
	public GameObject objetoParaComprobarSuelo;

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
	void FixedUpdate () {
		RaycastHit2D colisionSuelo = Physics2D.Linecast (transform.position, objetoParaComprobarSuelo.transform.position,1<<8);
		pisandoElSuelo = (colisionSuelo.transform != null);

		entrada ();
		animacion.SetBool("EnElAire",estaEnElAire());
		animacion.SetFloat("velocidadY",fisicas.velocity.y);

		
	}

	void entrada(){
		if (estaEnElAire()) {
			return;		
		}

		float jump = Input.GetAxis ("Jump");
		if (jump > 0) {
			fisicas.AddForce(fuerzaSalto);
			fisicas.AddForce(new Vector2(-1*fisicas.velocity.x,0));
		}

		float velRel = Input.GetAxis ("Horizontal");
		fisicas.velocity = new Vector2 (VelocidadMaxima * velRel, fisicas.velocity.y);
		animacion.SetFloat ("velocidad", Mathf.Abs(VelocidadMaxima*velRel));
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
	

	bool estaEnElAire(){
		return !pisandoElSuelo;
	}
}
