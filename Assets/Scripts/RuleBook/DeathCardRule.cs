using UnityEngine;

public class DeathCardRule : MonoBehaviour
{
    public void GameLose()
    {
        //このカードが表側表示にされた場合ゲームに敗北する
        Debug.Log("負け");
    }
}
