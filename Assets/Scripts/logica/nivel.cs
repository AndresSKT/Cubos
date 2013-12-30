using UnityEngine;
using System.Collections;
using Configuracion;


public class nivel : MonoBehaviour {


	public GUISkin estilo;
	public Texture texturaVida;
	public Texture texturaMana;
	public Texture fondoBarra;

	public int Puntaje=0;
	public GameObject jugador;


	Rect posicionPuntaje;
	Rect posGrupoVida;
	Rect posBarraVida;
	Rect recBarraVidaReal;
	Rect posLabelVida;
	Rect posLabelNombreVida;
	Rect recfondoBarraVida;

	Rect posBarraMana;
	Rect recBarraManaReal;
	Rect posLabelMana;
	Rect posLabelNombreMana;
	Rect recfondoBarraMana;

	GUIStyle estiloPuntaje;
	GUIStyle estiloLabelVida;
	GUIStyle estiloLabelMana;

	float porcentajeVida=0;
	float porcentajeVidaActual=0;

	float porcentajeMana=0;
	float porcentajeManaActual=0;

	float longitudBarraDeVida;
	float longitudBarraDeMana;
	string ContenidoLabelVida;
	string ContenidoLabelMana;


	float _velocidadAnimacionVida=0f;
	float _velocidadAnimacionMana=0f;
	float tiempoEnAlcanzarPorcentejaDeLaBarra=1f;


	Vida vidaJugador=null;
	mana manaJugador=null;

	string labelPuntaje="";
	string labelVida="";
	string labelMana="";

	LanguageManager traductor;

	void Start () {
		estiloPuntaje = estilo.GetStyle ("puntaje");
		estiloLabelVida = estilo.GetStyle ("label_vida");
		estiloLabelMana = estilo.GetStyle("label_mana");

		posicionPuntaje = new Rect (50, 30, 300, this.estiloPuntaje.fontSize+estiloPuntaje.padding.bottom);
		posGrupoVida = new Rect (posicionPuntaje.xMax + 10,30,Screen.width-250-(posicionPuntaje.xMax),60);
		longitudBarraDeVida = posGrupoVida.width - 80;
		posBarraVida = new Rect (60, 5,longitudBarraDeVida, 20);
		recBarraVidaReal = new Rect (0, 0,longitudBarraDeVida, 22);
		posLabelVida = new Rect (60,5,longitudBarraDeVida,22);
		posLabelNombreVida = new Rect (13, 5, 50, 22);
		recfondoBarraVida = new Rect (posBarraVida.x,posBarraVida.y,recBarraVidaReal.width,posBarraVida.height);

		longitudBarraDeMana = posGrupoVida.width - 80;
		posBarraMana = new Rect (60, posBarraVida.yMax+5,longitudBarraDeVida, 20);
		recBarraManaReal = new Rect (0, 0,longitudBarraDeVida, 22);
		posLabelMana = new Rect (60,posBarraMana.yMin,longitudBarraDeVida,22);
		posLabelNombreMana = new Rect (13, posLabelNombreVida.yMax, 50, 22);
		recfondoBarraMana = new Rect (posBarraMana.x,posBarraMana.y,recBarraManaReal.width,posBarraMana.height);


		if (jugador != null) {
			setJugador(jugador);
		}
		traductor = LanguageManager.Instance;
		labelinit ();
	}


	void labelinit(){

		labelPuntaje = traductor.GetTextValue("guiscore");
		labelVida = traductor.GetTextValue("guilife");
	}
	// Update is called once per frame
	void Update () {

		if (vidaJugador != null) {
			porcentajeVida = ((float)vidaJugador.vida/vidaJugador.vidaMaxima);
			ContenidoLabelVida = vidaJugador.vida+" / "+vidaJugador.vidaMaxima;

			porcentajeMana = ((float)manaJugador.manaActual/manaJugador.maxMana);
			ContenidoLabelMana = manaJugador.manaActual+" / "+manaJugador.maxMana;
		} else {
			porcentajeVida=0;
			ContenidoLabelVida = "0/?";
		
			porcentajeMana=0;
			ContenidoLabelMana = "0/?";
		}
		//porcentajeVidaActual= Mathf.Lerp(porcentajeVidaActual,porcentajeVida,Time.deltaTime);
		porcentajeVidaActual = Mathf.SmoothDamp (porcentajeVidaActual, porcentajeVida,ref _velocidadAnimacionVida, tiempoEnAlcanzarPorcentejaDeLaBarra,Mathf.Infinity,Time.deltaTime);
		posBarraVida.width=(porcentajeVidaActual*longitudBarraDeVida);

		porcentajeManaActual= Mathf.SmoothDamp (porcentajeManaActual, porcentajeMana,ref _velocidadAnimacionMana, tiempoEnAlcanzarPorcentejaDeLaBarra,Mathf.Infinity,Time.deltaTime);
		posBarraMana.width=(porcentajeManaActual*longitudBarraDeMana);

	}

	void OnGUI(){
		GUI.Label (posicionPuntaje,labelPuntaje+": "+Puntaje, this.estiloPuntaje);
		GUI.BeginGroup (posGrupoVida, estilo.box);
			GUI.DrawTexture(recfondoBarraVida,fondoBarra);
			GUI.BeginGroup (posBarraVida);
				GUI.DrawTexture (recBarraVidaReal, texturaVida);
			GUI.EndGroup ();
			GUI.Label (posLabelVida, ContenidoLabelVida,estiloLabelVida);
			GUI.Label (posLabelNombreVida, labelVida + ": ",estilo.label);
			GUI.DrawTexture(recfondoBarraMana,fondoBarra);
			GUI.BeginGroup (posBarraMana);
				GUI.DrawTexture (recBarraManaReal, texturaMana);
			GUI.EndGroup ();

		GUI.Label (posLabelMana, ContenidoLabelMana,estiloLabelMana);
		GUI.Label (posLabelNombreMana, labelMana + ": ",estilo.label);
		GUI.EndGroup ();


	}

	public void jugadorMurio(){
		
	}

	public void setJugador(GameObject JUG){
		jugador = JUG;
		vidaJugador = jugador.GetComponent<Vida> ();
		manaJugador = jugador.GetComponent<mana> ();
	}
}
