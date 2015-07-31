package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
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
	public Character player;
	private Texture bg;
	private Rectangle dorm;
	private Rectangle physci;
	private Rectangle su; 
	private Rectangle hum;
	private Rectangle fpark;
	
	Texture pic;
	
	public  Life (final StartLife gam) {
		
		this.game = gam;
		
		opening = Gdx.audio.newMusic(Gdx.files.internal("audio/mapBG.mp3"));
		step = Gdx.audio.newSound(Gdx.files.internal("audio/step.mp3"));
		opening.setLooping(true);
		opening.play();
		
		bg = new Texture(Gdx.files.internal("maps/map.jpg"));
		
		camera = new OrthographicCamera();
		camera.setToOrtho(false, 1024, 512);
		
		player = new Character(10,10,10,"Player 1",true);
		
		dorm = new Rectangle();
		dorm.x = 35;
		dorm.y = 402;
		dorm.height = 98;
		dorm.width = 160;
		
		physci = new Rectangle();
		physci.x = 155;
		physci.y = 46;
		physci.height = 98;
		physci.width = 160;
		
		fpark = new Rectangle();
		fpark.x = 517;
		fpark.y = 252;
		fpark.height = 98;
		fpark.width = 160;
		
		hum = new Rectangle();
		hum.x = 835;
		hum.y = 97;
		hum.height = 98;
		hum.width = 160;
		
		su = new Rectangle();
		su.x = 765;
		su.y = 382;
		su.height = 98;
		su.width = 160;
				
		
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
		game.batch.draw(player.character, player.x, player.y);
		game.batch.end();
		
		
		if(Gdx.input.isKeyPressed(Keys.LEFT)){
			step.play();
			if(player.overlaps(dorm)){
				opening.stop();
				game.setScreen(new Bed(game));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				game.setScreen(new Physci(game));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				game.setScreen(new SU(game));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				game.setScreen(new Hum(game));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				game.setScreen(new FPark(game));
				dispose();
			}
			else
				player.x -= 200 * Gdx.graphics.getDeltaTime();
			
		}
		if(Gdx.input.isKeyPressed(Keys.RIGHT)){
			if(player.overlaps(dorm)){
				opening.stop();
				game.setScreen(new Bed(game));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				game.setScreen(new Physci(game));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				game.setScreen(new SU(game));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				game.setScreen(new Hum(game));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				game.setScreen(new FPark(game));
				dispose();
			}
				
			else
				player.x += 200 * Gdx.graphics.getDeltaTime();
		}
		
		if(Gdx.input.isKeyPressed(Keys.UP)){
			step.play();
			if(player.overlaps(dorm)){
				opening.stop();
				game.setScreen(new Bed(game));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				game.setScreen(new Physci(game));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				game.setScreen(new SU(game));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				game.setScreen(new Hum(game));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				game.setScreen(new FPark(game));
				dispose();
			}
			else
				player.y += 200 * Gdx.graphics.getDeltaTime();
			
		}
		
		if(Gdx.input.isKeyPressed(Keys.DOWN)){
			step.play();
			if(player.overlaps(dorm)){
				opening.stop();
				game.setScreen(new Bed(game));
				dispose();
			}
			else if(player.overlaps(physci)){
				opening.stop();
				game.setScreen(new Physci(game));
				dispose();
			}
			else if(player.overlaps(su)){
				opening.stop();
				game.setScreen(new SU(game));
				dispose();
			}
			else if(player.overlaps(hum)){
				opening.stop();
				game.setScreen(new Hum(game));
				dispose();
			}
			else if(player.overlaps(fpark)){
				opening.stop();
				game.setScreen(new FPark(game));
				dispose();
			}
			else
				player.y -= 200 * Gdx.graphics.getDeltaTime();
			
			
		}
		
		if(player.x < 20) player.x = 20;
	    if(player.x > 1004 - 32) player.x = 1004 - 32;
	    if(player.y < 20) player.y = 20;
	    if(player.y > 492 - 32) player.y = 492 - 32;
	    
	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub
		opening.play();
	}
		
	
}
