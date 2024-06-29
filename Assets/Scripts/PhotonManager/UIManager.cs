using UnityEngine;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("Cardを配る")] private CardGenerator generater;
    
    /// <summary>GameがStartしたときhandを配る</summary>
    public void DrawCard()
    {
        for (var i = 0; i < 6; i++)
        {
            //Masterがカードをドロー
            if (PhotonNetwork.IsMasterClient)
            {
                generater.MasterCardSpawn(i,true); //Cardを配る
                generater.MasterCardSpawn(i,false);
            }
            //Guestがカードをドロー
            else
            {
                generater.GuestCardSpawn(i, true);
                generater.GuestCardSpawn(i, false);
            }
        }

        generater.ResetPosition(); //手札を整える
    }
}