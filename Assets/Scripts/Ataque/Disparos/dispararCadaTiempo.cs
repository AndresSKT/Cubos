using UnityEngine;
using System.Collections;
using bala;
using ataque;

namespace ataque.disparos{

	
[RequireComponent(typeof(mana))]
public class dispararCadaTiempo : AtaqueBase {

	public GameObject prefabBala;
	public GameObject puntoDeDisparo;
	
	

	public float tiempoEntreBalas = 0.5f;
	public float velocidad=0;

	float ultimaBala=0;
		mana manaJugador;

	// Use this for initialization
	void Start () {
			direccion=Vector2.right;
			manaJugador=GetComponent<mana>();
	}
	
	// Update is called once per frame
	void Update () {

	}
		public new bool PuedeDisparar{
			get{
				return !(Time.time - ultimaBala < tiempoEntreBalas);
			}
		}

		#region implemented abstract members of AtaqueBase

		public override bool Disparar ()
		{
			if (PuedeDisparar && manaJugador.restarMana(consumoDeMana)){
				ultimaBala = Time.time;
				
				GameObject bala = Instantiate (prefabBala) as GameObject;
				bala.transform.position = puntoDeDisparo.transform.position;
				logicaBala logica = bala.GetComponent<logicaBala> ();
				logica.iniciar(direccion,velocidad);
				return true;
			}
			return false;
		}

		#endregion
}

}