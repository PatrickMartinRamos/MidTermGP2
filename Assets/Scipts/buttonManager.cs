using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    private enemySpawner enemySpawner;
    private Player player;
    private scoreManager scoremanager;
    private UIscript uiscript;

    private void Start()
    {
        enemySpawner = FindObjectOfType<enemySpawner>();
        player = FindObjectOfType<Player>();
        scoremanager = FindAnyObjectByType<scoreManager>();
        uiscript = FindAnyObjectByType<UIscript>();

    }

    public void restartGame()
    {
        enemySpawner.RestartSpawning();
        uiscript.CloseUI();
        scoremanager.ResetScore();
    }
}
