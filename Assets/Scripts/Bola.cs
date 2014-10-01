using UnityEngine;
using System.Collections;

public class Bola : FSprite {
	public float xVelocity;
	public float yVelocity;
	public float defaultVelocity;
	public float currentVelocity;
	
	public Bola() : base("bolita") {
		defaultVelocity = 250.0f;
		currentVelocity = defaultVelocity;
	}
}