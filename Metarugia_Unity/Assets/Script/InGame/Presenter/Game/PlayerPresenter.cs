using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : GamePresenterManager
{
    [SerializeField] private PlayerUseCase _useCase;
    [SerializeField] private PlayerView _view;
    public void Initialize(GameManager gameManager)
    {
        _useCase.Initialize(gameManager.GetDataRepository());

        _useCase.SyncModel();
    }
}
