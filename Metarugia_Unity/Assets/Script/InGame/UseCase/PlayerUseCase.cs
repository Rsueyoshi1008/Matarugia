using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InGame.Model.Player;
using Datas.DataRepository;
public class PlayerUseCase : MonoBehaviour
{
    private DataRepository _repository;
    private PlayerModel _model;
    public UnityAction<PlayerModel> ChangeModel;
    public void Initialize(DataRepository repository)
    {
        _repository = repository;
        _model = new PlayerModel();
    }
    public void SyncModel()
    {
        var player = _repository.player;

        _model.Speed = player.Speed;
        _model.Attack = player.Attack;
        _model.Defense = player.Defense;
        _model.HP = player.HP;

        ChangeModel?.Invoke(_model);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
