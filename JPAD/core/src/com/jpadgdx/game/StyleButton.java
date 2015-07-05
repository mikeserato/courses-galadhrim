package com.jpadgdx.game;

import com.badlogic.gdx.scenes.scene2d.ui.ImageButton.ImageButtonStyle;
import com.badlogic.gdx.scenes.scene2d.utils.Drawable;



public class StyleButton extends ImageButtonStyle{
	public Drawable imageOver;
	public Drawable imageUp;
	public Drawable imageDown;
	
	public StyleButton(Drawable imageUp, Drawable imageDown, Drawable imageOver){
		this.imageUp = imageUp;
		this.imageDown = imageDown;
		this.imageOver = imageOver;
		
	}
}