using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneControl : MonoBehaviour
{
    Button enterEditorWithBoss;
    Button enterEditor;
    string[] lines;

    public UnityEngine.UI.Image sceneTransition;
    //Color colorSet = new Color(31, 10, 10, 255);


    // Start is called before the first frame update
    void Start()
    {

        enterEditorWithBoss = GameObject.Find("Load boss button").GetComponent<Button>();

        enterEditor = GameObject.Find("Enter editor button").GetComponent<Button>();


        enterEditorWithBoss.onClick.AddListener(delegate
        {
            openEditorSceneWithBoss();
        });

        enterEditor.onClick.AddListener(delegate
        {
            openEditorScene();
        });

    }

    void openEditorScene()
    {
        StartCoroutine(ExitScene("editor"));
    }

    void openEditorSceneWithBoss()
    {

#if UNITY_STANDALONE
        string[] readFileBoxBuild = StandaloneFileBrowser.OpenFilePanel("Load a boss", "", "txt", false);

#endif        
        if (readFileBoxBuild.Length > 0)
        {
            StaticFilePath.filePath = readFileBoxBuild;
            StartCoroutine(ExitScene("editor"));
        }
    }

    public IEnumerator EnterScene()
    {
        float timePassed = 0;
        float t = timePassed / 2f;

        t = t * t * (3f - 2f * t);
        sceneTransition.fillClockwise = false;
        while (timePassed < 2f)
        {
            t = timePassed / 2f;
            sceneTransition.fillAmount = Mathf.Lerp(1, 0, t);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ExitScene(string sceneToOpen)
    {
        float timePassed = 0;
        float t = timePassed / 2f;
        t = t * t * (3f - 2f * t);
        sceneTransition.fillClockwise = true;
        while (timePassed < 2.01f)
        {
            t = timePassed / 2f;
            sceneTransition.fillAmount = Mathf.SmoothStep(0, 1, t);
            timePassed += Time.deltaTime;

            if (timePassed >= 2f)
            {
                switch (sceneToOpen)
                {
                    case "editor":
                        loadEditorScene();
                        break;

                    case "main menu":
                        loadMenuScene();
                        break;
                    case "tut1":
                        loadTut1Scene();
                        break;
                    case "tut2":
                        loadTut2Scene();
                        break;
                    case "tut3":
                        loadTut3Scene();
                        break;
                    default:
                        break;
                }
                
            }
            yield return null;
        }
    }

    void loadEditorScene()
    {
        SceneManager.LoadScene("editor");
    }

    void loadMenuScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    void loadTut1Scene()
    {
        SceneManager.LoadScene("Turret_panel_tutorial");
    }
    void loadTut2Scene()
    {
        SceneManager.LoadScene("bullet_panel_tutorial");
    }
    void loadTut3Scene()
    {
        SceneManager.LoadScene("Phase_panel_tutorial");
    }
}


