using UnityEngine;
using System.Collections;

namespace Menu{

public class SeleccionDeIdioma : MonoBehaviour {

		public Idioma[] idiomasDisponibles;
		public GUISkin estilo;
		public bool puedeCerrar;

		GUIStyle estiloBotonesBanderas;
		GUIStyle estiloBotonAtras;

		Texture2D texturaFondo;
		Rect posFondo;
		Rect posCerrar;

		int cantidaDeFilas=0;
		int cantidadDeColumnas=4;
		Rect areaBanderas;
		int anchoBandera=128;
		int altoBandera=128;
		int margen=10;
	
	// Use this for initialization
		void Start () {
			estiloBotonesBanderas = estilo.GetStyle("boton_banderas");
			//hoverBanderas = estilo.GetStyle("hover_boton_banderas");
			cantidaDeFilas = Mathf.CeilToInt(idiomasDisponibles.Length/(float)cantidadDeColumnas);
			Vector2 tamBoton = estiloBotonesBanderas.CalcSize(new GUIContent(idiomasDisponibles[0].bandera));
			areaBanderas = new Rect(0,0,(anchoBandera+margen)*cantidadDeColumnas,(cantidaDeFilas*tamBoton.y));
			areaBanderas.x=Mathf.Max(0,(Screen.width-areaBanderas.width)/2);
			areaBanderas.y=Mathf.Max(0,(Screen.height-areaBanderas.height)/2);
			texturaFondo = new Texture2D(1,1);
			texturaFondo.SetPixel(0,0,Color.black);
			texturaFondo.Apply();
			posFondo = new Rect(0,0,Screen.width,Screen.height);
			posCerrar = new Rect(20,20,64,64);
			estiloBotonAtras = estilo.GetStyle("boton_atras");
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
			GUI.depth=0;
			//GUI.DrawTexture(posFondo,texturaFondo);
			if (puedeCerrar){
				if (GUI.Button(posCerrar,"",estiloBotonAtras)){
					this.enabled=false;
				}
			}

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
					//GUI.Button(GUILayoutUtility.GetLastRect(),"",hoverBanderas);

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