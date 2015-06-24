package com.mygdx.game.desktop;

import com.badlogic.gdx.backends.lwjgl.LwjglApplication;
import com.badlogic.gdx.backends.lwjgl.LwjglApplicationConfiguration;
import com.mygdx.game.MyGdxGame;
import com.mygdx.game.screens.Play;


public class DesktopLauncher {
	public static void main (String[] arg) {
		LwjglApplicationConfiguration config = new LwjglApplicationConfiguration();
		config.title = "PAD";
		config.useGL30 = true;
		config.width = Play.V_WIDTH;
		config.height = Play.V_HEIGHT;
		config.resizable = false;
		new LwjglApplication(new MyGdxGame(), config);
	}
}
