using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using vida;

[RequireComponent(typeof(nivel))]
public class SumarPuntajePorEnemigosMuertos : MonoBehaviour {

	nivel logicalDelNivel;
	public string tagObjetosIniciales="enemigo";


	void Awake(){
		logicalDelNivel = GetComponent<nivel> ();
		GameObject[] enemigostmp = GameObject.FindGameObjectsWithTag (tagObjetosIniciales);
		foreach (GameObject enemigo in enemigostmp) {
			enemigo.GetComponent<Vida>().estaMueriendo+=ObjetivoEstaMuriendo;
		}

	}

	void ObjetivoEstaMuriendo(GameObject objeto,vida.motivoDeMuerte motivo){
		Vida tmpvida = objeto.GetComponent<Vida> ();
		if (tmpvida != null) {
			tmpvida.estaMueriendo -= ObjetivoEstaMuriendo;
			if (motivo == motivoDeMuerte.SinVida){
				logicalDelNivel.anadirPuntos(tmpvida.PuntosDeRecompensaAlMorir);
			}
		}
	}
}
