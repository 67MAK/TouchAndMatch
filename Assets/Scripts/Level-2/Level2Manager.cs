using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;
    [SerializeField]
    GameObject _cubePrefab;

    GameObject[] _colorCubes = new GameObject[30];
    //GameObject[] _selectedCubes = new GameObject[2];

    List<int> indexList = new List<int>();

    bool[] isCubeColored = new bool[30];

    public bool canSelect, isColorHiding;

    Vector3 instantiateAnchor = Vector3.zero;

    int colorCubesCount = 0, selectedCount = 0;
    int rand;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        StartCoroutine(CreateCubes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CreateCubes()
    {
        canSelect = false;
        //StartCoroutine(SetPhasesLeftText());
        for (int i = 0; i < 30; i++)
        {
            _colorCubes[i] = Instantiate(_cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            _colorCubes[i].name = "ColorCube" + i;
            _colorCubes[i].GetComponent<Level2MouseFeedback>()._index = i;
            indexList.Add(i);
            colorCubesCount++;
            instantiateAnchor = instantiateAnchor + new Vector3(1.25f, 0, 0);
            if ((i + 1) % 5 == 0)
            {
                instantiateAnchor = new Vector3(0, 0, instantiateAnchor.z);
                instantiateAnchor = instantiateAnchor + new Vector3(0, 0, 1.25f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        instantiateAnchor = Vector3.zero;
        //StartCoroutine(SetColors());
        canSelect = true;
        isColorHiding = true;
    }
}
