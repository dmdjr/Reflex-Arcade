using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/* ======================================================================
/* 게임 오버 시 결과 화면(UI) 표시 및 씬 이동(재시작, 로비) 버튼 입력을 처리하는 클래스
 * * [Method Summary]
 * 1. ShowResult     : 게임 종료 후 전달받은 현재 점수와 최고 점수를 UI 텍스트에 반영
 * 2. OnClickRestart : (버튼) 정지된 시간을 다시 흐르게 하고(TimeScale 1), 현재 씬을 재로드
 * 3. OnClickHome    : (버튼) 정지된 시간을 다시 흐르게 하고, 로비(Lobby) 씬으로 이동
/* ====================================================================== */

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [SerializeField] private GameObject inGameScorePanel; // 상단 점수 UI (ScoreBar) 연결용

    public void ShowResult(int currentScore, int bestScore, bool isNewRecord)
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "Score " + currentScore.ToString();
        }
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best " + bestScore.ToString();

            if (isNewRecord)
            {
                StartCoroutine(PulseRoutine(bestScoreText.transform));
            }
            else
            {
                bestScoreText.transform.localScale = Vector3.one; // 크기 초기화
            }
        }

        // 인게임 점수판 비활성화
        if (inGameScorePanel != null)
        {
            inGameScorePanel.SetActive(false);
        }
    }
    IEnumerator PulseRoutine(Transform target)
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = originalScale * 1.2f; 

        float speed = 5f; 
        float time = 0f;

        while (true)
        {
            if (target == null || !gameObject.activeSelf) yield break;

            float t = Mathf.PingPong(time * speed, 1f);

            target.localScale = Vector3.Lerp(originalScale, targetScale, t);

            time += Time.unscaledDeltaTime;
            yield return null;
        }
    }
    public void OnClickRestart()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickHome()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
}