using UnityEngine;
using System.Collections;

public class Pong : MonoBehaviour {
	public Juego game;
	// Use this for initialization
	void Start () {
		FutileParams fparams = new FutileParams (true, true, true, true);

		fparams.AddResolutionLevel (510.0f, 1.0f, 1.0f,"");

		fparams.origin = new Vector2 (0.5f, 0.5f);

		Futile.instance.Init (fparams);



		Futile.atlasManager.LoadAtlas ("Atlases/AtlasPiezas");
		Futile.atlasManager.LoadAtlas ("Atlases/ArialFont");
		Futile.atlasManager.LoadFont("arial", "arial", "Atlases/arial", 0, 0);


		game = new Juego ();
	}
	
	// Update is called once per frame
	void Update () {
		game.Update(Time.deltaTime);
	}
}
