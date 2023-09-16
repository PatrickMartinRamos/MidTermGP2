using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIscript : MonoBehaviour
{
    public GameObject RestartUI;
    public Vector3 showtargetPos;
    public Vector3 closetargetPos;
    public float uiSpeed;

    public void ShowUI()
    {
        RestartUI.transform.DOLocalMove(showtargetPos, uiSpeed * Time.deltaTime);
    }

    public void CloseUI()
    {
        RestartUI.transform.DOLocalMove(closetargetPos, uiSpeed * Time.deltaTime);
    }
}
