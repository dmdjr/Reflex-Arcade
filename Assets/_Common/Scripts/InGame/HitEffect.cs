using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public static HitEffect Instance;

    void Awake() => Instance = this;

    public void PlayHighlight(Transform target)
    {
        if (target == null) return;
        StartCoroutine(PulseRoutine(target));
    }

    IEnumerator PulseRoutine(Transform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = originalScale * 1.2f; 

        float speed = 3f; 
        float time = 0;

        while (true)
        {
            if (target == null) yield break;

            // PingPong은 시간이 계속 늘어나도 알아서 0 -> 1 -> 0 -> 1 ... 왕복
            float t = Mathf.PingPong(time * speed, 1); 
            
            target.localScale = Vector3.Lerp(originalScale, targetScale, t);
            
            // 게임오버라 TimeScale이 0일 테니 unscaledDeltaTime 사용
            time += Time.unscaledDeltaTime; 
            yield return null;
        }
    }
}
