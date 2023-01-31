using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInCameraSingle : MonoBehaviour
{

    [SerializeField, Header("スポーンポイントの親")]
    private GameObject _enemySpawn;

    [field:SerializeField]
    public List<GameObject> _spawnChildren { get; private set; }

    [field:SerializeField]
    public bool[] _insidecamera { get; private set; }
    [SerializeField, Header("敵を消す用の枠の最大値(普通の枠より大きく設定してください)")]
    private Vector2 _deathMaxPoint = default;
    [SerializeField, Header("敵を消す用の枠の最小値(普通の枠より大きく設定してください)")]
    private Vector2 _deathMinPoint = default;

    [SerializeField, Header("枠の最大値")]
    private Vector2 _maxPoint;

    [SerializeField, Header("枠の最小値")]
    private Vector2 _minPoint;

    public Vector2 _rightTopPoint = default;
    public Vector2 _leftUnderPoint = default;
    [HideInInspector]
    public Vector2 _deathRightTopPoint = default;
    [HideInInspector]
    public Vector2 _deathLeftUnderPoint = default;

    public int _spawnCount { get; private set; }   

    protected void Start()
    {
        //配列初期化
        _spawnChildren = new List<GameObject>();
        //子オブジェクトの数
        _spawnCount = _enemySpawn.transform.childCount;

        //配列の長さを子オブジェクトと同じにする
        _insidecamera = new bool[_spawnCount];

        //_enemyspawnの子オブジェクト全てを_spawnchildrenの中に入れる
        foreach (Transform child in _enemySpawn.transform)
        {
            _spawnChildren.Add(child.gameObject);
        }
    }
    protected void Update()
    {
        //右上の頂点
         _rightTopPoint = new Vector2(transform.position.x + _maxPoint.x, transform.position.y + _maxPoint.y);
        //左上の頂点
        _leftUnderPoint = new Vector2(transform.position.x + _minPoint.x, transform.position.y + _minPoint.y) ;
        //敵を消す右上の頂点
        _deathRightTopPoint = new Vector2(transform.position.x + _deathMaxPoint.x, transform.position.y + _deathMaxPoint.y);
        //敵を消す左上の頂点
        _deathLeftUnderPoint = new Vector2(transform.position.x + _deathMinPoint.x, transform.position.y + _deathMinPoint.y);


        //設定した枠の中に入っているかどうか
        for (int i = 0; i < _spawnCount; i++)
        {

            //親オブジェクト（スポーンポイント）が枠の中に入っているか判定
            if (_rightTopPoint.x > _spawnChildren[i].transform.position.x && _spawnChildren[i].transform.position.x > _leftUnderPoint.x
            && _rightTopPoint.y > _spawnChildren[i].transform.position.y && _spawnChildren[i].transform.position.y > _leftUnderPoint.y)
            {
                if (_insidecamera[i] == false)
                {
                    _spawnChildren[i].GetComponent<EnemypoolSingle>().InstantiateEnemy();
                    _insidecamera[i] = true;
                }
            }

            else
            {
                _insidecamera[i] = false;
                
            }
            //カメラ外の敵のactiveをfalseにする
            for (int j = 0; j < _spawnChildren[i].transform.childCount; j++)
            {
                print(_spawnChildren[i].transform.GetChild(j));
                //子オブジェクト一つ一つが敵を消す枠に入っているかどうか
                if (_deathRightTopPoint.x > _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.x
                    && _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.x > _deathLeftUnderPoint.x
                    && _deathRightTopPoint.y > _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.y
                    && _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.y > _deathLeftUnderPoint.y)
                {

                }
                else
                {
                    //枠から出た子オブジェクトのアクティブをfalseにする
                    _spawnChildren[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }

        }

    }

}
