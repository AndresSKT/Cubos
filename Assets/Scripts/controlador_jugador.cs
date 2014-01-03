using UnityEngine;
using System.Collections;
using ataque;
using System;

[RequireComponent(typeof(InputWrapper))]
public class controlador_jugador : MonoBehaviour {

	public float VelocidadMaxima=10f;
	public float velocidadSalto = 20f;
	public GameObject objetoParaComprobarSuelo;


	AtaqueBase ataque1;
	AtaqueBase ataque2;


	InputWrapper entradaAlternativa;


	[HideInInspector]
	public bool pisandoElSuelo=false;
	[HideInInspector]
	public Vector3 upVector=Vector3.up;

	Animator animacion;
	bool mirandoALaDerecha=true;


	// Use this for initialization
	void Start () {
		animacion = this.GetComponent<Animator> ();
		entradaAlternativa = GetComponent<InputWrapper> ();

		AtaqueBase[] scripts = GetComponents<AtaqueBase>();
		Array.Sort(scripts);
		ataque1=scripts[0];
		ataque2=scripts[1];
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
		bool mustFlip = (mirandoALaDerecha && velRel < 0) || (!mirandoALaDerecha && velRel > 0);
		if (mustFlip){
			flip ();
			mirandoALaDerecha=!mirandoALaDerecha;
		} 


		if (Input.GetAxis ("disparar1") > 0 || entradaAlternativa.getDisparar(0)) {
			ataque1.Disparar();
		}else if (Input.GetAxis ("disparar2") > 0 || entradaAlternativa.getDisparar(1)){
			ataque2.Disparar();
		}


		if (estaEnElAire()) {
			return;		
		}
		bool vaASaltar=false;
		float jump = Input.GetAxis ("Jump");
		if (jump > 0 || entradaAlternativa.Jump>0) {
			Saltar();
			vaASaltar=true;
		}

		Vector3 direccion=Vector3.Cross(upVector,Vector3.forward);

		float velY=0;
		if (vaASaltar){
			velY=rigidbody2D.velocity.y;
		}else{
			velY=direccion.y*velRel*VelocidadMaxima;
		}
		rigidbody2D.velocity = new Vector2 (VelocidadMaxima*direccion.x*velRel,velY);
		animacion.SetFloat ("velocidad", Mathf.Abs(VelocidadMaxima*velRel));


	}


	void flip(){
		transform.localScale = new Vector3 (transform.localScale.x*-1,transform.localScale.y, transform.localScale.z);
		int dir=1;
		if (transform.localScale.x<0){
			dir=-1;
		}
		ataque1.direccion=Vector2.right*dir;
		ataque2.direccion=Vector2.right*dir;
	}

		void Saltar(){

		float velY = rigidbody2D.velocity.x;
		rigidbody2D.velocity = new Vector2 (velY, velocidadSalto);


		}
	

	public bool estaEnElAire(){
		return !pisandoElSuelo;
	}

}
