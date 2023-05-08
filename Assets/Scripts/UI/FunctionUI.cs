using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour {
    public void LaunchScene(string nameScene) {
        SceneManager.LoadScene(nameScene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
