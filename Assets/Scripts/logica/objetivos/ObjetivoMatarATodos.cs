using System;
using Logica.Objetivos;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using vida;



namespace Logica.Objetivos
{
	[RequireComponent(typeof(nivel))]
	public class ObjetivoMatarATodos:Objetivo
	{

		public String tagObjetosIniciales="enemigo";

		nivel logicalDelNivel;
		List<GameObject> aMatar = new List<GameObject>();


		void Awake(){
			logicalDelNivel = GetComponent<nivel> ();
			GameObject[] enemigostmp = GameObject.FindGameObjectsWithTag (tagObjetosIniciales);
			foreach (GameObject enemigo in enemigostmp) {
				enemigo.GetComponent<Vida>().estaMueriendo+=ObjetivoEstaMuriendo;
				aMatar.Add(enemigo);
			}
		}

		public override bool ObjetivoCumplido ()
		{
			return aMatar.Count == 0;
		}


		//
		void FixedUpdate(){


		}

		void ObjetivoEstaMuriendo(GameObject objeto,vida.motivoDeMuerte motivo){
			Vida tmpvida = objeto.GetComponent<Vida> ();
			if (tmpvida != null) {
				tmpvida.estaMueriendo -= ObjetivoEstaMuriendo;
				if (motivo == motivoDeMuerte.SinVida){
					logicalDelNivel.anadirPuntos(tmpvida.PuntosDeRecompensaAlMorir);
				}
				aMatar.Remove (objeto);
			}
			if (aMatar.Count==0){
				base.OnHaCumplidoElObjetivo(this);
			}
		}


	}
}

