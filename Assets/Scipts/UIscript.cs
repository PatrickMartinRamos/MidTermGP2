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
    public float menuUISpeed;
    public Vector3 scaleSize;

    public Transform mainMenuUI;
    
    public void ShowUI()
    {
        //show Restart UI
        RestartUI.transform.DOLocalMove(showtargetPos, uiSpeed * Time.deltaTime);
    }

    public void CloseUI()
    {
        //close Restart UI
        RestartUI.transform.DOLocalMove(closetargetPos, uiSpeed * Time.deltaTime);
    }

    public void closeMenuUI()
    {
        //Close Menu UI when Play button is press
        mainMenuUI.transform.DOScale(Vector3.zero, menuUISpeed * Time.deltaTime).SetEase(Ease.InBack);
    }

    public void openMenuUI()
    {
        //Show the Menu UI when player choose "NO" button in restart menu
        mainMenuUI.transform.DOScale(scaleSize, menuUISpeed * Time.deltaTime).SetEase(Ease.InBack);
    }    
}
