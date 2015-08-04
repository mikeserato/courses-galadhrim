package com.jpadgdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.Rectangle;

public class Character extends Rectangle{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int health;
	private int intel;
	private int luck;
	private int score;
	int gender;
	public String name;
	int timeLeft;
	public Texture character;
	
	Character(int gender){
		this.gender = gender;
	}
	
	Character(int health, int intel, int luck, String name, int gender){
		
		if(gender == 1)
			character = new Texture(Gdx.files.internal("sprites/256.png"));
		else
			character = new Texture(Gdx.files.internal("sprites/256.png"));
		
		this.width = 128;
		this.height = 128;		
		this.x = 2;
		this.y = 2;
	
		
		this.health = health;
		this.intel = intel;
		this.luck = luck;
		this.name = name;
		this.gender = gender;
		
		
		
	}

	public void toMap(){
		
		if(gender == 1)
			character = new Texture(Gdx.files.internal("sprites/32.png"));
		else
			character = new Texture(Gdx.files.internal("sprites/32.png"));
		
		this.x = 500;
		this.y = 400;
		this.width = 32;
		this.height = 32;
		
	}
	
	public void toArea(){
		if(gender == 1)
			character = new Texture(Gdx.files.internal("sprites/256.png"));
		else
			character = new Texture(Gdx.files.internal("sprites/256.png"));
		
		this.width = 128;
		this.height = 128;		
		this.x = 2;
		this.y = 2;
	}
	
	public void changeHealth(int change){
		this.health += change;
	}
	
	public int returnHealth(){
		return this.health;
	}
	
	public void changeIntel(int change){
		this.intel += change;
	}
	
	public int returnIntel(){
		return this.intel;
	}
	
	public void changeLuck(int change){
		this.luck += change;
	}
	
	public int returnLucl(){
		return this.luck;
	}
	
	public void changeScore(int change){
		this.score += change;
	}
	
	public int returnScore(){
		return this.score;
	}
	
}
