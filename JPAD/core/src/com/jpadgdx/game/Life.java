package com.jpadgdx.game;

import com.badlogic.gdx.ApplicationListener;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.Input.Keys;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Rectangle;

public class Life implements ApplicationListener {
	
	private SpriteBatch batch;
	private Music opening;
	private Sound step;
	private OrthographicCamera camera;
	private Texture character;
	private Rectangle container;
	private Texture bg;
	
	@Override
	public void create () {
		
		batch = new SpriteBatch();
		character = new Texture(Gdx.files.internal("sprites/sample.png"));
		opening = Gdx.audio.newMusic(Gdx.files.internal("audio/opening.mp3"));
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		opening.setLooping(true);
		opening.play();
		
		bg = new Texture(Gdx.files.internal("maps/map.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
		
		
		batch = new SpriteBatch();
		
		container = new Rectangle();
		container.x = 800 / 2 - 64 / 2;
		container.y = 20;
		container.width = 64;
		container.height = 64;
	}

	@Override
	public void render () {
		Gdx.gl.glClearColor(0, 0, 0.2f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		batch.setProjectionMatrix(camera.combined);
		batch.begin();
		batch.draw(bg, 0, 0);
		batch.draw(character, container.x, container.y);
		batch.end();
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
	    if(container.x > 1024 - 64) container.x = 1024 - 64;
	    if(container.y < 0) container.y = 0;
	    if(container.y > 512 - 64) container.y = 512 - 64;
	    
	    if (Gdx.input.isKeyPressed(Input.Keys.A)) {
	        camera.zoom += 0.02;
	    }
	    if (Gdx.input.isKeyPressed(Input.Keys.Q)) {
	        camera.zoom -= 0.02;
	    }
	
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
		batch.dispose();
		opening.dispose();
		character.dispose();
		
	}
}
