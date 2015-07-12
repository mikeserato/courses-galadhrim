package com.jpadgdx.game;

public class Event {
	
	private int changeHealth;
	private int changeLuck;
	private int changeIntel;
	private int probability;
	
	Event(int changeHealth, int changeLuck, int changeIntel, int probability){
		this.changeHealth = changeHealth;
		this.changeIntel = changeIntel;
		this.changeLuck = changeLuck;
		this.probability = probability;
	}
	
	public void actionPerformed(Character player){
		player.changeHealth(this.changeHealth);
		player.changeLuck(this.changeLuck);
		player.changeIntel(this.changeIntel);
	}

	public void trigger(Character player){
		//insert probability computation blah blah
		actionPerformed(player);
	}
}
