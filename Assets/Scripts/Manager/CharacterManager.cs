using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>FieldにUIを配置</summary>
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }
    
    public GameObject _leaderGameObject;
    public GameObject _objectToPlace;
    public GameObject[] _characterToPlace;
    public List<ClickSelectCharacter> characterList = new List<ClickSelectCharacter>();
    [SerializeField]private int _columCount = 2;　//行
    [SerializeField]private int _rowCount = 3; //列
    [SerializeField]private float _horizontalSpacing = 2;　//横の間隔
    [SerializeField]private float _verticalSpacing = 2;　//縦の間隔
    private Vector3 _startPoint = new Vector3(1,0,1);　//配置の始点
    private float _objectOffsetY = 1.0f;
    
    private ClickSelectCharacter[] _selectCharacter = new ClickSelectCharacter[2];　//選択されたCharacterを格納
    
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

    /// <summary>CharacterとFieldを配置</summary>
    public void CreatePlayerObject()
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
                characterList.Add(character.GetComponent<ClickSelectCharacter>());
                character.transform.forward = leaderDirection;

                currentPosition.x += _horizontalSpacing;
                characterIndex = (characterIndex + 1) % _characterToPlace.Length; //次のcharacterのIndexを更新 
            }

            currentPosition.x = _startPoint.x;
            currentPosition.z += _verticalSpacing;
        }
    }

    /// <summary>Battleを実行する</summary>
    private void InitiateBattle()
    {
        if (_selectCharacter[0] != null && _selectCharacter[1] != null)
        {
            var power1 = _selectCharacter[0]._powerValue;
            var power2 = _selectCharacter[1]._powerValue;

            Debug.Log(power1 >= power2 ? "1の勝ち" : "２の勝ち");
        }
    }

    /// <summary>選択されたcharacterをセットしてBattleを開始する</summary>
    public void SelectCharacter(ClickSelectCharacter character)
    {
        if (_selectCharacter[0] == null)
        {
            _selectCharacter[0] = character;
        }
        else if (_selectCharacter[1] == null)
        {
            _selectCharacter[1] = character;
            InitiateBattle();　//characterが2体セットされたらBattleを開始する
            _selectCharacter[0] = null;
            _selectCharacter[1] = null;
        }
    }
}