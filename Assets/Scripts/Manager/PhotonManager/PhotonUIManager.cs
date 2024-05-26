using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonUIManager : MonoBehaviourPunCallbacks
{
    public static PhotonUIManager Instance { get; private set; }
    
    // 各オブジェクトの間隔
    [SerializeField,Header("行")] private float rowSpacing = 2.0f;
    [SerializeField,Header("列")] private float columnSpacing = 2.0f;
    
    [SerializeField,Header("Playerからの距離")] private float frontOffset = 2.0f; // Playerの前に配置するためのオフセット

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

    public void SetUpPlayerUI()
    {
        var firstPosition = new Vector3(0f, 0f,0f); //初期位置の指定
        //playerFieldをインスタンス化してその位置を取得する
        var playerField = PhotonNetwork.Instantiate("PlayerField", firstPosition, Quaternion.identity);
        var playerFieldPosition = playerField.transform.position;
        //Playerの位置をFieldの上に設定してインスタンス化
        var avatarPosition = playerFieldPosition;
        avatarPosition.y += 0.5f;
        var player = PhotonNetwork.Instantiate("Avatar", avatarPosition, Quaternion.identity);
        // Playerの向きを取得
        var forwardDirection = player.transform.forward;
        var rightDirection = player.transform.right;
        

        // Playerの前にオブジェクトを配置
        for (var row = 0; row < 2; row++)
        {
            for (var col = 0; col < 3; col++)
            {
                // 各オブジェクトの位置を計算
                var offset = forwardDirection * (row * rowSpacing + frontOffset) + rightDirection * (col - 1) * columnSpacing;
                var objectPosition = playerFieldPosition + offset;

                // オブジェクトをインスタンス化
                PhotonNetwork.Instantiate("PlayerField", objectPosition, Quaternion.identity);
            }
        }
    }

    public void SetUpEnemyUI()
    {
        
    }
}
