using UnityEngine;
using System.Collections;

public class Juego {
	public Barra player1;
	public Barra player2;
	public Bola ball;
	public FLabel lblScore1;
	public FLabel lblScore2;
	public bool paused = false; 
	public int maxScore = 3; 
	
	public Juego() {
		//players
		player1 = new Barra("Jugador 1, (w/s)");  
		player2 = new Barra("Jugador 2, (p/l)");  
		ResetPaddles();                     
		
		ball = new Bola();                
		ResetBall();                        
		
		player1.color = Color.blue;
		player2.color = Color.magenta;

		Futile.stage.AddChild(player1);     
		Futile.stage.AddChild(player2);
		Futile.stage.AddChild(ball);


		lblScore1 = new FLabel("arial", player1.name + ": " + player1.score);
		lblScore1.anchorX = 0; 
		lblScore1.anchorY = 0; 
		lblScore1.x = -Futile.screen.halfWidth;
		lblScore1.y = -Futile.screen.halfHeight;
		

		lblScore2 = new FLabel("arial", player2.name + ": " + player2.score);
		lblScore2.anchorX = 1.0f;
		lblScore2.anchorY = 0; 
		lblScore2.x = Futile.screen.halfWidth;
		lblScore2.y = -Futile.screen.halfHeight;
		

		Futile.stage.AddChild(lblScore1);
		Futile.stage.AddChild(lblScore2);
	}   
	
	public void ResetPaddles() {
		player1.x = -Futile.screen.halfWidth + player1.width/2;
		player1.y = 0;                                         
		
		player2.x = Futile.screen.halfWidth - player2.width/2; 
		player2.y = 0;                                         
	}
	
	public void ResetBall(){
		ball.x = 0;
		ball.y = 0;

		ball.yVelocity = (ball.defaultVelocity / 2) - (RXRandom.Float () * ball.defaultVelocity);
		ball.xVelocity = Mathf.Sqrt((ball.defaultVelocity*ball.defaultVelocity) - (ball.yVelocity*ball.yVelocity)) * (RXRandom.Int(2) * 2 - 1);
	}

	public void Update(float dt) {
		if (!paused){

		
			float newPlayer1Y = player1.y;
			float newPlayer2Y = player2.y;
		

			if (Input.GetKey("w")) { newPlayer1Y += dt * player1.currentVelocity; }
			if (Input.GetKey("s")) { newPlayer1Y -= dt * player1.currentVelocity; }
			if (Input.GetKey("p")) { newPlayer2Y += dt * player2.currentVelocity; }
			if (Input.GetKey("l")) { newPlayer2Y -= dt * player2.currentVelocity; }
			

			float newBallX = ball.x + dt * ball.xVelocity;
			float newBallY = ball.y + dt * ball.yVelocity;
			

			Rect ballRect = ball.localRect.CloneAndOffset(newBallX, newBallY);
			Rect player1Rect = player1.localRect.CloneAndOffset(player1.x, newPlayer1Y);
			Rect player2Rect = player2.localRect.CloneAndOffset(player2.x, newPlayer2Y);
			
			if (ballRect.CheckIntersect(player1Rect) && ball.xVelocity < 0) {
				BallPaddleCollision(player1, newBallY, newPlayer1Y);
			}
			if (ballRect.CheckIntersect(player2Rect) && ball.xVelocity > 0) {
				BallPaddleCollision(player2, newBallY, newPlayer2Y);
			}
	
			if (newBallY + (ball.height/2) >= Futile.screen.halfHeight) {
				newBallY = Futile.screen.halfHeight - (ball.height/2) - Mathf.Abs((newBallY - Futile.screen.halfHeight));
				ball.yVelocity = -ball.yVelocity;
			} else if (newBallY - ball.height/2 <= -Futile.screen.halfHeight) {
				newBallY = -Futile.screen.halfHeight + (ball.height/2) + Mathf.Abs((-Futile.screen.halfHeight - newBallY));
				ball.yVelocity = -ball.yVelocity;
			}

			ball.x = newBallX;
			ball.y = newBallY;
			player1.y = newPlayer1Y;
			player2.y = newPlayer2Y;
	

			Barra scoringPlayer = null;
			
			if (newBallX - ball.width/2 < -Futile.screen.halfWidth) { 
				scoringPlayer = player2;
			} else if (newBallX + ball.width/2 > Futile.screen.halfWidth) {
				scoringPlayer = player1;
			}
			

			if (scoringPlayer != null) {
				ResetBall();
				ResetPaddles();
				scoringPlayer.score++; 
				lblScore1.text = player1.name + ": " + player1.score; 
				lblScore2.text = player2.name + ": " + player2.score;
				

				if (scoringPlayer.score >= maxScore) {
					paused = true; 
					Futile.stage.RemoveAllChildren();
					FLabel lblWinner = new FLabel("arial", scoringPlayer.name + " Gana!");
					Futile.stage.AddChild(lblWinner); 
				}
			}	
		}
	}
	public void BallPaddleCollision(Barra player, float newBallY, float newPaddleY) {
		float localHitLoc = newBallY - newPaddleY; 
		float angleMultiplier = Mathf.Abs(localHitLoc / (player.height/2)); 
		
	
		float xVelocity = Mathf.Cos(65.0f * angleMultiplier * Mathf.Deg2Rad) * ball.currentVelocity; 
		float yVelocity = Mathf.Sin(65.0f * angleMultiplier * Mathf.Deg2Rad) * ball.currentVelocity;
		

		if (localHitLoc < 0) {
			yVelocity = -yVelocity;
		}
		

		if (ball.xVelocity > 0) {
			xVelocity = -xVelocity;
		}
		

		ball.xVelocity = xVelocity;
		ball.yVelocity = yVelocity;
	}
}