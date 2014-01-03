using UnityEngine;
using System.Collections;

public class Sonido : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void PlayFX(AudioSource audio){
		if (Configuracion.configuracionGeneral.estaElSonidoActivo){
			audio.Play();
		}
	}

	public static void PlayFX(Vector3 pos, AudioClip sonido){
		if (Configuracion.configuracionGeneral.estaElSonidoActivo){
			AudioSource.PlayClipAtPoint(sonido,pos);
		}
	}

}
