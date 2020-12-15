using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerTile : MonoBehaviour
{
    public Color[] onHover;
    public Color[] onIdle;
    public Color[] onClick;
    MeshRenderer renderer;
    Clicker clicker;
    public GridMovementController gmc;
    public bool used = false;

    public enum TileType
    {
        Walkable = 0, Attackable, Healable, Range, Max
    }
    public TileType tileType = TileType.Range;
    // Start is called before the first frame update
    public void setClicker(Clicker _clicker) => clicker = _clicker;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        gmc = GetComponent<GridMovementController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn(TileType type)
    {
        if(!renderer) renderer = GetComponent<MeshRenderer>();
        if(!gmc) gmc = GetComponent<GridMovementController>();
        used = true;
        tileType = type;
        int index = (int)tileType;
        Color color = onIdle[(int)tileType];
        renderer.material.color = onIdle[(int)tileType];
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        used = false;
        gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        renderer.material.color = onHover[(int)tileType];
    }
    private void OnMouseExit()
    {
        renderer.material.color = onIdle[(int)tileType];
    }

    private void OnMouseDown()
    {
        renderer.material.color = onClick[(int)tileType];       
    }

    private void OnMouseUp()
    {
        clicker.Clicked(this);
    }


}
