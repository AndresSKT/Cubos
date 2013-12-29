using UnityEngine;
using System.Collections;


public class MenuPrincipal : MonoBehaviour {


	public GUISkin estiloMenu;

	public float anchoMenuCentral=200;
	float altoBotonMenuCentral=64;


	Rect posMenuCentral;
	Rect posBotonJugar;

	bool carga=false;

	GUIStyle estiloBotonMenuCentral;


	// Use this for initialization
	void Start () {
		posMenuCentral = new Rect ((Screen.width - anchoMenuCentral) / 2, altoBotonMenuCentral,anchoMenuCentral, altoBotonMenuCentral * 5);
		posBotonJugar = new Rect (0, 0, anchoMenuCentral, altoBotonMenuCentral);
		estiloBotonMenuCentral = estiloMenu.GetStyle("boton_principal");
	}
	
	// Upis called once per frame
	void Update () {
		if (carga) {
			Debug.Log("Cargando");
		}
	}

	void OnGUI(){
		GUI.BeginGroup (posMenuCentral);
		if (GUI.Button (posBotonJugar, "Jugar", estiloBotonMenuCentral)) {
			carga=true;
			Application.LoadLevel(1);

		}
		GUI.EndGroup();
	}
}
