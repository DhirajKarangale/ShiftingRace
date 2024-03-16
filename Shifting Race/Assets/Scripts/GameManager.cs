using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("UI")]
    [SerializeField] GameObject objGame;
    [SerializeField] GameObject objStart;
    [SerializeField] GameObject objGameOver;

    [Header("Refrences")]
    [SerializeField] internal ObjectPooler objectPooler;
    [SerializeField] RoadSpawner roadSpawner;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem psGameOver;

    internal int currLevel;
    internal bool isGameStarted;
    public float speed;


    private void Start()
    {
        isGameStarted = false;
        currLevel = 0;
    }

    internal void GameOver()
    {
        isGameStarted = false;
        objGame.SetActive(false);
        objStart.SetActive(false);
        objGameOver.SetActive(true);

        psGameOver.Play();

        currLevel = (currLevel + 1) % 3;
        speed = 0;
    }

    public void ButtonContinue()
    {
        isGameStarted = false;
        objGame.SetActive(false);
        objStart.SetActive(true);
        objGameOver.SetActive(false);

        roadSpawner.Reset();
        psGameOver.Stop();
        player.Animate();
    }

    public void ButtonPlay()
    {
        isGameStarted = true;
        objGame.SetActive(true);
        objStart.SetActive(false);
        objGameOver.SetActive(false);
        psGameOver.Stop();
        player.Reset();
        speed = 12;
    }
}