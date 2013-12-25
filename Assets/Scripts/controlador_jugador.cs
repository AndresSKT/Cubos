using UnityEngine;
using System.Collections;


[RequireComponent(typeof(InputWrapper))]
public class controlador_jugador : MonoBehaviour {

	public float VelocidadMaxima=10f;
	public Vector2 fuerzaSalto = new Vector2 (0,200);
	public GameObject objetoParaComprobarSuelo;

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
		if (estaEnElAire()) {
			return;		
		}

		float jump = Input.GetAxis ("Jump");
		if (jump > 0 || entradaAlternativa.Jump>0) {
				Saltar();
		}

		float velRel = entradaAlternativa.Horizontal;
		if (velRel == 0) {
			velRel=Input.GetAxis ("Horizontal");		
		}
		rigidbody2D.velocity = new Vector2 (VelocidadMaxima * velRel, rigidbody2D.velocity.y);
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

		void Saltar(){
			rigidbody2D.AddForce(fuerzaSalto);

		}
	

	public bool estaEnElAire(){
		return !pisandoElSuelo;
	}
}
