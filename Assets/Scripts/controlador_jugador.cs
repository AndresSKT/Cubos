using UnityEngine;
using System.Collections;
using ataque;
using System;

public class controlador_jugador : MonoBehaviour {

	public float VelocidadMaxima=10f;
	public float velocidadSalto = 20f;


	AtaqueBase ataque1;
	AtaqueBase ataque2;
    

	InputWrapper entradaAlternativa;


	[HideInInspector]
	private bool _pisandoElSuelo=false;
	public bool pisandoElSuelo{
		get { return _pisandoElSuelo;}
		set {
			_pisandoElSuelo=value; 
			if (_pisandoElSuelo && (Time.time-LastJumpTime)>=secureJumpTimeInFixedIntervals){

				numeroDeSaltosPosibles=numeroDeSaltosMaximos;
				isJumping=false;
			}
		
		}
	}
	[HideInInspector]
	public Vector3 upVector=Vector3.up;
	private int numeroDeSaltosMaximos=2;
	private int numeroDeSaltosPosibles=2;
	private bool isJumping=true;
	private float secureJumpTimeInFixedIntervals=2;
	Animator animacion;
	bool mirandoALaDerecha=true;
	private float LastJumpTime;

	// Use this for initialization
	void Start () {
		animacion = this.GetComponent<Animator> ();
		entradaAlternativa = GetComponent<InputWrapper> ();

		AtaqueBase[] scripts = GetComponents<AtaqueBase>();
		Array.Sort(scripts);
		ataque1=scripts[0];
		ataque2=scripts[1];

		secureJumpTimeInFixedIntervals*=Time.fixedDeltaTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (estaEnElAire () && rigidbody2D.velocity.x>(VelocidadMaxima/2)) {
			rigidbody2D.velocity = new Vector2 (VelocidadMaxima/2, rigidbody2D.velocity.y);

		}
		
		animacion.SetBool("EnElAire",estaEnElAire());
		animacion.SetFloat("velocidadY",rigidbody2D.velocity.y);
		}

	void Update(){
		entrada ();

	}

	void entrada(){

		float velRel = 0;
		if (entradaAlternativa!=null){
			velRel= entradaAlternativa.Horizontal;
		}
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
		} 


		if (Input.GetAxis ("disparar1") > 0 || (entradaAlternativa!=null && entradaAlternativa.getDisparar(0))) {
			ataque1.Disparar();
		}else if (Input.GetAxis ("disparar2") > 0 || (entradaAlternativa!=null && entradaAlternativa.getDisparar(1))){
			ataque2.Disparar();
		}


		bool jump =Input.GetKeyDown(KeyCode.UpArrow);

		if ((jump  || (entradaAlternativa!=null && entradaAlternativa.Jump>0)) && numeroDeSaltosPosibles>0) {
			numeroDeSaltosPosibles--;
			Saltar();

		}


		Vector3 direccion=Vector3.Cross(upVector,Vector3.forward);
		Vector3 velTmp = rigidbody2D.velocity;

		if (isJumping){
			velTmp.y=rigidbody2D.velocity.y;
		}else if (!estaEnElAire()){
			velTmp.y=direccion.y*velRel*VelocidadMaxima;
		}
		velTmp.x=VelocidadMaxima*direccion.x*velRel;
		if (isJumping){
			velTmp.x/=2.5f;
		}

		rigidbody2D.velocity = velTmp;
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
		
		mirandoALaDerecha=!mirandoALaDerecha;
	}

		void Saltar(){

		float velY = rigidbody2D.velocity.x;
		rigidbody2D.velocity = new Vector2 (velY, velocidadSalto);
		isJumping=true;
		LastJumpTime=Time.time;

		}
	

	public bool estaEnElAire(){
		return !pisandoElSuelo;
	}

}
