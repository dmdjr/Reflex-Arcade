using UnityEngine;

/* ======================================================================
 * [Class]: PersistentSingleton<T>
 * [Role] : 씬이 전환되어도 유지되는 전역 싱글톤 베이스 클래스
 *          AppManager, DataManager, SoundManager, AdManager 등에서 사용
 * ====================================================================== */
public abstract class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            // 자식 오브젝트에서 호출 시에도 동작하도록 루트 오브젝트에 적용
            // (@Managers 같은 부모 오브젝트 아래 묶어도 정상 작동)
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
