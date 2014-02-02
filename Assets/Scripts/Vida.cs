using UnityEngine;
using System.Collections;

namespace vida{

	public enum motivoDeMuerte{
		SinVida,
		MuerteAlSalirDelMundo,
		Indefinida
		
	}

public class Vida : MonoBehaviour {

	public delegate void Muriendo(GameObject objeto, motivoDeMuerte motivo);
		
		


	public int vida=100;
	public int vidaMaxima=100;
	public int PuntosDeRecompensaAlMorir=0;
	public GameObject prefabExplosion;
	public AudioSource sonidoDano;
	public AudioClip sonidoMuerte;

	public event Muriendo estaMueriendo;
		[HideInInspector]
		public motivoDeMuerte motivoAlMorir=motivoDeMuerte.Indefinida;
		private bool quitandoLaApp=false;

		public bool estaVivo{
			get{
				return vida>0;
			}

		}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (vida <= 0) {
			motivoAlMorir=motivoDeMuerte.SinVida;
			Destroy(this.gameObject);
		}
	}
		

	public void aplicarDaño(int dano){
		vida -= dano;
			if (sonidoDano!=null){
				Sonido.PlayFX(sonidoDano);
			}
	}

		public void anadirVida(int vida){
			this.vida+=vida;
			if (this.vida>this.vidaMaxima){
				this.vida=vidaMaxima;
			}
		}

	
		
	void OnDestroy(){
        if (prefabExplosion!=null && !quitandoLaApp && motivoAlMorir!=motivoDeMuerte.Indefinida){
				(Instantiate(prefabExplosion) as GameObject).transform.position=this.gameObject.transform.position;
			}

			if (sonidoMuerte!=null && motivoAlMorir!=motivoDeMuerte.Indefinida){
				Sonido.PlayFX(transform.position,sonidoMuerte);

			}

			Muriendo tmp = estaMueriendo;
			if (tmp != null) {
				estaMueriendo(this.gameObject,motivoAlMorir);
			}
	}

		void OnApplicationQuit(){
			quitandoLaApp=true;
		}

}



}

