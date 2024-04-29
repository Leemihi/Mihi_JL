using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [Header("페이드 시간")]
    [SerializeField]
    private float fadeDuration = 1f;
    [Header("이미지가 출력되는 시간")]
    private float displayImageDuration;
    [Header("플레이어 게임 오브젝트")]
    [SerializeField]
    private GameObject player;
    [Header("성공 캔버스 그룹")]
    [SerializeField]
    private CanvasGroup exitBackgroundImageGroup = null;
    [Header("실패 캔버스 그룹")]
    [SerializeField]
    private CanvasGroup caughtBackgroundImageGroup = null;

    private bool isPlayerExit = false;
    private bool isPlayerCaught = false;

    private float timer = 0f;

    private void Update()
    {
        if (isPlayerExit == true)
        {
            EndLevel(exitBackgroundImageGroup, false);
        }

        else if(isPlayerCaught == true) 
        {
            EndLevel(caughtBackgroundImageGroup);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // .Equals : == 와 같으나 성능이 더 좋음
        if (other.gameObject.Equals(player))
        {
            isPlayerExit = true;
        }
    }

    private void EndLevel(CanvasGroup imageGroup, bool doRestart = true)
    {
        timer += Time.deltaTime;

        imageGroup.alpha = timer / fadeDuration;

        if(timer > fadeDuration + displayImageDuration)
        {
            if (doRestart == true)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

}
