using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    static public Level3Manager Instance;
    [SerializeField]
    GameObject _cubePrefab, timerObj, endGameScreen, timesUpScreen, phasesLeftTextObj;
    [SerializeField]
    public GameObject pauseScreen;

    GameObject[] _colorCubes = new GameObject[42];
    GameObject[] _selectedCubes = new GameObject[3];

    [SerializeField] Text phasesLeftText;

    public Color[] _colorsOfCubes = new Color[42];
    Color[] colors = new Color[14];

    List<int> indexList = new List<int>();
    int[] _selectedIndex = new int[3];

    bool[] isCubeColored = new bool[42];
    public bool canSelect, isColorHiding, gameEnded, gamePaused;

    Vector3 instantiateAnchor = Vector3.zero;

    int colorCubesCount = 0, selectedCount = 0;
    int rand;
    int phasesLeft, totalPhases = 2;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        canSelect = false;
        gameEnded = false;
        SetFalse();
        phasesLeft = totalPhases;
        colors[0] = new Color(1f, 0f, 0.784f);
        colors[1] = new Color(0.078f, 0f, 1f);
        colors[2] = new Color(1f, 1f, 0f);
        colors[3] = new Color(0.078f, 1f, 0f);
        colors[4] = new Color(0.5f, 0.28f, 0f);
        colors[5] = new Color(1f, 0f, 0f);
        colors[6] = new Color(0f, 1f, 1f);
        colors[7] = new Color(0f, 0f, 0f);
        colors[8] = new Color(1f, 0.431f, 0f);
        colors[9] = new Color(0f, 0.392f, 0.392f);
        colors[10] = new Color(0.392f, 0f, 0f);
        colors[11] = new Color(0.392f, 0f, 0.392f);
        colors[12] = new Color(0f, 0.392f, 0f);
        colors[13] = new Color(0f, 0f, 0.431f);
        StartCoroutine(CreateCubes());
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            Timer.Instance.SetDuration(3f, 0f);
        }
    }

    IEnumerator CreateCubes()
    {
        canSelect = false;
        StartCoroutine(SetPhasesLeftText());
        for (int i = 0; i < 42; i++)
        {
            _colorCubes[i] = Instantiate(_cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            _colorCubes[i].name = "ColorCube" + i;
            _colorCubes[i].GetComponent<Level3MouseFeedback>()._index = i;
            indexList.Add(i);
            colorCubesCount++;
            instantiateAnchor = instantiateAnchor + new Vector3(1.25f, 0, 0);
            if ((i + 1) % 6 == 0)
            {
                instantiateAnchor = new Vector3(0, 0, instantiateAnchor.z);
                instantiateAnchor = instantiateAnchor + new Vector3(0, 0, 1.25f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        instantiateAnchor = Vector3.zero;
        StartCoroutine(SetColors());
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
                if (k == 3)
                {
                    k = 0;
                    colorIndex++;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        HideColors();
        if (phasesLeft == totalPhases)
        {
            timerObj.SetActive(true);
            phasesLeftTextObj.SetActive(true);
            Timer.Instance.SetDuration(4f, 0f);
            Timer.Instance.StartTimer();
        }
        else
        {
            Timer.Instance.StartTimer();
        }
    }

    public void HideColors()
    {
        for (int i = 0; i < isCubeColored.Length; i++)
        {
            if (isCubeColored[i] && _colorCubes[i] != null)
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        isColorHiding = true;
        SetCanSelect();
    }
    void HideSelectedColors()
    {
        foreach(GameObject obj in _selectedCubes)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
    public void ShowColors()
    {
        for (int i = 0; i < _colorCubes.Length; i++)
        {
            if (isCubeColored[i] && _colorCubes[i] != null)
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = _colorsOfCubes[i];
            }
        }
        isColorHiding = false;
        canSelect = false;
    }


    public void CubeSelect(int selectedIndex)
    {
        if (selectedCount == 0)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount++;
        }
        else if (selectedCount == 1 && _selectedCubes[0].GetComponent<Level3MouseFeedback>()._index != selectedIndex)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount++;
        }
        else if(selectedCount == 2 && _selectedCubes[1].GetComponent<Level3MouseFeedback>()._index != selectedIndex && _selectedCubes[0].GetComponent<Level3MouseFeedback>()._index != selectedIndex)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount = 0;
            canSelect = false;
            CheckColor();
        }
    }

    void CheckColor()
    {
        bool areColorsMatch = true;

        if (_selectedCubes[0].GetComponent<MeshRenderer>().material.color != _selectedCubes[1].GetComponent<MeshRenderer>().material.color)
            areColorsMatch = false;
        if (_selectedCubes[0].GetComponent<MeshRenderer>().material.color != _selectedCubes[2].GetComponent<MeshRenderer>().material.color)
            areColorsMatch = false;
        if (_selectedCubes[1].GetComponent<MeshRenderer>().material.color != _selectedCubes[2].GetComponent<MeshRenderer>().material.color)
            areColorsMatch = false;

        if (areColorsMatch)
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
        Level3Calculator.Instance.Score += 50f;
        for(int i = 0; i < _selectedCubes.Length; i++)
        {
            _selectedCubes[i].GetComponent<Level3MouseFeedback>().isCorrected = true;
            _selectedCubes[i].AddComponent<Rigidbody>();
            _selectedCubes[i].GetComponent<Rigidbody>().AddForce(Vector3.up * 150f);
            Destroy(_selectedCubes[i], 2f);
        }
        colorCubesCount -= 3;
        if (colorCubesCount == 0)
        {
            phasesLeft--;
            Invoke("EndGameCheck", 1f);
        }
        Invoke("SetCanSelect", 0.5f);
    }
    void MatchWrong()
    {
        Debug.Log("Match Wrong");
        Level3Calculator.Instance.wrongSelectCount++;
        if (Level3Calculator.Instance.Score > 30f)
        {
            Level3Calculator.Instance.Score -= 30f;
        }
        else if (Level3Calculator.Instance.Score <= 30f)
        {
            Level3Calculator.Instance.Score = 0;
        }
        Invoke("HideSelectedColors", 1f);
        Invoke("SetCanSelect", 1.1f);
    }
    void EndGameCheck()
    {
        if (phasesLeft > 0)
        {
            SetFalse();
            StartCoroutine(CreateCubes());
        }
        else
        {
            EndGameProcess();
        }

    }

    void EndGameProcess()
    {
        gameEnded = true;
        Timer.Instance.StartTimer();
        endGameScreen.SetActive(true);
        Level3Calculator.Instance.SetEndGameText();
    }
    public void PauseGameProcess()
    {
        gamePaused = true;
        canSelect = false;
        Time.timeScale = 0f;
        pauseScreen.gameObject.SetActive(true);
    }
    public void TimesUpProcess()
    {
        Time.timeScale = 0f;
        timesUpScreen.SetActive(true);
    }

    IEnumerator SetPhasesLeftText()
    {
        phasesLeftTextObj.GetComponent<Text>().color = Color.red;
        phasesLeftText.text = "Phases Left\n" + phasesLeft + "/" + totalPhases;
        yield return new WaitForSeconds(1f);
        phasesLeftTextObj.GetComponent<Text>().color = Color.black;
        yield return null;
    }

    void SetCanSelect()
    {
        canSelect = true;
    }
    void SetFalse()
    {
        for (int j = 0; j < isCubeColored.Length; j++)
        {
            isCubeColored[j] = false;
        }
    }
}
