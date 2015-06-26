package com.jpadgdx.game.desktop;

import com.badlogic.gdx.backends.lwjgl.LwjglApplication;
import com.badlogic.gdx.backends.lwjgl.LwjglApplicationConfiguration;
import com.jpadgdx.game.Life;

public class DesktopLauncher {
	public static void main (String[] arg) {
		LwjglApplicationConfiguration config = new LwjglApplicationConfiguration();
		config.title = "Life";
		config.width = 1024;
		config.height = 512;
		config.resizable = false;
		new LwjglApplication(new Life(), config);
	}
}
