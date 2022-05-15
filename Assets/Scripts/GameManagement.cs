using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    GameObject[] _colorCubes = new GameObject[16];
    GameObject[] _selectedCubes = new GameObject[2];
    [SerializeField] GameObject _cubePrefab;

    public Color[] _colorsOfCubes = new Color[16];
    Color[] colors = new Color[8];

    int colorCubesCount = 0, selectedCount = 0, wrongSelectCount = 0;
    int rand;

    List<int> indexList = new List<int>();

    bool[] isCubeColored = new bool[16];
    public bool isColorHiding, canSelect;

    Vector3 instantiateAnchor = new Vector3 (0, 0, 0);

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {

        }
    }
    void Start()
    {
        SetFalse();
        colors[0] = new Color(1f, 0f, 0.784f);
        colors[1] = new Color(0.078f, 0f, 1f);
        colors[2] = new Color(1f, 1f, 0f);
        colors[3] = new Color(0.078f, 1f, 0f);
        colors[4] = new Color(0.5f, 0.28f, 0f);
        colors[5] = new Color(1f, 0f, 0f);
        colors[6] = new Color(0f, 1f, 1f);
        colors[7] = new Color(0f, 0f, 0f);
        StartCoroutine(CreateCubes());
        canSelect = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowColors();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HideColors();
        }
    }

    IEnumerator CreateCubes()
    {
        for(int i = 0; i < 16; i++)
        {
            _colorCubes[i] = Instantiate(_cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            _colorCubes[i].name = "ColorCube" + i;
            _colorCubes[i].GetComponent<MouseFeedback>()._index = i;
            indexList.Add(i);
            colorCubesCount++;
            instantiateAnchor = instantiateAnchor + new Vector3 (1.25f, 0, 0);
            if((i + 1) % 4 == 0)
            {
                instantiateAnchor = new Vector3(0, 0, instantiateAnchor.z);
                instantiateAnchor = instantiateAnchor + new Vector3(0, 0, 1.25f);
            }
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(SetColors());
    }
    
    void SetFalse()
    {
        for(int j = 0; j < isCubeColored.Length; j++)
        {
            isCubeColored[j] = false;
        }
    }

    IEnumerator SetColors()
    {
        int setIndex, k = 0, colorIndex = 0;
        while (indexList.Count > 0)
        {
            rand = Random.Range(0, (indexList.Count - 1));
            setIndex = indexList[rand];
            if (!isCubeColored[setIndex])
            {
                _colorCubes[setIndex].GetComponent<MeshRenderer>().material.color = colors[colorIndex];
                _colorsOfCubes[setIndex] = colors[colorIndex];
                isCubeColored[setIndex] = true;
                indexList.RemoveAt(rand);
                k++;
                if (k == 2)
                {
                    k = 0;
                    colorIndex++;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        HideColors();
    }

    void HideColors()
    {
        for(int i = 0; i < isCubeColored.Length; i++)
        {
            if (isCubeColored[i])
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f);
            }
        }
        isColorHiding = true;
    }
    void HideSelectedColors()
    {
        _selectedCubes[0].GetComponent<MeshRenderer>().material.color = Color.white;
        _selectedCubes[1].GetComponent<MeshRenderer>().material.color = Color.white;
    }
    void ShowColors()
    {
        for(int i = 0; i < _colorCubes.Length; i++)
        {
            if (isCubeColored[i])
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = _colorsOfCubes[i];
            }
        }
        isColorHiding = false;
    }

    public void CubeSelect(int selectedIndex)
    {
        if(selectedCount < 2)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            selectedCount++;
            if (selectedCount == 2)
            {
                selectedCount = 0;
                canSelect = false;
                CheckColor();
            }
        }
        else
        {
            selectedCount = 0;
        }
    }

    void CheckColor()
    {
        if (_selectedCubes[0].GetComponent<MeshRenderer>().material.color == _selectedCubes[1].GetComponent<MeshRenderer>().material.color)
        {
            MatchCorrect();
        }
        else
        {
            MatchWrong();
        }
    }

    void MatchCorrect()
    {
        Debug.Log("Match Correct");
        _selectedCubes[0].AddComponent<Rigidbody>();
        _selectedCubes[0].GetComponent<Rigidbody>().AddForce(Vector3.up * 150f);
        _selectedCubes[1].AddComponent<Rigidbody>();
        _selectedCubes[1].GetComponent<Rigidbody>().AddForce(Vector3.up * 150f);
        Destroy(_selectedCubes[0], 2f);
        Destroy(_selectedCubes[1], 2f);
        Invoke("SetCanSelect", 0.5f);
    }
    void MatchWrong()
    {
        Debug.Log("Match Wrong");
        wrongSelectCount++;
        Invoke("HideSelectedColors", 1f);
        Invoke("SetCanSelect", 1.1f);
    }
    void SetCanSelect()
    {
        canSelect = true;
    }

    IEnumerator delayFor(float _sec)
    {
        yield return new WaitForSeconds(_sec);
    }
}
