package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
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
	private Texture character;
	private Rectangle container;
	private Texture bg;
	
	public  Life (final StartLife gam) {
		
		this.game = gam;
		
		character = new Texture(Gdx.files.internal("sprites/32.png"));
		opening = Gdx.audio.newMusic(Gdx.files.internal("audio/mapBG.mp3"));
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		opening.setLooping(true);
		opening.play();
		
		bg = new Texture(Gdx.files.internal("maps/map.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
		
		container = new Rectangle();
		container.x = 50;
		container.y = 390;
		container.width = 32;
		container.height = 32;
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
		Gdx.gl.glClearColor(0, 0, 0, 0);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		game.batch.setProjectionMatrix(camera.combined);
		game.batch.begin();
		game.batch.draw(bg, 0, 0);
		game.batch.draw(character, container.x, container.y);
		game.batch.end();
		camera.position.set(container.x, container.y, 0);
		camera.update();
		
		if(Gdx.input.isKeyPressed(Keys.LEFT)){
			step.play();
			container.x -= 200 * Gdx.graphics.getDeltaTime();
			camera.position.set(container.x, container.y, 0);
			camera.update();
		}
		if(Gdx.input.isKeyPressed(Keys.RIGHT)){
			step.play();
			container.x += 200 * Gdx.graphics.getDeltaTime();
			camera.position.set(container.x, container.y, 0);
			camera.update();
		}
		
		if(Gdx.input.isKeyPressed(Keys.UP)){
			step.play();
			container.y += 200 * Gdx.graphics.getDeltaTime();
			camera.position.set(container.x, container.y, 0);
			camera.update();
		}
		
		if(Gdx.input.isKeyPressed(Keys.DOWN)){
			step.play();
			container.y -= 200 * Gdx.graphics.getDeltaTime();
			camera.position.set(container.x, container.y, 0);
			camera.update();
		}
		
		if(container.x < 0) container.x = 0;
	    if(container.x > 1024 - 32) container.x = 1024 - 32;
	    if(container.y < 0) container.y = 0;
	    if(container.y > 512 - 32) container.y = 512 - 32;
	    if(container.x > 50 && container.x < 60 && container.y > 390){ 
	    	game.setScreen(new Bed(game));
	    	opening.stop();
	    }
	    
	    if (Gdx.input.isKeyPressed(Input.Keys.A)) {
	        camera.zoom += 0.02;
	    }
	    if (Gdx.input.isKeyPressed(Input.Keys.Q)) {
	        camera.zoom -= 0.02;
	    }
	    
	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub
		opening.play();
	}
}
