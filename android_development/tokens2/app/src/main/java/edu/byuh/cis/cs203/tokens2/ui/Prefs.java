package edu.byuh.cis.cs203.tokens2.ui;

import android.content.Context;
import android.os.Bundle;
import android.preference.PreferenceActivity;
import android.preference.PreferenceManager;

import edu.byuh.cis.cs203.tokens2.R;

/**
 * Created by tiffannie on 2/8/2017.
 */

public class Prefs extends PreferenceActivity {

    /***
     * Constructor for creating the Preference Activity
     * @param b
     */
    @Override
    protected void onCreate(Bundle b) {
        super.onCreate(b);
        addPreferencesFromResource(R.xml.prefs);
    }

    /***
     * a simple "facade" method to hide the ugliness that is the Android Preferences API. like a getter method
     * @param context
     * @return
     */
    public static boolean musicOn(Context context) {
        return PreferenceManager.getDefaultSharedPreferences(context).getBoolean("music", true);
    }

    /***
     * This method is a facade method for obtaining the speed preference
     * @param c
     * @return
     */
    public static int getSpeed(Context c) {
        String speed = PreferenceManager.getDefaultSharedPreferences(c).getString("speed", "10");
        return Integer.parseInt(speed);
    }
}
