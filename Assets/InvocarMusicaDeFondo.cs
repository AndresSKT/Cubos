using UnityEngine;
using System.Collections;
using Herramientas;


public class InvocarMusicaDeFondo : MonoBehaviour {

    public AudioClip fondo;

	// Use this for initialization
	void Start () {
        if (fondo != null) {
            Sonido.PlayBackgroundInLoop(fondo);   
        }
        Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
