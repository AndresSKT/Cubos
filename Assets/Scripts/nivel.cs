using UnityEngine;
using System.Collections;

public class nivel : MonoBehaviour {


	public GUISkin estilo;
	public Texture texturaVida;
	public int Puntaje=0;
	public GameObject jugador;


	Rect posicionPuntaje;
	Rect posGrupoVida;
	Rect posBarraVida;
	Rect recBarraVidaReal;
	Rect posLabelVida;

	GUIStyle estiloPuntaje;
	GUIStyle estiloLabelPuntaje;
	float porcentajeVida=0;
	float porcentajeVidaActual=0;
	float longitudBarraDeVida=200;
	string ContenidoLabelVida;

	Vida vidaJugador=null;


	void Start () {
		estiloPuntaje = estilo.GetStyle ("puntaje");
		estiloLabelPuntaje = estilo.GetStyle ("label_vida");

		posicionPuntaje = new Rect (100, 20, 300, this.estiloPuntaje.fontSize+estiloPuntaje.padding.bottom);
		posGrupoVida = new Rect (100,posicionPuntaje.yMax + 5,250,30);
		posBarraVida = new Rect (10, 3,longitudBarraDeVida, 22);
		recBarraVidaReal = new Rect (0, 0,longitudBarraDeVida, 22);
		posLabelVida = new Rect (10,3,longitudBarraDeVida,22);
		if (jugador != null) {
			setJugador(jugador);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (vidaJugador != null) {
			porcentajeVida = ((float)vidaJugador.vida/vidaJugador.vidaMaxima);
			ContenidoLabelVida = vidaJugador.vida+" / "+vidaJugador.vidaMaxima;
		} else {
			porcentajeVida=0;
			ContenidoLabelVida = "0/?";
		}
		float nporcentajeVidaActual= Mathf.Lerp(porcentajeVidaActual,porcentajeVida,Time.deltaTime);
		if (Mathf.Abs (nporcentajeVidaActual - porcentajeVida) < 0.01) {
			nporcentajeVidaActual=porcentajeVida;
		}
		porcentajeVidaActual = nporcentajeVidaActual;
		posBarraVida.width=(porcentajeVidaActual*longitudBarraDeVida);
	}

	void OnGUI(){
		GUI.Label (posicionPuntaje, Puntaje + "", this.estiloPuntaje);
		GUI.BeginGroup (posGrupoVida, estilo.box);
			GUI.BeginGroup (posBarraVida);
			GUI.DrawTexture (recBarraVidaReal, texturaVida);
			GUI.EndGroup ();
		GUI.Label (posLabelVida, ContenidoLabelVida,estiloLabelPuntaje);
		GUI.EndGroup ();
	}

	public void jugadorMurio(){
		
	}

	public void setJugador(GameObject JUG){
		jugador = JUG;
		vidaJugador = jugador.GetComponent<Vida> ();
	}
}
