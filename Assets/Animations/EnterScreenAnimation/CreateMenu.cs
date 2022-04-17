using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMenu : MonoBehaviour
{
    public GameObject menu;

    public void MakeMenuAppear()
    {
        GameObject activeMenu = Instantiate(menu, this.transform.parent, false);
    }
}