using UnityEngine;
using System.Collections;

public class nivel : MonoBehaviour {


	public GUISkin estilo;

	public int Puntaje=0;

	Rect posicionPuntaje;
	GUIStyle estiloPuntaje;

	void Start () {
		posicionPuntaje = new Rect (100, 20, 400, 40);
		estiloPuntaje = estilo.GetStyle ("puntaje");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.Label (posicionPuntaje, Puntaje + "", this.estiloPuntaje);
	}

	public void jugadorMurio(){
		
	}
}
