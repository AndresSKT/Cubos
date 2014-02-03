using UnityEngine;
using System.Collections;
using UnityEngine.WSA;
public class CambioTamanoVentanaWindowsStore : MonoBehaviour {

	private static CambioTamanoVentanaWindowsStore instancia=null;

	public delegate void OnCambioPantalla(int ancho, int largo);
	public static event OnCambioPantalla cambioTamanoPantalla{
		add {
			if (instancia==null){
				instanciar();
			}
			instancia.cambiando+=value;
		}
		remove{
			if (instancia!=null){
				instancia.cambiando-=value;
			}
		}
	}

	private event OnCambioPantalla cambiando;

	private static void instanciar(){
		GameObject ins = new GameObject();
		ins.AddComponent<CambioTamanoVentanaWindowsStore>();
		if (instancia==null){
			instancia = ins.GetComponent<CambioTamanoVentanaWindowsStore>();
		}
	}

	// Use this for initialization
	void Start () {
#if UNITY_METRO
		UnityEngine.WSA.Application.windowSizeChanged+=OnActivacionPantalla;
#endif

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnActivacionPantalla(int ancho, int alto){
		OnCambioPantalla tmp = cambiando;
		if (tmp!=null){
			cambiando(ancho,alto);
		}
	}
}
