using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class ControladorEnemigo : MonoBehaviour {

	public bool seMueve=true;
	public float distanciaALaDerecha=100;
	public float distanciaALaIzquierda=100;
	public float velocidad=10;
	public int daño=20;
	public float tiempoParaDañar=1f;

	float ultimoDaño=0;
	bool miraAlaDerecha=false;
	Animator animacion;

	private Vector2 posicionOriginal;
	// Use this for initialization
	void Start () {
		posicionOriginal = new Vector2 (transform.position.x, transform.position.y);
		animacion = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!seMueve) {
			return;
		}
		if ((miraAlaDerecha && Mathf.Abs (transform.position.x - posicionOriginal.x) >= distanciaALaDerecha && transform.position.x - posicionOriginal.x>0) ||
		    (!miraAlaDerecha && Mathf.Abs (transform.position.x - posicionOriginal.x) >= distanciaALaIzquierda && transform.position.x - posicionOriginal.x<0)) {
			flip();
		}
		rigidbody2D.velocity = new Vector2 (velocidad*(miraAlaDerecha?1:-1), rigidbody2D.velocity.y);
		animacion.SetFloat ("velocidad", velocidad);

	}

	void flip(){
		miraAlaDerecha = !miraAlaDerecha;
		transform.localScale = new Vector3 (transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
		//posicionOriginal = transform.position;
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.CompareTag ("Player")) {
			atacarJugador(col.gameObject);
			return;
		}

		foreach (ContactPoint2D punto in col.contacts) {
			if ((punto.normal.x>=0.5 && !miraAlaDerecha) || ((punto.normal.x<=-0.5 && miraAlaDerecha))){
				flip();
				break;
			}
		}
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player")) {
			atacarJugador(col.gameObject);
		}
	}

	void atacarJugador(GameObject jug){
		if (Time.time-ultimoDaño<tiempoParaDañar){return;}
		ultimoDaño=Time.time;
		Vida cont = jug.GetComponent<Vida>();
		cont.aplicarDaño(daño);

	}
}
