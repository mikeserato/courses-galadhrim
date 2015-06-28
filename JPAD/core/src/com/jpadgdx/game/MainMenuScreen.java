package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;

public class MainMenuScreen implements Screen {
	
	final StartLife game;

    OrthographicCamera camera;
    
    public MainMenuScreen(final StartLife gam) {
    	
    	batch = new SpriteBatch();
    	float scrw = 1024; float scrh = 640;
    	
        game = gam;

        camera = new OrthographicCamera();
        camera.setToOrtho(false, 1024, 640);
        
        camera.viewportHeight = scrh;
        camera.viewportWidth = scrw;
        camera.position.set(camera.viewportWidth * .5f, camera.viewportHeight * .5f, 0f);
        camera.update();
        
        texture = new Texture("startBackground.png");
        
        sprite = new Sprite(texture);
        sprite.setSize(1024, 640);

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

        game.batch.begin();
        sprite.draw(batch);
        game.font.draw(game.batch, "Temporary Main Screen", 300,350);
        game.font.draw(game.batch, "Click anywhere to begin", 300, 300);
        game.batch.end();

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

