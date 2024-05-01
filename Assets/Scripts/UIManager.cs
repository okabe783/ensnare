using UnityEngine;

/// <summary>FieldにUIを配置</summary>
public class UIManager : MonoBehaviour
{
   public GameObject _leaderGameObject;
   public GameObject _objectToPlace;
   public GameObject[] _characterToPlace;
   public int _columCount = 2;　//行
   public int _rowCount = 3; //列
   public float _horizontalSpacing = 2;　//横の間隔
   public float _verticalSpacing = 2;　//縦の間隔
   public Vector3 _startPoint = Vector3.zero;　//配置の始点
   public float _objectOffsetY = 1.0f;
   
   private void Start()
   {
      CreatePlayerObject();
   }

   private void CreatePlayerObject()
   {
      var leaderDirection = _leaderGameObject.transform.forward;
      var currentPosition = _startPoint;
      var characterIndex = 0;

      //objectを配置
      for (var i = 0; i < _rowCount; i++)
      {
         for (var j = 0; j < _columCount; j++)
         {
            //objectを配置
            var battleFieldTransform = Instantiate(_objectToPlace, currentPosition, Quaternion.identity).transform;
            
            //characterを配置
            var characterPosition = battleFieldTransform.transform.position + Vector3.up * _objectOffsetY;
            var character = Instantiate(_characterToPlace[characterIndex], characterPosition, Quaternion.identity);
            character.transform.forward = leaderDirection;
            
            currentPosition.x += _horizontalSpacing;
            characterIndex = (characterIndex + 1) % _characterToPlace.Length; //次のcharacterのIndexを更新 
         }

         currentPosition.x = _startPoint.x;
         currentPosition.z += _verticalSpacing;
      }
   }
}
