using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BootScene : MonoBehaviour
{
    public Button tapToStart;
    public GameObject Tpanel;
    public GameObject panel;
    public RectTransform t_Potion;
    public RectTransform t_Story;
    private Vector2 P_pos;
    private Vector2 S_pos;

    void Awake()
    {
        P_pos = t_Potion.anchoredPosition;
        S_pos = t_Story.anchoredPosition;
       
    }

    void Start()
    {
        Tpanel.SetActive(false);
        panel.SetActive(false);

        BootAnim();

    }

    public void OntapToStart()
    {
        panel.SetActive(true);
        Tpanel.SetActive(true);
    }

    public void OnInGameButton()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void BootAnim()
    {
        t_Potion.anchoredPosition = Vector2.zero;
        t_Potion.localScale = Vector3.zero;
        t_Story.anchoredPosition = Vector2.zero;
        t_Story.localScale = Vector3.zero;

        t_Potion.DOAnchorPos(P_pos, 1f).SetEase(Ease.OutCubic);
        t_Potion.DOScale(Vector3.one, 1f).SetEase(Ease.OutExpo);
        t_Story.DOAnchorPos(S_pos, 1f).SetEase(Ease.OutCubic);
        t_Story.DOScale(Vector3.one, 1f).SetEase(Ease.OutExpo);
    }

}
