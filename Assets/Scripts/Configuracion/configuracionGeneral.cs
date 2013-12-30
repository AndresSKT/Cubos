using UnityEngine;
using System.Collections;


namespace Configuracion {
public class configuracionGeneral {


	public static void Load(){
			if (!PlayerPrefs.HasKey ("generalinit")) {
				 Default();
			}
	}

	static void Default(){
			PlayerPrefs.SetString("deflang","en");
			PlayerPrefs.SetInt("generalinit",1);
			PlayerPrefs.Save ();
	}

		public static string UserPreferedLanguage{
			get{
				if (PlayerPrefs.HasKey("userlang")){
					return PlayerPrefs.GetString("userlang");
				}
				return PlayerPrefs.GetString("deflang");
			}
		}
}
}