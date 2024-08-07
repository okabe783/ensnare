// using System.Collections.Generic;
// using Photon.Pun;
// using UnityEngine;
//
// public class GameObjectManager : MonoBehaviourPunCallbacks
// {
//     [SerializeField] private OnlineGameManager _onlineGameManager;
//
//     [SerializeField] private CharacterObserver _characterObserver;
//     // 各オブジェクトの間隔
//     [SerializeField, Header("行")] private float _rowSpacing = 2.0f;
//     [SerializeField, Header("列")] private float _columnSpacing = 2.0f;
//
//     [SerializeField, Header("Playerからの距離")] private float _frontOffset = 2.0f; // Playerの前に配置するためのオフセット
//     //FieldのPosition
//     private Vector3 _firstPlayerFieldPosition;
//     private Vector3 _secondPlayerFieldPosition;
//
//     // Masterの向き
//     private Vector3 _masterForwardDirection; //前向きの方向
//     private Vector3 _masterRightDirection;　//右向きの方向
//
//     //Guestの向き
//     private Vector3 _guestForwardDirection; //前向きの方向
//     private Vector3 _guestRightDirection;　//右向きの方向
//
//     //Characterに関する変数
//     [SerializeField] private string[] _masterCharacter;
//     [SerializeField] private string[] _guestCharacter;
//
//     [SerializeField] private GameObject _cameraPrefab;
//
//     public List<ClickSelectCharacter> MasterCharacterList { get; } = new();
//     public List<ClickSelectCharacter> GuestCharacterList { get; } = new();
//
//     public GameObject Master { get; private set; }
//     public GameObject Guest { get; private set; }
//     
//     private void Start()
//     {
//         _characterObserver.CharacterInitialize(OnBindStateChanged);
//     }
//
//     private void OnBindStateChanged(bool isBind)
//     {
//         Debug.Log(isBind);
//         if (isBind)
//         {
//             DisableCharacterSelection(true);
//         }
//     }
//     //Master用Characterを置くためのFieldを生成
//     public void PhotonSetUpGameField()
//     {
//         var characterIndex = 0;
//         //マスタークライアントのみがFieldを生成する
//         if (PhotonNetwork.IsMasterClient)
//         {
//             // Playerの前にオブジェクトを配置
//             for (var row = 0; row < 4; row++)
//             {
//                 for (var col = 0; col < 3; col++)
//                 {
//                     // 各オブジェクトの位置を計算
//                     var offset = _masterForwardDirection * (row * _rowSpacing + _frontOffset) +
//                                  _masterRightDirection * (col - 1) * _columnSpacing;
//                     var objectPosition = _firstPlayerFieldPosition + offset;
//
//                     // オブジェクトをインスタンス化
//                     PhotonNetwork.Instantiate(row > 2 ? "PlayerField" : "EnemyField", objectPosition,
//                         Quaternion.identity);
//
//                     var avatarPosition = objectPosition;
//                     avatarPosition.y += 0.5f;
//
//                     if (characterIndex >= _masterCharacter.Length) continue;
//                     //Characterをインスタンス化
//                     var character = PhotonNetwork.Instantiate(_masterCharacter[characterIndex], avatarPosition, Quaternion.identity);
//                     MasterCharacterList.Add(character.GetComponent<ClickSelectCharacter>());
//                     characterIndex++;
//                 }
//             }
//         }
//         else
//         {
//             for (var row = 0; row < 4; row++)
//             {
//                 for (var col = 0; col < 3; col++)
//                 {
//                     var offset = -_guestForwardDirection * (row * _rowSpacing + _frontOffset) +
//                                  _guestRightDirection * (col - 1) * _columnSpacing;
//                     var secondPlayerObjectPosition = _secondPlayerFieldPosition + offset;
//
//                     var avatarPosition = secondPlayerObjectPosition;
//                     avatarPosition.y += 0.5f;
//                     if (characterIndex >= _guestCharacter.Length) continue;
//                     var character = PhotonNetwork.Instantiate(_guestCharacter[characterIndex], avatarPosition,
//                         Quaternion.Euler(0, 180, 0));
//                     GuestCharacterList.Add(character.GetComponent<ClickSelectCharacter>());
//                     characterIndex++;
//                 }
//             }
//         }
//     }
//
//     //Readerを生成
//     public void PhotonSetUpReader()
//     {
//         var firstPlayerPosition = new Vector3(0f, 0f, 0f); //初期位置の指定
//         var secondPlayerPosition = new Vector3(0, 0, 10); //Player2の初期位置を指定
//
//         if (PhotonNetwork.IsMasterClient)
//         {
//             //playerFieldをインスタンス化してその位置を取得する
//             var firstPlayer = PhotonNetwork.Instantiate("PlayerField", firstPlayerPosition, Quaternion.identity);
//             
//             //FieldのPositionを取得
//             _firstPlayerFieldPosition = firstPlayer.transform.position;
//
//             //Avatarの位置をFieldの上に設定してインスタンス化
//             var avatarPosition = _firstPlayerFieldPosition;
//             avatarPosition.y += 0.5f;
//             var masterAvatar = PhotonNetwork.Instantiate("Avatar", avatarPosition, Quaternion.identity);
//             Master = masterAvatar;
//
//             //Camera
//             var masterCamera = Instantiate(_cameraPrefab, masterAvatar.transform);
//             masterCamera.transform.position = masterAvatar.transform.position + new Vector3(0.3f,4.5f,-4.6f);
//             masterCamera.transform.rotation = Quaternion.Euler(34,0,0);
//             
//             // Playerの向きを取得
//             _masterForwardDirection = masterAvatar.transform.forward;
//             _masterRightDirection = masterAvatar.transform.right;
//         }
//         else
//         {
//             var secondPlayer = PhotonNetwork.Instantiate("EnemyField", secondPlayerPosition, Quaternion.identity);
//             _secondPlayerFieldPosition = secondPlayer.transform.position;
//
//             var guestAvatarPosition = _secondPlayerFieldPosition;
//             guestAvatarPosition.y = 0.5f;
//
//             var guestAvatar = PhotonNetwork.Instantiate("EnemyAvatar", guestAvatarPosition, Quaternion.Euler(0, 180, 0));
//             Guest = guestAvatar;
//             
//             //EnemyCamera
//             var guestCamera = Instantiate(_cameraPrefab, guestAvatar.transform);
//             var cameraPosition = new Vector3(-0.3f, 4.0f, 4.6f);
//             
//             guestCamera.transform.position = guestAvatar.transform.position + cameraPosition;
//             guestCamera.transform.rotation = Quaternion.Euler(34,180,0);
//
//             //Guestの向きを取得
//             _guestForwardDirection = -guestAvatar.transform.forward;
//             _guestRightDirection = -guestAvatar.transform.right;
//         }
//     }
//
//     //通知が送られてきたらこの関数を呼び出す
//     private void DisableCharacterSelection(bool isBind)
//     {
//         var playerList = PhotonNetwork.IsMasterClient ? MasterCharacterList : GuestCharacterList;
//         if (!isBind) return;
//         Debug.Log(playerList.Count);
//         for (var i = 3; i < playerList.Count && i < 6; i++)
//         {
//             Debug.Log(playerList[i]);
//             // クリック不可にする
//             playerList[i].GetComponent<Collider>().enabled = false;
//             
//             var collider = playerList[i].GetComponent<Collider>();
//             if (collider != null)
//             {
//                 collider.enabled = false;
//                 Debug.Log("Colliderを無効にしました");
//             }
//             else
//             {
//                 Debug.LogWarning("Colliderが見つかりませんでした");
//             }
//         }
//     }
// }