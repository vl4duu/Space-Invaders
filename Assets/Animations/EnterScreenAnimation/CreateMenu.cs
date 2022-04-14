using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMenu : MonoBehaviour
{
    public GameObject menu;

    public void MakeMenuAppear()
    {
        Debug.Log("ishappening!");
        GameObject activeMenu = Instantiate(menu, this.transform.parent, false);
        Debug.Log(activeMenu.GetComponent<RectTransform>().position);
        Debug.Log(activeMenu.GetComponent<RectTransform>().position);
    }
}
