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

public class Bed implements Screen {
	
	final StartLife game;
	
	private Music opening;
	private Sound step;
	private OrthographicCamera camera;
	private Texture character;
	private Rectangle container;
	private Texture bg;
	
	public  Bed (final StartLife gam) {
		
		this.game = gam;
		
		character = new Texture(Gdx.files.internal("sprites/256.png"));
		opening = Gdx.audio.newMusic(Gdx.files.internal("audio/opening.mp3"));
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		opening.setLooping(true);
		opening.play();
		
		bg = new Texture(Gdx.files.internal("maps/bedroom.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
		
		container = new Rectangle();
		container.x = 2;
		container.y = 2;
		container.width = 128;
		container.height = 128;
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
		character.dispose();
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
		game.batch.draw(character, container.x, container.y);
		game.batch.end();
		//camera.position.set(container.x, container.y, 0);
		camera.update();
		
		if(Gdx.input.isKeyPressed(Keys.LEFT)){
			step.stop();
			step.play();
			container.x -= 200 * Gdx.graphics.getDeltaTime();
			/*camera.position.set(container.x, container.y, 0);
			camera.update();*/
		}
		if(Gdx.input.isKeyPressed(Keys.RIGHT)){
			step.stop();
			step.play();
			container.x += 200 * Gdx.graphics.getDeltaTime();
			/*camera.position.set(container.x, container.y, 0);
			camera.update();*/
		}
		
		if(container.x < 0) container.x = 0;
	    if(container.x > 1024 - 128){
	    	game.setScreen(new Life(game));
	    	opening.stop();
	    }
	    if(container.y < 0) container.y = 0;
	    if(container.y > 512 - 128) container.y = 512 - 128;
	    
	  /*  if (Gdx.input.isKeyPressed(Input.Keys.A)) {
	        camera.zoom += 0.02;
	    }
	    if (Gdx.input.isKeyPressed(Input.Keys.Q)) {
	        camera.zoom -= 0.02;
	    }*/
	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub
		opening.play();
	}
}
