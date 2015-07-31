package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.scenes.scene2d.Stage;

public class Intro implements Screen {
	
	final StartLife game;
	private OrthographicCamera camera;
	private Texture bg;
	int currentScreen = 1;
	
	public Intro(final StartLife gam){
		
		this.game = gam;
		
		bg = new Texture(Gdx.files.internal("intro/1.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
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
		game.batch.end();
		
		
		 if (Gdx.input.justTouched()) {
			if(currentScreen == 1){
				bg = new Texture(Gdx.files.internal("intro/2.jpg"));
				game.batch.begin();
				game.batch.draw(bg, 0, 0);
				game.batch.end();
				currentScreen++;
			}
			else if(currentScreen == 2){
				bg = new Texture(Gdx.files.internal("intro/3.jpg"));
				game.batch.begin();
				game.batch.draw(bg, 0, 0);
				game.batch.end();
				currentScreen++;
			}
			else if(currentScreen == 3){
				bg = new Texture(Gdx.files.internal("intro/4.jpg"));
				game.batch.begin();
				game.batch.draw(bg, 0, 0);
				game.batch.end();
				currentScreen++;
			}
			else if(currentScreen == 4){
				bg = new Texture(Gdx.files.internal("intro/5.jpg"));
				game.batch.begin();
				game.batch.draw(bg, 0, 0);
				game.batch.end();
				currentScreen++;
			}
			else if(currentScreen == 5){
				bg = new Texture(Gdx.files.internal("intro/6.jpg"));
				game.batch.begin();
				game.batch.draw(bg, 0, 0);
				game.batch.end();
				currentScreen++;
			}
			else if(currentScreen == 6){
				bg = new Texture(Gdx.files.internal("intro/7.jpg"));
				game.batch.begin();
				game.batch.draw(bg, 0, 0);
				game.batch.end();
				currentScreen++;
			}
			else if(currentScreen == 7){
				game.setScreen(new TurnStart(game));
	            dispose();
	            currentScreen++;
			}
			System.out.println(currentScreen);
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
	public void hide() {
		// TODO Auto-generated method stub

	}

	@Override
	public void dispose() {
		// TODO Auto-generated method stub
		bg.dispose();
	}

}
