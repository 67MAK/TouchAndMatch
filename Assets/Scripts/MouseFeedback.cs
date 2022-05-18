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
        if (GameManagement.instance.isColorHiding)
        {
            _renderer.material.SetFloat("_Metallic", 1f);
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log(_index + ". index color : " + GameManagement.instance._colorsOfCubes[_index]);
        Debug.Log(GameManagement.instance.canSelect);
        if (GameManagement.instance.canSelect && GameManagement.instance.isColorHiding)
        {
            _renderer.material.color = GameManagement.instance._colorsOfCubes[_index];
            GameManagement.instance.CubeSelect(_index);
        }
    }
    private void OnMouseExit()
    {
        _renderer.material.SetFloat("_Metallic", 0f);
    }
}
