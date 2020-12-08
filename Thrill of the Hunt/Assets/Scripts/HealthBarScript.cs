using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    [SerializeField]
    Image fill;
    [SerializeField]
    Text amount;

    Stats stat;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        stat = gameObject.transform.parent.GetComponent<Stats>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        amount.text = stat.currHealth + "/" + stat.maxHealth;
        fill.fillAmount = (float)stat.currHealth / (float)stat.maxHealth;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
}
