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

public class Personality implements Screen {
	
		final StartLife game;

	    OrthographicCamera camera;
		ImageButton ath;
		ImageButton cha;
		ImageButton intel;
		Stage stage;
		Texture player;
		int turn = 1;
	    
	    public Personality(final StartLife gam) {
	    	
	        game = gam;
	        
	        camera = new OrthographicCamera();
	        camera.setToOrtho(false, 1024, 512);
	        
	        create();

	    }

	    public void create(){
	    	
	    	player = new Texture(Gdx.files.internal("character creation/player1.png"));

			ath = new ImageButton(new SpriteDrawable(new Sprite(new Texture("personality/athletic.jpg"))), new SpriteDrawable(new Sprite(new Texture("personality/athletic2.jpg"))));
			cha = new ImageButton(new SpriteDrawable(new Sprite(new Texture("personality/charismatic.jpg"))), new SpriteDrawable(new Sprite(new Texture("personality/charismatic2.jpg"))));
			intel = new ImageButton(new SpriteDrawable(new Sprite(new Texture("personality/intelligent.jpg"))), new SpriteDrawable(new Sprite(new Texture("personality/intelligent2.jpg"))));
			stage = new Stage();
			
			ath.setHeight(300);
			ath.setWidth(300);
			ath.setPosition(70, 100);
			cha.setHeight(300);
			cha.setWidth(300);
			cha.setPosition(370, 100);
			intel.setHeight(300);
			intel.setWidth(300);
			intel.setPosition(665, 100);
			
			ath.addListener(new ClickListener(){
				@Override
				public void clicked(InputEvent event, float x, float y){
					if(turn == 1){
						player = new Texture(Gdx.files.internal("character creation/player2.png"));
						turn++;
					}
					else{
						game.setScreen(new Intro(game));
			            dispose();
					}
				}
			});
			
			cha.addListener(new ClickListener(){
				@Override
				public void clicked(InputEvent event, float x, float y){
					if(turn==1){
						player = new Texture(Gdx.files.internal("character creation/player2.png"));
						turn++;
					}
					else{
						game.setScreen(new Intro(game));
			            dispose();
					}
				}
			});
			
			intel.addListener(new ClickListener(){
				@Override
				public void clicked(InputEvent event, float x, float y){
					if(turn==1){
						player = new Texture(Gdx.files.internal("character creation/player2.png"));
						turn++;
					}
					else{
						game.setScreen(new Intro(game));
			            dispose();
					}
				}
			});
			
			stage.addActor(ath);
			stage.addActor(cha);
			stage.addActor(intel);
			
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
			stage.getBatch().draw(new Texture("personality/background.png"), 0, 0);
			stage.getBatch().draw(player, 0, 0);
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
