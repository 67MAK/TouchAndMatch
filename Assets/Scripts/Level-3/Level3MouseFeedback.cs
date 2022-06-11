using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3MouseFeedback : MonoBehaviour
{
    Renderer _renderer;

    public int _index;
    public bool isCorrected = false;

    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }*/

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        if (Level3Manager.Instance.isColorHiding && !Level3Manager.Instance.gamePaused)
        {
            transform.localScale += new Vector3(0, 0.2f, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Level3Manager.Instance.canSelect && Level3Manager.Instance.isColorHiding)
        {
            if (!isCorrected)
            {
                _renderer.material.color = Level3Manager.Instance._colorsOfCubes[_index];
                Level3Manager.Instance.CubeSelect(_index);
            }
        }
    }
    private void OnMouseExit()
    {
        if (Level3Manager.Instance.isColorHiding && !Level3Manager.Instance.gamePaused)
        {
            transform.localScale -= new Vector3(0, 0.2f, 0);
        }
    }
}
