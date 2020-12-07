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
    // Start is called before the first frame update
    void Start()
    {
        stat = gameObject.transform.parent.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        amount.text = stat.health + "/" + stat.maxHealth;
        fill.fillAmount = (float)stat.health / (float)stat.maxHealth;
    }
}
