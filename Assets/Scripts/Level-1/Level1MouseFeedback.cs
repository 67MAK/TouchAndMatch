using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1MouseFeedback : MonoBehaviour
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
        if (Level1Manager.Instance.isColorHiding && !Level1Manager.Instance.gamePaused)
        {
            transform.localScale += new Vector3(0, 0.2f, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Level1Manager.Instance.canSelect && Level1Manager.Instance.isColorHiding)
        {
            if (!isCorrected)
            {
                FindObjectOfType<AudioManager>().Play("CubeSelect");
                _renderer.material.color = Level1Manager.Instance._colorsOfCubes[_index];
                Level1Manager.Instance.CubeSelect(_index);
            }
        }
    }
    private void OnMouseExit()
    {
        if (Level1Manager.Instance.isColorHiding && !Level1Manager.Instance.gamePaused)
        {
            transform.localScale -= new Vector3(0, 0.2f, 0);
        }
    }
}
