using UnityEngine;
using System.Collections;

public class controles_UI :InputWrapper {


	public GUISkin estilo;
	

	float margenInferior=10;

	Rect posFlechaIzquierda = new Rect(20,0,48,48);
	GUIStyle estiloFlechaIzquierda;

	Rect posFlechaDerecha = new Rect(100,0,48,48);
	GUIStyle estiloFlechaDerecha;


	Rect posSaltar = new Rect(0,0,48,48);
	GUIStyle estiloSaltar;


	void Start () {
		posFlechaIzquierda.y = Screen.height - (posFlechaIzquierda.height + margenInferior);
		estiloFlechaIzquierda = estilo.GetStyle ("flecha_izquierda");
	
	
		posFlechaDerecha.y = Screen.height - (posFlechaDerecha.height + margenInferior);
		estiloFlechaDerecha = estilo.GetStyle ("flecha_derecha");


		posSaltar.y = Screen.height - (posSaltar.height + margenInferior);
		posSaltar.x = Screen.width - (posSaltar.width + 30);
		estiloSaltar = estilo.GetStyle ("saltar");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		//boton izquierda
		this.Horizontal= 0;
		Jump = 0;
		if (GUI.RepeatButton (posFlechaIzquierda,"",estiloFlechaIzquierda)) {
			_horizontal-=1;
		}

		if (GUI.RepeatButton (posFlechaDerecha,"",this.estiloFlechaDerecha)) {
			_horizontal+=1;
		}

		if (GUI.RepeatButton (this.posSaltar,"",this.estiloSaltar)) {
			this.Jump=1;
		}
	}
}
