using UnityEngine;
using System.Collections;

namespace Herramientas{

public class LevelLoader : MonoBehaviour {

		public Texture2D ScreenImage;
		public GUISkin estilo;


		Texture2D fondo;
		const string fondoRes = "Default/UI/fondo_anim_carga";
		const string estiloRes="Default/UI/estiloLoadingScreen";
		Rect posFondo;
		Rect posTexto;
		Rect[] posScreenImages;

		public static LevelLoader instancia=null;
		

		public bool EstaCargandoUnNivel{
			get{
				return Application.isLoadingLevel;
			}
		}

		void Awake(){
			if (instancia==null){
				DontDestroyOnLoad(this.gameObject);
				instancia=this;
			}else{
				//Destroy(this.gameObject);
				this.enabled=false;
			}
		}
	// Use this for initialization
	void Start () {

			fondo= new Texture2D(1,1);
			fondo.SetPixel(0,0,Color.black);
			fondo.Apply();
			posFondo = new Rect(0,0,Screen.width,Screen.height);
			if (ScreenImage==null){
				ScreenImage = Resources.Load<Texture2D>(fondoRes);
			}

			if (estilo==null){
				estilo = Resources.Load<GUISkin>(estiloRes);
			}

			posScreenImages = new Rect[Mathf.CeilToInt(Screen.width/(float)ScreenImage.width)];
			//int posximagenes = Mathf.Min(0,Screen.height-ScreenImage.height-350);
			for(int i=0;i<posScreenImages.Length;i++){
				posScreenImages[i]= new Rect((i*ScreenImage.width),0,ScreenImage.width,ScreenImage.height);
			}

			Vector2 tam = estilo.label.CalcSize(new GUIContent(LanguageManager.Instance.GetTextValue("pantallacargaprincipal")));
			posTexto = new Rect(20,posScreenImages[0].center.y,tam.x*2,tam.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		static public void CargarNivel(string nombre){
			if (LevelLoader.instancia==null){
				GameObject vacio = new GameObject();
				vacio.AddComponent<LevelLoader>();
				//instancia = vacio.GetComponent<LevelLoader>();
			}
			Application.LoadLevel(nombre);
		}

		void OnGUI(){
			if (!EstaCargandoUnNivel){return;}
			GUI.depth=0;
			GUI.DrawTexture(posFondo,fondo);
			foreach(Rect r in posScreenImages){
				GUI.DrawTexture(r,ScreenImage);
			}
			GUI.Label(posTexto,LanguageManager.Instance.GetTextValue("pantallacargaprincipal"),estilo.label);
		}
	
}
}