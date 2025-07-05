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
    public Button Load;
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

    // ★★★ "새 게임" 버튼 (돈 0으로 초기화) ★★★
    public void OnInGameButton()
    {
        MoneyManager.Instance.SetMoney(0); // 또는 원하는 초기값
        MoneyManager.Instance.SaveMoney(); // 저장
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

    // ★★★ "불러오기" 버튼 (저장값 로드) ★★★
    public void LoadButton()
    {
        MoneyManager.Instance.LoadMoney(); // 저장된 돈 로드 및 적용
        SceneManager.LoadScene("MainGame");
    }
}
