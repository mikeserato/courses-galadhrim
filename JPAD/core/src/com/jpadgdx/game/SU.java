package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.Input.Keys;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;

public class SU implements Screen {

	final StartLife game;
	private Music opening;
	private Sound step;
	private OrthographicCamera camera;
	private Texture bg;
	public Character player;
	Character player1;
	Character player2;
	
	
	public  SU (final StartLife gam, Character player1, Character player2) {
		
		this.game = gam;
		this.player1 = player1;
		this.player2 = player2;
		
		if(TurnStart.turn == 1){
			player = player1;
			player.toArea();
		}
		else{
			player = player2;
			player.toArea();
		}
		
		opening = Gdx.audio.newMusic(Gdx.files.internal("audio/opening.mp3"));
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		opening.setLooping(true);
		opening.play();
		
		bg = new Texture(Gdx.files.internal("maps/SU.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);

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
		Gdx.gl.glClearColor(0, 0, 0.2f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		game.batch.setProjectionMatrix(camera.combined);
		game.batch.begin();
		game.batch.draw(bg, 0, 0);
		game.font.draw(game.batch,"Time Left:  " + player.timeLeft/100 , 10, 500);
		game.batch.draw(player.character, player.x, player.y);
		game.batch.end();
		camera.update();
		
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
			step.stop();
			step.play();
			player.x -= 200 * Gdx.graphics.getDeltaTime();
			
		}
		if(Gdx.input.isKeyPressed(Keys.RIGHT)){
			step.stop();
			step.play();
			player.x += 200 * Gdx.graphics.getDeltaTime();
			
		}
		
		if(player.x < 0) player.x = 0;
	    if(player.x > 1024 - 128){
	    	opening.stop();
	    	if(TurnStart.turn == 1){		
				game.setScreen(new Life(game, player, player2));
			}
			else{
				game.setScreen(new Life(game, player1, player));
			}
	    	dispose();
	    }
	    if(player.y < 0) player.y = 0;
	    if(player.y > 512 - 128) player.y = 512 - 128;
	    
	    player.timeLeft--;
	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub
		opening.play();
	}
}
