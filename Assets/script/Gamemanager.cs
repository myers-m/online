using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public AsyncOperation pro;

    string waitname = "";

    private void Awake()
    {
        instance = this;
    }

   public void loadscene(string name) {
        this.waitname = name;
        SceneManager.LoadScene("wait");
    }

    public void waitload()
    {
        pro = SceneManager.LoadSceneAsync(waitname);
    }

    public void exit() {
        Application.Quit();
    }
}
