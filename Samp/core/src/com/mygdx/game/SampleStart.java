package com.mygdx.game;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;
import com.badlogic.gdx.scenes.scene2d.ui.*;
import com.badlogic.gdx.scenes.scene2d.Stage;

public class SampleStart extends ApplicationAdapter {
	SpriteBatch batch;
	Texture img;
	Texture startButton;
	Texture exitButton;
	//ImageButton startImage;
	//Stage stage;
	
	@Override
	public void create () {
		batch = new SpriteBatch();
		img = new Texture("startBackground.jpg");
		startButton = new Texture("start.jpg");
		//stage = new Stage();
		exitButton = new Texture("exit.jpg");
		
		
	}

	@Override
	public void render () {
		Gdx.gl.glClearColor(1, 0, 0, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		batch.begin();
		batch.draw(img, 0, 0);
		batch.draw(startButton, 275, 150, 130, 70);
		batch.draw(exitButton, 275, 50, 130, 70);
		batch.end();

	}
}
