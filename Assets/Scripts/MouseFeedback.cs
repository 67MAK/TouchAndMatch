using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFeedback : MonoBehaviour
{

    public static MouseFeedback instance;
    Renderer _renderer;

    public int _index;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    private void OnMouseEnter()
    {
        if (Level1Manager.Instance.isColorHiding)
        {
            transform.position += new Vector3(0, 0.1f, 0); 
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log(_index + ". index color : " + Level1Manager.Instance._colorsOfCubes[_index]);
        Debug.Log(Level1Manager.Instance.canSelect);
        if (Level1Manager.Instance.canSelect && Level1Manager.Instance.isColorHiding)
        {
            _renderer.material.color = Level1Manager.Instance._colorsOfCubes[_index];
            Level1Manager.Instance.CubeSelect(_index);
        }
    }
    private void OnMouseExit()
    {
        if (Level1Manager.Instance.isColorHiding)
        {
            transform.position -= new Vector3(0, 0.1f, 0);
        }
    }
}
