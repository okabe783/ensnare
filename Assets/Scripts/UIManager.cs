using UnityEngine;

public class UIManager : MonoBehaviour
{
   public GameObject _objectToPlace;
   public int _rowCount = 3;
   public int _columCount = 4;
   public float _horizontalSpacing = 2;
   public float _verticalSpacing = 2;
   public Vector3 _startPoint = Vector3.zero;
   private void Start()
   {
      CreateObject();
   }

   private void CreateObject()
   {
      var currentPosition = _startPoint;

      for (var i = 0; i < _rowCount; i++)
      {
         for (var j = 0; j < _columCount; j++)
         {
            Instantiate(_objectToPlace, currentPosition, Quaternion.identity);

            currentPosition.x += _horizontalSpacing;
         }

         currentPosition.x = _startPoint.x;
         currentPosition.z += _verticalSpacing;
      }
   }
}
