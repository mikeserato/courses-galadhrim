package com.mygdx.game;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.ui.*;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.utils.*;

public class SampleStart extends ApplicationAdapter {
	SpriteBatch batch;
	Sprite img;
	ImageButton startButton;
	ImageButton exitButton;
	Stage stage;
	Table table;
	//Label title;
	
	
	
	@Override
	public void create () {
		batch = new SpriteBatch();
		img = new Sprite(new Texture("startBackground.jpg"));
		startButton = new ImageButton(new SpriteDrawable(new Sprite(new Texture("start.jpg"))), new SpriteDrawable(new Sprite(new Texture("start1.jpg"))));
		stage = new Stage();
		exitButton = new ImageButton(new SpriteDrawable(new Sprite(new Texture("exit.jpg"))), new SpriteDrawable(new Sprite(new Texture("exit1.jpg"))));
		startButton.setHeight(70);
		startButton.setWidth(130);
		startButton.setPosition(275, 150);
		exitButton.setHeight(70);
		exitButton.setWidth(130);
		exitButton.setPosition(275, 50);
		
		//title = new Label("Insert Title", null);

		stage.addActor(startButton);
		stage.addActor(exitButton);
	}

	@Override
	public void render () {
		Gdx.gl.glClearColor(1, 0, 0, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		
		stage.act();
		
		batch.begin();
		batch.draw(img, 0, 0);
		//batch.draw(new Texture("start.jpg"), 275, 150, 130, 70);
		//batch.draw(new Texture("exit.jpg"), 275, 50, 130, 70);
		stage.draw();
		batch.end();


	}
}
