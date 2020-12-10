using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB;
using UnityEngine.SceneManagement;

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
        StartCoroutine(ExitScene());
    }

    void openEditorSceneWithBoss()
    {

#if UNITY_STANDALONE
        string[] readFileBoxBuild = StandaloneFileBrowser.OpenFilePanel("Load a boss", "", "txt", false);

#endif        
        if (readFileBoxBuild.Length > 0) 
        {
            StaticFilePath.filePath = readFileBoxBuild;
            StartCoroutine(ExitScene());
        }
    }

    public IEnumerator EnterScene()
    {
     //   float timePassed = 0;
     //   float t = timePassed / 2f;
     //
     //   t = t * t * (3f - 2f * t);
     //   sceneTransition.fillClockwise = false;
     //   while (timePassed < 2f)
     //   {
     //       t = timePassed / 2f;
     //       sceneTransition.fillAmount = Mathf.Lerp(1, 0, t);
     //       timePassed += Time.deltaTime;
            yield return null;
     //   }
    }

    public IEnumerator ExitScene()
    {
      //  float timePassed = 0;
      //  float t = timePassed / 2f;
      //  t = t * t * (3f - 2f * t);
      //  sceneTransition.fillClockwise = true;
      //  while (timePassed < 2.01f)
      //  {
      //      t = timePassed / 2f;
      //      sceneTransition.fillAmount = Mathf.SmoothStep(0, 1, t);
      //      timePassed += Time.deltaTime;
      //
      //      if (timePassed >= 2f)
      //      {
                loadEditorScene();
      //      }
            yield return null;
      // }
    }
    
    void loadEditorScene()
    {
        SceneManager.LoadScene("editor");
    }
}


