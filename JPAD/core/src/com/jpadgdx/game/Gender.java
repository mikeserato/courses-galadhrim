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

public class Gender implements Screen {
	
		final StartLife game;

	    OrthographicCamera camera;
		ImageButton male;
		ImageButton female;
		Stage stage;
		Texture player;
		int turn = 1;
	    
	    public Gender(final StartLife gam) {
	    	
	        game = gam;
	        
	        camera = new OrthographicCamera();
	        camera.setToOrtho(false, 1024, 512);
	        
	        create();

	    }

	    public void create(){
	    	
	    	player = new Texture(Gdx.files.internal("character creation/player1.png"));

			male = new ImageButton(new SpriteDrawable(new Sprite(new Texture("character creation/male.png"))), new SpriteDrawable(new Sprite(new Texture("character creation/male2.png"))));
			female = new ImageButton(new SpriteDrawable(new Sprite(new Texture("character creation/female.png"))), new SpriteDrawable(new Sprite(new Texture("character creation/female2.png"))));
			stage = new Stage();
			
			male.setHeight(200);
			male.setWidth(200);
			male.setPosition(300, 150);
			female.setHeight(200);
			female.setWidth(200);
			female.setPosition(500, 150);
			
			male.addListener(new ClickListener(){
				@Override
				public void clicked(InputEvent event, float x, float y){
					if(turn == 1){
						player = new Texture(Gdx.files.internal("character creation/player2.png"));
						turn++;
					}
					else{
						game.setScreen(new Personality(game));
			            dispose();
					}
				}
			});
			
			female.addListener(new ClickListener(){
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
			
			stage.addActor(male);
			stage.addActor(female);
			
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
			stage.getBatch().draw(new Texture("character creation/bgMF.png"), 0, 0);
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
