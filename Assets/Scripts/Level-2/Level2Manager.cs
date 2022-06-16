using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;

    [SerializeField]
    GameObject _cubePrefab, timerObj, endGameScreen, timesUpScreen, phasesLeftTextObj;
    [SerializeField]
    public GameObject pauseScreen;

    GameObject[] _colorCubes = new GameObject[30];
    GameObject[] _selectedCubes = new GameObject[2];

    [SerializeField] Text phasesLeftText;

    public Color[] _colorsOfCubes = new Color[30];
    Color[] colors = new Color[15];

    List<int> indexList = new List<int>();
    int[] _selectedIndex = new int[2];

    bool[] isCubeColored = new bool[30];
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
        FindObjectOfType<AudioManager>().SetVolume("Waterfall", 0.1f);
        FindObjectOfType<AudioManager>().Play("Waterfall");
        FindObjectOfType<AudioManager>().Play("Floating");
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
        colors[14] = new Color(0.353f, 0.353f, 0.353f);
        StartCoroutine(CreateCubes());
    }

    // Update is called once per frame
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
            FindObjectOfType<AudioManager>().Play("CubeIns");
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
                if (k == 2)
                {
                    k = 0;
                    colorIndex++;
                }
            }
            FindObjectOfType<AudioManager>().Play("SetColor");
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        HideColors();
        if (phasesLeft == totalPhases)
        {
            timerObj.SetActive(true);
            phasesLeftTextObj.SetActive(true);
            Timer.Instance.SetDuration(3f, 0f);
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
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f);
            }
        }
        isColorHiding = true;
        SetCanSelect();

    }
    void HideSelectedColors()
    {
        _selectedCubes[0].GetComponent<MeshRenderer>().material.color = Color.white;
        _selectedCubes[1].GetComponent<MeshRenderer>().material.color = Color.white;
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
        else if (selectedCount == 1 && _selectedCubes[0].GetComponent<Level2MouseFeedback>()._index != selectedIndex)
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
        FindObjectOfType<AudioManager>().Play("Correct");
        Level2Calculator.Instance.Score += 50f;
        _selectedCubes[0].GetComponent<Level2MouseFeedback>().isCorrected = true;
        _selectedCubes[1].GetComponent<Level2MouseFeedback>().isCorrected = true;
        _selectedCubes[0].AddComponent<Rigidbody>();
        _selectedCubes[0].GetComponent<Rigidbody>().AddForce(Vector3.up * 150f);
        _selectedCubes[1].AddComponent<Rigidbody>();
        _selectedCubes[1].GetComponent<Rigidbody>().AddForce(Vector3.up * 150f);
        colorCubesCount -= 2;
        if (colorCubesCount == 0)
        {
            phasesLeft--;
            Invoke("EndGameCheck", 1f);
        }
        Invoke("SetCanSelect", 0.5f);
    }
    void MatchWrong()
    {
        FindObjectOfType<AudioManager>().Play("Wrong");
        Level2Calculator.Instance.wrongSelectCount++;
        if (Level2Calculator.Instance.Score > 30f)
        {
            Level2Calculator.Instance.Score -= 30f;
        }
        else if (Level2Calculator.Instance.Score <= 30f)
        {
            Level2Calculator.Instance.Score = 0;
        }
        Invoke("HideSelectedColors", 1f);
        Invoke("SetCanSelect", 1.1f);
    }
    void EndGameCheck()
    {
        if (phasesLeft > 0)
        {
            FindObjectOfType<AudioManager>().Play("Phase");
            Timer.Instance.StopTimer();
            SetFalse();
            isColorHiding = false;
            StartCoroutine(CreateCubes());
        }
        else
        {
            EndGameProcess();
        }

    }

    void EndGameProcess()
    {
        FindObjectOfType<AudioManager>().Play("EndLevel");
        FindObjectOfType<AudioManager>().Stop("Waterfall");
        FindObjectOfType<AudioManager>().Stop("Floating");
        gameEnded = true;
        Timer.Instance.StopTimer();
        endGameScreen.SetActive(true);
        Level2Calculator.Instance.SetEndGameText();
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
        FindObjectOfType<AudioManager>().Stop("Waterfall");
        FindObjectOfType<AudioManager>().Stop("Floating");
        FindObjectOfType<AudioManager>().Play("TimesUp");
        gameEnded = true;
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
