package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.Input.Keys;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.MathUtils;

public class TurnStart implements Screen {
	
	int player1Turn;
	int player2Turn;
	String turn;
	int day;
	final StartLife game;
	private OrthographicCamera camera;
	private int randomNumber;
	private Sound step;
	
	String rand;
	String dayString;
	
	public TurnStart(final StartLife gam){
		this.game = gam;
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
		turn = "Player 1";
		day = 1;
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
        if(day == 1)
        	dayString = "Monday";
        if(day == 2)
        	dayString = "Tuesday";
        if(day == 3)
        	dayString = "Wednesday";
        if(day == 4)
        	dayString = "Thursday";
        if(day == 5)
        	dayString = "Friday";
        if(day == 6)
        	dayString = "Saturday";
        if(day == 7)
        	dayString = "Sunday";
        
	    	randomNumber = MathUtils.random(10,20);
	        rand = Integer.toString(randomNumber);
	        game.batch.begin();
	        step.play();
	        game.font.draw(game.batch,dayString , 970, 500);
	        game.font.draw(game.batch,turn, 10, 500);
	        game.font.draw(game.batch, "Press ENTER to stop", 490, 300);
	        game.font.draw(game.batch, rand, 512, 256);
	        
	        if(Gdx.input.isKeyPressed(Keys.ENTER)){
	        	try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
	        	game.setScreen(new Bed(game));
	            dispose();
	        }
	        game.batch.end();
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
