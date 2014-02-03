using UnityEngine;
using System.Collections;
using Configuracion;


public class Sonido : MonoBehaviour {

    private static AudioSource instanciaMusicaActual=null;
    private static Sonido _instancia = null;
    private static bool debeEstarSonandoLaMusica = false;
    private static Sonido instancia{
            get{
                if (_instancia==null) {
                    
                    GameObject dummy = new GameObject();
                    DontDestroyOnLoad(dummy);
                    dummy.AddComponent<Sonido>();
                    _instancia = dummy.GetComponent<Sonido>();
                }
                return _instancia;
             }
        }

	// Use this for initialization
	void Awake () {
        if (_instancia != null) {
            Destroy(this);
        }
        else
        {
            _instancia = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (debeEstarSonandoLaMusica && configuracionGeneral.estaLaMusicaActiva && instanciaMusicaActual != null && !instanciaMusicaActual.isPlaying) {
			instanciaMusicaActual.Play();
        }
        else if (!configuracionGeneral.estaLaMusicaActiva && instanciaMusicaActual != null && instanciaMusicaActual.isPlaying) {
			instanciaMusicaActual.Stop();
        }
	}

    public static void continuarBackground() {
        if (instanciaMusicaActual != null && !instanciaMusicaActual.isPlaying) {
            debeEstarSonandoLaMusica = true;
            //instanciaMusicaActual.Play();
            
        }
    }

    public static void PausarBackGround() {
        if (instanciaMusicaActual != null && instanciaMusicaActual.isPlaying) {
            debeEstarSonandoLaMusica = false;
            instanciaMusicaActual.Pause();
        }
    
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

    public static void PlayBackgroundInLoop(AudioClip audio) {
        DetenerBackground();
        instanciaMusicaActual = instancia.GetComponent<AudioSource>();
        if (instanciaMusicaActual == null) {
            instancia.gameObject.AddComponent<AudioSource>();
            instanciaMusicaActual = instancia.GetComponent<AudioSource>();
        }
        instanciaMusicaActual.volume = 1;
        instanciaMusicaActual.loop = true;
        instanciaMusicaActual.clip = audio;
		debeEstarSonandoLaMusica = true;
		if (!Configuracion.configuracionGeneral.estaLaMusicaActiva) { return; }
        instanciaMusicaActual.Play();
        

    }

    public static void DetenerBackground() {
        if (instanciaMusicaActual != null) {
            debeEstarSonandoLaMusica = false;
            instanciaMusicaActual.Stop();
            
        }
    }

}
