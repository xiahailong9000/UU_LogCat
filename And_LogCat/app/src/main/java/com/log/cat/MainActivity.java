package com.log.cat;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.log.unity.CrashHandler;


public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        CrashHandler crashHandler = CrashHandler.getInstance();
        crashHandler.init(this);
    }
}
