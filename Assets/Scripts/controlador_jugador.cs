using UnityEngine;
using System.Collections;


[RequireComponent(typeof(InputWrapper))]
public class controlador_jugador : MonoBehaviour {

	public float VelocidadMaxima=10f;
	public float velocidadSalto = 20f;
	public GameObject objetoParaComprobarSuelo;
	public GameObject puntoDeDisparo;
	public GameObject[] disparos;


	public float tiempoEntreBalas = 0.5f;
	
	float ultimaBala=0;



	InputWrapper entradaAlternativa;


	[HideInInspector]
	public bool pisandoElSuelo=false;
	
	Animator animacion;
	bool mirandoALaDerecha=true;


	// Use this for initialization
	void Start () {
		animacion = this.GetComponent<Animator> ();
		entradaAlternativa = GetComponent<InputWrapper> ();
		}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (estaEnElAire () && rigidbody2D.velocity.x>(VelocidadMaxima/2)) {
			rigidbody2D.velocity = new Vector2 (VelocidadMaxima/2, rigidbody2D.velocity.y);

		}

		entrada ();
		animacion.SetBool("EnElAire",estaEnElAire());
		animacion.SetFloat("velocidadY",rigidbody2D.velocity.y);

		
	}

	void entrada(){

		float velRel = entradaAlternativa.Horizontal;
		if (velRel == 0) {
			velRel=Input.GetAxis ("Horizontal");		
		}
		if (velRel > 0) {
			velRel = 1;		
		} else if (velRel < 0) {
			velRel=-1;		
		}
		if (mirandoALaDerecha && velRel < 0) {
			flip ();
			mirandoALaDerecha=false;
		} else if (!mirandoALaDerecha && velRel > 0) {
			flip();		
			mirandoALaDerecha=true;
		}


		//disparos
		if (Input.GetAxis ("disparar") > 0) {
			disparar();		
		}

		if (estaEnElAire()) {
			return;		
		}

		float jump = Input.GetAxis ("Jump");
		if (jump > 0 || entradaAlternativa.Jump>0) {
			Saltar();
		}

		rigidbody2D.velocity = new Vector2 (VelocidadMaxima*velRel, rigidbody2D.velocity.y);
		animacion.SetFloat ("velocidad", Mathf.Abs(VelocidadMaxima*velRel));


	}


	void flip(){
		transform.localScale = new Vector3 (transform.localScale.x*-1,transform.localScale.y, transform.localScale.z);
	}

		void Saltar(){

		float velY = rigidbody2D.velocity.x;
		rigidbody2D.velocity = new Vector2 (velY, velocidadSalto);


		}
	

	public bool estaEnElAire(){
		return !pisandoElSuelo;
	}

	public void disparar(){
		if (Time.time - ultimaBala < tiempoEntreBalas) {
			return;
		}
		ultimaBala = Time.time;

		GameObject bala = Instantiate (disparos [0]) as GameObject;
		//bala.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1f);
		bala.transform.position = puntoDeDisparo.transform.position;
		(bala.GetComponent<logicaBala> ()).Direccion = (mirandoALaDerecha) ? Vector2.right : Vector2.right * -1;
		
	}
}
