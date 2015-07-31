package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input.Keys;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.Rectangle;

public class Life implements Screen {
	
	final StartLife game;
	
	private Music opening;
	private Sound step;
	private OrthographicCamera camera;
	private Texture bg;
	private Rectangle dorm;
	private Rectangle physci;
	private Rectangle su; 
	private Rectangle hum;
	private Rectangle fpark;
	Character player1;
	Character player2;
	Character player;
	
	
	public  Life (final StartLife gam, Character player1, Character player2) {
		
		this.game = gam;
		this.player1 = player1;
		this.player2 = player2;
		
		if(TurnStart.turn == 1){
			player = player1;
			player.toMap();
		}
		else{
			player = player2;
			player.toMap();
		}
		
		opening = Gdx.audio.newMusic(Gdx.files.internal("audio/mapBG.mp3"));
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		opening.setLooping(true);
		opening.play();
		
		bg = new Texture(Gdx.files.internal("maps/map.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
		
		dorm = new Rectangle();
		dorm.x = 35;
		dorm.y = 402;
		dorm.height = 98;
		dorm.width = 160;
		
		physci = new Rectangle();
		physci.x = 155;
		physci.y = 46;
		physci.height = 154;
		physci.width = 145;
		
		fpark = new Rectangle();
		fpark.x = 517;
		fpark.y = 252;
		fpark.height = 78;
		fpark.width = 105;
		
		hum = new Rectangle();
		hum.x = 835;
		hum.y = 97;
		hum.height = 118;
		hum.width = 152;
		
		su = new Rectangle();
		su.x = 765;
		su.y = 382;
		su.height = 98;
		su.width = 160;
				
		
	}

	@Override
	public void resize(int width, int height) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void pause() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void resume() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void dispose() {
		// TODO Auto-generated method stub
		opening.dispose();
		step.dispose();
		bg.dispose();
		
	}

	@Override
	public void show() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void render(float delta) {
		
		// TODO Auto-generated method stub
		Gdx.gl.glClearColor(0, 0, 0, 0);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		game.batch.setProjectionMatrix(camera.combined);
		game.batch.begin();
		game.batch.draw(bg, 0, 0);
		game.font.draw(game.batch,"Time Left:  " + player.timeLeft/100 , 10, 500);
		game.batch.draw(player.character, player.x, player.y);
		game.batch.end();
		
		if(player.timeLeft < 0){
			opening.stop();
			if(TurnStart.turn == 1){
				TurnStart.turn++;
				game.setScreen(new TurnStart(game, player, player2));
			}
			else{
				TurnStart.turn--;
				TurnStart.day++;
				game.setScreen(new TurnStart(game, player1, player));
			}
	    	dispose();
		}
		
		
		if(Gdx.input.isKeyPressed(Keys.LEFT)){
			step.play();
			if(player.overlaps(dorm)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Bed(game,player,player2));
				else
					game.setScreen(new Bed(game,player1,player));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Physci(game,player,player2));
				else
					game.setScreen(new Physci(game,player1,player));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new SU(game,player,player2));
				else
					game.setScreen(new SU(game,player1,player));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Hum(game,player,player2));
				else
					game.setScreen(new Hum(game,player1,player));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new FPark(game,player,player2));
				else
					game.setScreen(new FPark(game,player1,player));
				dispose();
			}
			else
				player.x -= 100 * Gdx.graphics.getDeltaTime();
			
		}
		if(Gdx.input.isKeyPressed(Keys.RIGHT)){
			if(player.overlaps(dorm)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Bed(game,player,player2));
				else
					game.setScreen(new Bed(game,player1,player));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Physci(game,player,player2));
				else
					game.setScreen(new Physci(game,player1,player));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new SU(game,player,player2));
				else
					game.setScreen(new SU(game,player1,player));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Hum(game,player,player2));
				else
					game.setScreen(new Hum(game,player1,player));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new FPark(game,player,player2));
				else
					game.setScreen(new FPark(game,player1,player));
				dispose();
			}
				
			else
				player.x += 100 * Gdx.graphics.getDeltaTime();
		}
		
		if(Gdx.input.isKeyPressed(Keys.UP)){
			step.play();
			if(player.overlaps(dorm)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Bed(game,player,player2));
				else
					game.setScreen(new Bed(game,player1,player));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Physci(game,player,player2));
				else
					game.setScreen(new Physci(game,player1,player));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new SU(game,player,player2));
				else
					game.setScreen(new SU(game,player1,player));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Hum(game,player,player2));
				else
					game.setScreen(new Hum(game,player1,player));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new FPark(game,player,player2));
				else
					game.setScreen(new FPark(game,player1,player));
				dispose();
			}
			else
				player.y += 100 * Gdx.graphics.getDeltaTime();
			
		}
		
		if(Gdx.input.isKeyPressed(Keys.DOWN)){
			step.play();
			if(player.overlaps(dorm)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Bed(game,player,player2));
				else
					game.setScreen(new Bed(game,player1,player));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Physci(game,player,player2));
				else
					game.setScreen(new Physci(game,player1,player));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new SU(game,player,player2));
				else
					game.setScreen(new SU(game,player1,player));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new Hum(game,player,player2));
				else
					game.setScreen(new Hum(game,player1,player));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				if(TurnStart.turn == 1)
					game.setScreen(new FPark(game,player,player2));
				else
					game.setScreen(new FPark(game,player1,player));
				dispose();
			}
			else
				player.y -= 100 * Gdx.graphics.getDeltaTime();
			
			
		}
		
		if(player.x < 20) player.x = 20;
	    if(player.x > 1004 - 32) player.x = 1004 - 32;
	    if(player.y < 20) player.y = 20;
	    if(player.y > 492 - 32) player.y = 492 - 32;
	    
	    player.timeLeft--;
	    
	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub
		opening.play();
	}
		
	
}
