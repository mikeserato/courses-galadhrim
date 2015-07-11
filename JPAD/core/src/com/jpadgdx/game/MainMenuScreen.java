package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.scenes.scene2d.InputEvent;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.ImageButton;
import com.badlogic.gdx.scenes.scene2d.utils.ClickListener;
import com.badlogic.gdx.scenes.scene2d.utils.SpriteDrawable;

public class MainMenuScreen implements Screen {
	
	final StartLife game;

    OrthographicCamera camera;
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

		startButton = new ImageButton(new SpriteDrawable(new Sprite(new Texture("start.jpg"))), new SpriteDrawable(new Sprite(new Texture("start1.jpg"))));
		exitButton = new ImageButton(new SpriteDrawable(new Sprite(new Texture("exit.jpg"))), new SpriteDrawable(new Sprite(new Texture("exit1.jpg"))));
		stage = new Stage();
		
		startButton.setHeight(70);
		startButton.setWidth(130);
		startButton.setPosition(445, 150);
		exitButton.setHeight(70);
		exitButton.setWidth(130);
		exitButton.setPosition(445, 50);
		
		startButton.addListener(new ClickListener(){
			@Override
			public void clicked(InputEvent event, float x, float y){
				game.setScreen(new Bed(game));
	            dispose();
			}
		});
		
		exitButton.addListener(new ClickListener(){
			@Override
			public void clicked(InputEvent event, float x, float y){
				Gdx.app.exit();
			}
		});
		
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
        
        stage.getBatch().begin();
		stage.getBatch().draw(new Texture("startBackground.jpg"), 0, 0);
		stage.getBatch().end();
        
		stage.act();
        stage.draw();
        
        Gdx.input.setInputProcessor(stage);

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
		stage.dispose();
	}

}
