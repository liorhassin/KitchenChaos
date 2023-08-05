using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader{

    public enum Scene{
        MainMenuScene,
        GameScene,
        LoadingScene,
    }
    
    private static Scene targetScene;
    
    public static void Load(Scene targetScene){
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    //When LoaderCallback is called this is the first update,
    //Meaning LoadingScene has been rendered.
    public static void LoaderCallback(){
        SceneManager.LoadScene(targetScene.ToString());
    }

}
