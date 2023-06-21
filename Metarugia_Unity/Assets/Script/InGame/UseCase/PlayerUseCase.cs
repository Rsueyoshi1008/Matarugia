using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InGame.Model.Player;
using Datas.DataRepository;
public class PlayerUseCase : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;// 接触判定を取るオブジェクト
    //[SerializeField] private GameObject _jumpingCheck;
    private DataRepository _repository;
    private PlayerModel _model;
    public UnityAction<PlayerModel> ChangeModel;
    private Rigidbody rb;
    private float rotationSpeed = 0.1f;
    private float _groundDistance = 0.1f;// 地面との判定距離
    //private float _jumpingDistance = 0.1f;// 飛んでる最中の衝突判定距離
    private bool _isGrounded, _isJumped, _isJumping;
    public void Initialize(DataRepository repository)
    {
        _repository = repository;
        _model = new PlayerModel();
        rb = GetComponent<Rigidbody>();
    }
    public void SyncModel()
    {
        var player = _repository.player;

        _model.Speed = player.Speed;
        _model.Attack = player.Attack;
        _model.Defense = player.Defense;
        _model.HP = player.HP;
        _model.JumpPower = player.JumpPower;

        ChangeModel?.Invoke(_model);
    }

    void Update()
    {
        float horizontalInput = 0.0f, verticalInput = 0.0f;
        // キャラクターのレイヤーマスクを作成
        int layerMask = ~LayerMask.GetMask("Player");
        // レイキャストを使って地面との接触を検出
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, layerMask);
        //_animator.SetBool("Grounded", _isGrounded);
        
        if(_isGrounded == false)//   地面についたらジャンプフラグをオフ
        {
            _isJumping = true;
            
        }
        else
        {
            _isJumping = false;
        }
        
        
        //移動量の取得
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1.0f;
        }
        Debug.Log(_isGrounded);
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)//ジャンプ処理
        {
            Jump();
        }
        
        
        
        if(verticalInput != 0.0f || horizontalInput != 0.0f)//呼び続けるとLookRotation(movement)がゼロのログが出続けるから押したときに呼び出す
        {
            
            if(_isJumped && _isJumping)//   ジャンプ中にオブジェクトに当たったら移動できなくなる
            {
                return;
            }
            else
            {
                Move(verticalInput,horizontalInput);
            }
            
        }
        
        
    }
    public void Move(float verticalInput, float horizontalInput)
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 movement = cameraForward * verticalInput + Camera.main.transform.right * horizontalInput;
        //移動ベクトルを正規化する
        movement = movement.normalized;
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = movement * _model.Speed + new Vector3(0, rb.velocity.y, 0);
        // 入力方向に回転する
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * _model.JumpPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Stairs"))
    {
        _isJumped = true;
    }
}
}
