using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class GameManagement : MonoBehaviour
{
    GameObject[] _colorCubes = new GameObject[16];
    [SerializeField] GameObject _cubePrefab;

    Color[] _colorsOfCubes = new Color[16];
    Color[] colors = new Color[8];

    int _colorCubesCount = 0;
    int rand;

    List<int> indexList = new List<int>();

    bool[] isCubeColored = new bool[16];

    Vector3 instantiateAnchor = new Vector3 (0, 0, 0);

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
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowColors();
            //checkColor();
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
            indexList.Add(i);
            _colorCubesCount++;
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
    }

    void checkColor()
    {
        for(int i = 0; i< _colorsOfCubes.Length; i++)
        {
            if(_colorsOfCubes[i] == null)
            {
                Debug.Log("Hata indisi : " + i);
            }
            else
            {
                Debug.Log(i + ".) Cube's Color : " + _colorsOfCubes[i]);
            }
        }
    }
}
