using UnityEngine;
using System.Collections;

public abstract class InputWrapper:MonoBehaviour {

	protected int _jump=0;
	protected int _horizontal=0;

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
}
