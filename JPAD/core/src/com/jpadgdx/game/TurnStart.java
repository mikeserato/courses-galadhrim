package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.Input.Keys;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.math.MathUtils;

public class TurnStart implements Screen {
	
	static int turn = 1;
	static int day = 1;
	final StartLife game;
	private OrthographicCamera camera;
	private int randomNumber;
	private Sound step;
	boolean pressed = false;
	String rand;
	String dayString;
	String player;
	Character p1;
	Character p2;
	
	public TurnStart(final StartLife gam, Character p1, Character p2){
		
		this.p1 = p1;
		this.p2 = p2;
		
		this.game = gam;
		
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
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
        
        if(turn == 1)
        	player = "Player 1";
        if(turn == 2)
        	player = "Player 2";
        
        if(pressed){
        	if(turn == 1){
        		p1.timeLeft = randomNumber * 100;
        		try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
        		game.setScreen(new Bed(game,p1,p2));
	            dispose();
        	}
        	if(turn == 2){
        		p2.timeLeft = randomNumber * 100;
        		try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
        		game.setScreen(new Bed(game,p1,p2));
	            dispose();
        	}
        }
        	
	        randomNumber = MathUtils.random(10,20);
	        rand = Integer.toString(randomNumber);
	        game.batch.begin();
	        game.font.draw(game.batch,dayString , 930, 500);
	        game.font.draw(game.batch,player, 10, 500);
	        game.font.draw(game.batch, "Press ENTER to stop", 460, 300);
	        game.font.draw(game.batch, rand, 512, 256);
	        game.batch.end();
	        
	        if(Gdx.input.isKeyPressed(Keys.ENTER)){
		          	pressed = true;
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
