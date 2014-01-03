using UnityEngine;
using System.Collections;

public abstract class InputWrapper:MonoBehaviour {

	protected int _jump=0;
	protected int _horizontal=0;

	protected bool[] Disparar= new bool[10];

	public int Jump {
		get{
			return _jump;
		}
		protected set {
			_jump=value;
		}
	}

	public int Horizontal {
		get{
			return _horizontal;
		}
		protected set{
			_horizontal=value;
		}
	}

	public bool getDisparar(int index){
		return Disparar[index];
	}
}
