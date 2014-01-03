using UnityEngine;
using System.Collections;

namespace Menu{

public class SeleccionDeIdioma : MonoBehaviour {

		public Idioma[] idiomasDisponibles;
		public GUISkin estilo;

		GUIStyle estiloBotonesBanderas;
		GUIStyle hoverBanderas;

		Texture2D texturaFondo;
		Rect posFondo;

		int cantidaDeFilas=0;
		int cantidadDeColumnas=4;
		Rect areaBanderas;
		int anchoBandera=128;
		int altoBandera=128;
		int margen=10;
	
	// Use this for initialization
		void Start () {
			estiloBotonesBanderas = estilo.GetStyle("boton_banderas");
			hoverBanderas = estilo.GetStyle("hover_boton_banderas");
			cantidaDeFilas = Mathf.CeilToInt(idiomasDisponibles.Length/(float)cantidadDeColumnas);
			areaBanderas = new Rect(0,0,(anchoBandera+margen)*cantidadDeColumnas,(cantidaDeFilas*altoBandera));
			areaBanderas.x=Mathf.Max(0,(Screen.width-areaBanderas.width)/2);
			areaBanderas.y=Mathf.Max(0,(Screen.height-areaBanderas.height)/2);
			texturaFondo = new Texture2D(1,1);
			texturaFondo.SetPixel(0,0,Color.black);
			texturaFondo.Apply();
			posFondo = new Rect(0,0,Screen.width,Screen.height);
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
			GUI.depth=0;
			GUI.DrawTexture(posFondo,texturaFondo);
			GUILayout.BeginArea(areaBanderas,estilo.box);
			GUILayout.BeginVertical();
			for(int f=0;f<idiomasDisponibles.Length;f+=cantidadDeColumnas){
				GUILayout.BeginHorizontal();
				int max=Mathf.Min(4,idiomasDisponibles.Length-f);

				for(int i=0;i<max;i++){
					if (i>0){
						GUILayout.FlexibleSpace();
					}
					if (GUILayout.Button(idiomasDisponibles[f+i].bandera,estiloBotonesBanderas,GUILayout.Width(anchoBandera))){
						Configuracion.configuracionGeneral.Idioma=idiomasDisponibles[f+i].llave;
						enabled=false;
					}
					GUI.Button(GUILayoutUtility.GetLastRect(),"",hoverBanderas);

				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
	}
}
	[System.Serializable]
	public class Idioma{
		public Texture2D bandera;
		public string llave;
	}
}