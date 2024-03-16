using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Enviroment : MonoBehaviour
{
    [SerializeField] Vector3 endPos;
    [SerializeField] Vector3 stPos;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        StartCoroutine(IEMove());
    }

    private IEnumerator IEMove()
    {
        while (true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.position, endPos, gameManager.speed * Time.deltaTime);

            yield return null;

            if(Vector3.Distance(transform.position, endPos) < 1)
            {
                Respawn();
                yield break;
            }
        }
    }

    private void Respawn()
    {
        StopAllCoroutines();
        transform.localPosition = stPos;
        StartCoroutine(IEMove());
    }
}