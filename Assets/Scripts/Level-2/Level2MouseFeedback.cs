using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2MouseFeedback : MonoBehaviour
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
        if (Level2Manager.Instance.isColorHiding && !Level2Manager.Instance.gamePaused)
        {
            transform.localScale += new Vector3(0, 0.2f, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Level2Manager.Instance.canSelect && Level2Manager.Instance.isColorHiding)
        {
            if (!isCorrected)
            {
                FindObjectOfType<AudioManager>().Play("CubeSelect");
                _renderer.material.color = Level2Manager.Instance._colorsOfCubes[_index];
                Level2Manager.Instance.CubeSelect(_index);
            }
        }
    }
    private void OnMouseExit()
    {
        if (Level2Manager.Instance.isColorHiding && !Level2Manager.Instance.gamePaused)
        {
            transform.localScale -= new Vector3(0, 0.2f, 0);
        }
    }
}
