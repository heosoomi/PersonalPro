using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.XR;

public enum GameState
{
    Boot,
    MainMenu,
    Playing,
    Pause,
    Crafting,
    QuaterEvaluation,
    Collection,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    // 상태 변경시 구독할 수 있는 이벤트 
    public event Action<GameState> OnStateChanged;

    void Awake()
    {
        // 싱글턴 보장
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CurrentState = GameState.Boot;

    }
    private void Start()
    {
        //부트 완료후 메인메뉴로 전환
        ChangeState(GameState.MainMenu);
    }

    /* 게임 상태를 새로 설정하고, 그에 맞는 씬 전환/초기화 로직을 호출*/

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
        //HandleStateEnter(newState);
    }
    // private void HandleStateEnter(GameState state)
    // {
    //     switch (state)
    //     {
            
    //         //default:
    //     }
    // }
}
