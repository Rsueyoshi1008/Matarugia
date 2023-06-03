using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePresenterManager : MonoBehaviour
{
    [SerializeField] private PlayerPresenter playerPresenter;
    [SerializeField] public GameManager gameManager;
    void Start()
    {
        playerPresenter.Initialize(gameManager);
    }

    
}
