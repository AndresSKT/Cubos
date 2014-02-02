using UnityEngine;
using System.Collections;

namespace Logica.Objetivos{
public abstract class Objetivo : MonoBehaviour {

	public delegate void notificacionDeObjetivoHandler(Objetivo objetivo);
	public event notificacionDeObjetivoHandler HaCumplidoElObjetivo;

	public abstract bool ObjetivoCumplido ();

		protected void OnHaCumplidoElObjetivo(Objetivo on){
			notificacionDeObjetivoHandler tmp = HaCumplidoElObjetivo;
			if (tmp!=null){
				HaCumplidoElObjetivo(on);
			}
		}

	}


}