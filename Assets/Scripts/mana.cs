using UnityEngine;
using System.Collections;

public class mana : MonoBehaviour {

	public int maxMana=100;
	public float manaActual=100;
	public int manaRecuperada=0;
	public int tiempoDeRecupararMana=1;
	float manaRecuperadaPorSegundo;

	// Use this for initialization
	void Start () {
		manaRecuperadaPorSegundo=manaRecuperada/(float)tiempoDeRecupararMana;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		manaActual+=manaRecuperadaPorSegundo*Time.deltaTime;
		if (manaActual>maxMana){
			manaActual=maxMana;
		}
	}

	public bool restarMana(int cantidad){
		if (manaActual>=cantidad){
			manaActual-=cantidad;
			return true;
		}
		return false;
	}

	public void AnadirMana(int masMana){
		manaActual+=masMana;
		if (manaActual>this.maxMana){
			this.manaActual=maxMana;
		}
	}
}
