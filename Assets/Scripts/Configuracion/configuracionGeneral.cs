using UnityEngine;
using System.Collections;


namespace Configuracion {
public class configuracionGeneral {

	 static bool cacheSonidoActivo=true;
	 static bool cacheMusicaActiva=true;
	
	static bool loaded=false;

	public static void Load(){
			if (!PlayerPrefs.HasKey ("generalinit")) {
				 Default();
			}
	
			if (PlayerPrefs.HasKey("sonido_activo")){
				cacheSonidoActivo= PlayerPrefs.GetInt("sonido_activo")>0;
			}
			
			if (PlayerPrefs.HasKey("musica_activo")){
				cacheMusicaActiva= PlayerPrefs.GetInt("musica_activo")>0;
			}
		
		}
		

	static void Default(){
			PlayerPrefs.SetString("deflang","en");
			PlayerPrefs.SetInt("generalinit",1);
			PlayerPrefs.Save ();
	}

		public static string Idioma{

			get{
				if (PlayerPrefs.HasKey("idioma")){
					return PlayerPrefs.GetString("idioma");
				}
				return null;
			}
			set{
				PlayerPrefs.SetString("idioma",value);
				PlayerPrefs.Save();
				if (LanguageManager.HasInstance){
					LanguageManager.Instance.ChangeLanguage(value);
				}
			}

		}

		public static bool estaElSonidoActivo{
			get{
				if (!loaded){
					Load();
				}
				return cacheSonidoActivo;
			}
			set{
				int val=0;
				if (value){
					val=1;
				}
				cacheSonidoActivo=value;
				PlayerPrefs.SetInt("sonido_activo",val);
				PlayerPrefs.Save();

			}
		}


		public static bool estaLaMusicaActiva{
			get{
				if (!loaded){
					Load();
				}
				return cacheMusicaActiva;
			}
			set{
				int val=0;
				if (value){
					val=1;
				}
				cacheMusicaActiva=value;
				PlayerPrefs.SetInt("musica_activo",val);
				PlayerPrefs.Save();
				
			}
		}
}
}