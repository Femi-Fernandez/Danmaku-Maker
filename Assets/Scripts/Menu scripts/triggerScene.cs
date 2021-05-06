using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScene : MonoBehaviour
{

    public SceneControl SC;
    public string sceneToLoad;

    public void triggerLoadScene()
    {
        StartCoroutine( SC.ExitScene(sceneToLoad));
    }
}
