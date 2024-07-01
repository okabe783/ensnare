using UnityEngine;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("Cardを配る")] private CardGenerator _generator;

    /// <summary>GameがStartしたときhandを配る</summary>
    public void DrawCard()
    {
        for (var i = 0; i < 6; i++)
        {
            //Masterがカードをドロー
            if (PhotonNetwork.IsMasterClient)
            {
                _generator.MasterCardSpawn(i, true); //Cardを配る
                _generator.MasterCardSpawn(i, false);
            }
            //Guestがカードをドロー
            else
            {
                _generator.GuestCardSpawn(i, true);
                _generator.GuestCardSpawn(i, false);
            }
        }

        _generator.ResetPosition(); //手札を整える
    }
}