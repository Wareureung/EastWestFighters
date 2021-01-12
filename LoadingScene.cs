using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    NewGameManager GM;

    string map_str;
    string ice_map = "Cube_Test";
    string space_map = "SpaceMap";
    string pirates_map = "PirateMap";
    string forest_map = "Jump_Test";

    Image progressBar;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<NewGameManager>();
        progressBar = GameObject.Find("progressbar").GetComponent<Image>();

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        if (GM.map_select_number == 0)
            map_str = ice_map;
        if (GM.map_select_number == 1)
            map_str = pirates_map;
        if (GM.map_select_number == 2)
            map_str = forest_map;
        if (GM.map_select_number == 3)
            map_str = space_map;

        AsyncOperation op = SceneManager.LoadSceneAsync(map_str);

        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if (progressBar.fillAmount >= op.progress)

                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)

                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
