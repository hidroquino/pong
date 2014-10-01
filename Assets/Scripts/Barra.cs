using UnityEngine;
using System.Collections;

public class Barra : FSprite {
	public string name;             
	public int score;               
	public float defaultVelocity;   
	public float currentVelocity;   
	
	public Barra(string name) : base("barrita")  {
		this.name = name;           
		
		defaultVelocity = Futile.screen.height;
		currentVelocity = defaultVelocity;
	}
}