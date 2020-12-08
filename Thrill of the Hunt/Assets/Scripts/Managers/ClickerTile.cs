using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerTile : MonoBehaviour
{
    public Material idle;
    public Material onHover;
    public Material onClick;
    MeshRenderer renderer;
    Clicker clicker;
    public GridMovementController gmc;
    public bool used = false;
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
    private void OnDisable()
    {
        renderer.material = idle;
        used = false;
    }

    private void OnMouseEnter()
    {
        renderer.material = onHover;
    }
    private void OnMouseExit()
    {
        renderer.material = idle;
    }

    private void OnMouseDown()
    {
        renderer.material = onClick;       
    }

    private void OnMouseUp()
    {
        clicker.Clicked(this);
    }


}
