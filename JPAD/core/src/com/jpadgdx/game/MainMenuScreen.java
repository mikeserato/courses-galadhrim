package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.ImageButton;
import com.badlogic.gdx.scenes.scene2d.utils.SpriteDrawable;

public class MainMenuScreen implements Screen {
	
	final StartLife game;

    OrthographicCamera camera;
    
    SpriteBatch batch;
	Sprite img;
	ImageButton startButton;
	ImageButton exitButton;
	Stage stage;
    
    public MainMenuScreen(final StartLife gam) {
    	
        game = gam;
        

        camera = new OrthographicCamera();
        camera.setToOrtho(false, 1024, 512);
        
        create();

    }

    public void create(){
    	
    	
    	batch = new SpriteBatch();
		img = new Sprite(new Texture("startBackground.jpg"));
		startButton = new ImageButton(new SpriteDrawable(new Sprite(new Texture("start.jpg"))), new SpriteDrawable(new Sprite(new Texture("start1.jpg"))));
		stage = new Stage();
		exitButton = new ImageButton(new SpriteDrawable(new Sprite(new Texture("exit.jpg"))), new SpriteDrawable(new Sprite(new Texture("exit1.jpg"))));
        
		startButton.setHeight(70);
		startButton.setWidth(130);
		startButton.setPosition(445, 150);
		exitButton.setHeight(70);
		exitButton.setWidth(130);
		exitButton.setPosition(445, 50);
		
		stage.addActor(startButton);
		stage.addActor(exitButton);
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

        camera.update();
        game.batch.setProjectionMatrix(camera.combined);
        
        stage.draw();
        


        if (Gdx.input.isTouched()) {
            game.setScreen(new Bed(game));
            dispose();
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

	}

}
