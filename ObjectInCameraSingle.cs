using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInCameraSingle : MonoBehaviour
{

    [SerializeField, Header("�X�|�[���|�C���g�̐e")]
    private GameObject _enemySpawn;

    [field:SerializeField]
    public List<GameObject> _spawnChildren { get; private set; }

    [field:SerializeField]
    public bool[] _insidecamera { get; private set; }
    [SerializeField, Header("�G�������p�̘g�̍ő�l(���ʂ̘g���傫���ݒ肵�Ă�������)")]
    private Vector2 _deathMaxPoint = default;
    [SerializeField, Header("�G�������p�̘g�̍ŏ��l(���ʂ̘g���傫���ݒ肵�Ă�������)")]
    private Vector2 _deathMinPoint = default;

    [SerializeField, Header("�g�̍ő�l")]
    private Vector2 _maxPoint;

    [SerializeField, Header("�g�̍ŏ��l")]
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
        //�z�񏉊���
        _spawnChildren = new List<GameObject>();
        //�q�I�u�W�F�N�g�̐�
        _spawnCount = _enemySpawn.transform.childCount;

        //�z��̒������q�I�u�W�F�N�g�Ɠ����ɂ���
        _insidecamera = new bool[_spawnCount];

        //_enemyspawn�̎q�I�u�W�F�N�g�S�Ă�_spawnchildren�̒��ɓ����
        foreach (Transform child in _enemySpawn.transform)
        {
            _spawnChildren.Add(child.gameObject);
        }
    }
    protected void Update()
    {
        //�E��̒��_
         _rightTopPoint = new Vector2(transform.position.x + _maxPoint.x, transform.position.y + _maxPoint.y);
        //����̒��_
        _leftUnderPoint = new Vector2(transform.position.x + _minPoint.x, transform.position.y + _minPoint.y) ;
        //�G�������E��̒��_
        _deathRightTopPoint = new Vector2(transform.position.x + _deathMaxPoint.x, transform.position.y + _deathMaxPoint.y);
        //�G����������̒��_
        _deathLeftUnderPoint = new Vector2(transform.position.x + _deathMinPoint.x, transform.position.y + _deathMinPoint.y);


        //�ݒ肵���g�̒��ɓ����Ă��邩�ǂ���
        for (int i = 0; i < _spawnCount; i++)
        {

            //�e�I�u�W�F�N�g�i�X�|�[���|�C���g�j���g�̒��ɓ����Ă��邩����
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
            //�J�����O�̓G��active��false�ɂ���
            for (int j = 0; j < _spawnChildren[i].transform.childCount; j++)
            {
                print(_spawnChildren[i].transform.GetChild(j));
                //�q�I�u�W�F�N�g�����G�������g�ɓ����Ă��邩�ǂ���
                if (_deathRightTopPoint.x > _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.x
                    && _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.x > _deathLeftUnderPoint.x
                    && _deathRightTopPoint.y > _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.y
                    && _spawnChildren[i].transform.GetChild(j).gameObject.transform.position.y > _deathLeftUnderPoint.y)
                {

                }
                else
                {
                    //�g����o���q�I�u�W�F�N�g�̃A�N�e�B�u��false�ɂ���
                    _spawnChildren[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }

        }

    }

}
