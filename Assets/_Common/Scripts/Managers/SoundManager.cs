using UnityEngine;

/* ======================================================================
 * [Class]: SoundManager (Singleton)
 * [Role] : 각 게임의 점수 획득 사운드를 재생하는 오디오 매니저 클래스
 * ====================================================================== */
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scoreClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    public void PlayScoreSound()
    {
        if (audioSource != null && scoreClip != null)
        {
            audioSource.PlayOneShot(scoreClip);
        }
    }
}
