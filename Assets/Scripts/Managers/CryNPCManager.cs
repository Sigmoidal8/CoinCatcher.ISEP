using System.Collections;
using UnityEngine;

public class CryNPCManager : MonoBehaviour
{
    public AudioSource crying;
    public SceneController sceneController;

    private bool isCrying = false;
    private float secondsBetweenCrying = 10f;

    void Start()
    {
        StartCoroutine(CryLoop());
    }

    IEnumerator CryLoop()
    {
        while (true)
        {
            if (!isCrying && !sceneController.IsSceneDilemaCompleted())
            {
                isCrying = true;
                crying.Play();
                yield return new WaitForSeconds(crying.clip.length + secondsBetweenCrying);
                isCrying = false;
            }
            yield return null;
        }
    }
}