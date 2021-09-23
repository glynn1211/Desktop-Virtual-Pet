using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(Cleanup);
        StartCoroutine("CareMistake");
    }

    private void Cleanup()
    {
        Destroy(this.gameObject);
    }
    private IEnumerator CareMistake()
    {
        yield return new WaitForSeconds(30);
        DigimonManager.instance.AddCareMistake();
    }
}
