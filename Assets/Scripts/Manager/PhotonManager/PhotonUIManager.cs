using Photon.Pun;
using UnityEngine;

public class PhotonUIManager : MonoBehaviourPunCallbacks
{
    public static PhotonUIManager Instance { get; private set; }
    
    // 各オブジェクトの間隔
    [SerializeField,Header("行")] private float rowSpacing = 2.0f;
    [SerializeField,Header("列")] private float columnSpacing = 2.0f;
    [SerializeField,Header("Playerからの距離")] private float frontOffset = 2.0f; // Playerの前に配置するためのオフセット
    
    //FieldのPosition
    private Vector3 _firstPlayerFieldPosition = new();
    private Vector3 _secondPlayerFieldPosition = new();
    
    // Playerの向き
    private Vector3 _forwardDirection;
    private Vector3 _rightDirection;

    private GameObject[] _readerField = new GameObject[1];

    private void Start()
    {
        //シングルトン化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    //Characterを置くためのFieldを生成
    public void SetUpGameField()
    {
        //マスタークライアントのみがFieldを生成する
        if (PhotonNetwork.IsMasterClient)
        {
            // Playerの前にオブジェクトを配置
            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    // 各オブジェクトの位置を計算
                    var offset = _forwardDirection * (row * rowSpacing + frontOffset) + _rightDirection * (col - 1) * columnSpacing;
                    var objectPosition = _firstPlayerFieldPosition + offset;
                    // オブジェクトをインスタンス化
                    PhotonNetwork.Instantiate(row > 2 ? "PlayerField" : "EnemyField", objectPosition,
                        Quaternion.identity);
                }
            }
        }
    }

    //Readerを生成
    public void SetUpReader()
    {
        var firstPlayerPosition = new Vector3(0f, 0f,0f); //初期位置の指定
        var secondPlayerPosition = new Vector3(0, 0, 10); //Player2の初期位置を指定
        
        //playerFieldをインスタンス化してその位置を取得する
        var firstPlayer = PhotonNetwork.Instantiate("PlayerField", firstPlayerPosition, Quaternion.identity);
        var secondPlayer = PhotonNetwork.Instantiate("EnemyField", secondPlayerPosition, Quaternion.identity);
        
        //FieldのPositionを取得
        _firstPlayerFieldPosition = firstPlayer.transform.position;
        _secondPlayerFieldPosition = secondPlayer.transform.position;
        
        //Avatarの位置をFieldの上に設定してインスタンス化
        var avatarPosition = _firstPlayerFieldPosition;
        var enemyAvatarPosition = _secondPlayerFieldPosition;
        
        avatarPosition.y += 0.5f;
        enemyAvatarPosition.y = 0.5f;
        
        var playerAvatar = PhotonNetwork.Instantiate("Avatar", avatarPosition, Quaternion.identity);
        PhotonNetwork.Instantiate("Avatar", enemyAvatarPosition, Quaternion.identity);
        
        // Playerの向きを取得
        _forwardDirection = playerAvatar.transform.forward;
        _rightDirection = playerAvatar.transform.right;
    }
}
