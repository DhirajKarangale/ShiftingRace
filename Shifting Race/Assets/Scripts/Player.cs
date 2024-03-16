using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem psChange;
    [SerializeField] Road roadNormal;
    [SerializeField] Road roadSpeedBrakers;
    [SerializeField] Road roadStoper;

    [SerializeField] Animator[] avatars;
    [SerializeField] float animDuration;

    private Road currRoad;
    private Animator currAvatar;
    private GameManager gameManager;

    private void Start()
    {
        currRoad = roadNormal;
        currAvatar = avatars[0];
        Animate();
        gameManager = GameManager.instance;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!gameManager.isGameStarted) return;

        if (collider.CompareTag("Finish"))
        {
            gameManager.GameOver();
            Stop();
        }
        else if (collider.CompareTag("RoadNormal"))
        {
            currRoad = roadNormal;
            UpdateSpeed();
        }
        else if (collider.CompareTag("RoadSpeedBreakers"))
        {
            currRoad = roadSpeedBrakers;
            UpdateSpeed();
        }
        else if (collider.CompareTag("RoadStopper"))
        {
            currRoad = roadStoper;
            UpdateSpeed();
        }
    }


    private IEnumerator IEAnimate(Transform item)
    {
        item.gameObject.SetActive(true);

        Vector3 stScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        float currTime = 0;

        yield return null;
        while (currTime < animDuration)
        {
            currTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(stScale, endScale, currTime / animDuration);
            yield return null;
        }
        transform.localScale = endScale;
        yield return null;
    }


    private void Stop()
    {
        currAvatar.Play("Idle");
    }

    private void UpdateSpeed()
    {
        if (currAvatar.name.Equals("Human")) gameManager.speed = currRoad.human;
        else if (currAvatar.name.Equals("Car")) gameManager.speed = currRoad.car;
        else if (currAvatar.name.Equals("Helecopter")) gameManager.speed = currRoad.helecopter;
    }

    internal void Animate()
    {
        currAvatar.Play("Play");
    }

    internal void Reset()
    {
        currRoad = roadNormal;
        ButtonAvatar(0);
    }


    public void ButtonAvatar(int avatar)
    {
        if (currAvatar == avatars[avatar]) return;

        currAvatar.gameObject.SetActive(false);
        currAvatar = avatars[avatar];
        StartCoroutine(IEAnimate(currAvatar.transform));
        Animate();
        UpdateSpeed();
        psChange.Play();
    }
}

[System.Serializable]
public class Road
{
    public float human;
    public float car;
    public float helecopter;
}