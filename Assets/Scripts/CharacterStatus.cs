using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private GameObject blushGO;
    [SerializeField]
    private GameObject shadowGO;
    [SerializeField]
    private GameObject lipstickGO;

    [SerializeField]
    private CreamInteract creamScript;
    [SerializeField]
    private BlushInteract blushScript;
    [SerializeField]
    private BlushInteract shadowScript;
    [SerializeField]
    private LipstickInteract lipstickScript;

    private void OnEnable()
    {
        creamScript.OnSelected += CreamSelect;
        blushScript.OnSelected += BlushSelect;
        shadowScript.OnSelected += ShadowSelect;
        lipstickScript.OnSelected += LipstickSelect;
    }

    private void OnDisable()
    {
        creamScript.OnSelected -= CreamSelect;
        blushScript.OnSelected -= BlushSelect;
        shadowScript.OnSelected -= ShadowSelect;
        lipstickScript.OnSelected -= LipstickSelect;
    }

    //Блокировка выбора других инструментов, пока используется текущий

    public void ResetMakeUp()
    {
        blushGO.SetActive(false);
        shadowGO.SetActive(false);
        lipstickGO.SetActive(false);
    }

    private void CreamSelect(bool isSelected)
    {
        blushScript.SetBlocked(isSelected);
        shadowScript.SetBlocked(isSelected);
        lipstickScript.SetBlocked(isSelected);
    }

    private void BlushSelect(bool isSelected)
    {
        creamScript.SetBlocked(isSelected);
        shadowScript.SetBlocked(isSelected);
        lipstickScript.SetBlocked(isSelected);
    }

    private void ShadowSelect(bool isSelected)
    {
        creamScript.SetBlocked(isSelected);
        blushScript.SetBlocked(isSelected);
        lipstickScript.SetBlocked(isSelected);
    }

    private void LipstickSelect(bool isSelected)
    {
        creamScript.SetBlocked(isSelected);
        blushScript.SetBlocked(isSelected);
        shadowScript.SetBlocked(isSelected);
    }
}
