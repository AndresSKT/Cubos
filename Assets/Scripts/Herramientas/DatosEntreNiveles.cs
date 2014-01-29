using UnityEngine;
using System.Collections;

namespace Herramientas{

	public class DatosEntreNiveles : MonoBehaviour {

		static private DatosEntreNiveles _instancia=null;

		private int _puntaje=0;
		private int _vida=0;
		private float _mana=0;

		public static DatosEntreNiveles Instancia{
			get{
				if (_instancia==null){
					_instancia = instanciar();
				}
				return _instancia;
			}
		}

		private static DatosEntreNiveles instanciar(){
			GameObject tmp = new GameObject();
			tmp.AddComponent<DatosEntreNiveles>();
			DontDestroyOnLoad(tmp);
			return tmp.GetComponent<DatosEntreNiveles>();
		}

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public static int Puntaje{
			get{
				return Instancia._puntaje;
			}
			set{
				Instancia._puntaje=value;
				if (Puntaje<0){
					Instancia._puntaje=0;
				}
			}
		}

		public static int Vida{
			get {
				return Instancia._vida;
			}set{
				_instancia._vida=value;
			}
		}

		public static float Mana{
			get{ return Instancia._mana;}
			set {Instancia._mana=value;}

		}

		public static void ReiniciarPuntaje(){
			Instancia._puntaje=0;
		}

	}
}