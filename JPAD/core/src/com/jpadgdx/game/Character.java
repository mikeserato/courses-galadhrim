package com.jpadgdx.game;

public class Character{
	
	private int health;
	private int intel;
	private int luck;
	private int score;
	public String name;
	int timeLeft;
	
	Character(int health, int intel, int luck, String name){
		this.health = health;
		this.intel = intel;
		this.luck = luck;
		this.name = name;
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
