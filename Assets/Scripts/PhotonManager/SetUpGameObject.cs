using Photon.Pun;
using UnityEngine;

public class SetUpGameObject : MonoBehaviour
{
    [SerializeField] private OnlineGameManager _onlineGameManager;
    [SerializeField,Header("キャラクターの監視クラス")] private CharacterObserver _characterObserver;
    
    [SerializeField,Header("Masterのアバター")] private GameObject _masterAvatar;　
    [SerializeField,Header("Guestのアバター")] private GameObject _guestAvatar;
    
    [SerializeField,Header("Cameraプレハブを入れる")] private GameObject _cameraPrefab;

    /// <summary>それぞれのクライアントにcameraを設置</summary>
    public void PhotonSetUpCamera()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //MasterCameraを生成
            var masterCamera = Instantiate(_cameraPrefab,_masterAvatar 
                .transform);
            masterCamera.transform.position = _masterAvatar.transform.position 
                                              + new Vector3(0.3f,4.5f,-4.6f);
            masterCamera.transform.rotation = Quaternion.Euler(34,0,0);
        }
        else
        {
            //EnemyCameraを生成
            var guestCamera = Instantiate(_cameraPrefab, _guestAvatar
                .transform);
            
            guestCamera.transform.position = _guestAvatar.transform.position 
                                             + new Vector3(-0.3f, 4.0f, 4.6f);
            guestCamera.transform.rotation = Quaternion.Euler(34,180,0);
        }
    }
}
