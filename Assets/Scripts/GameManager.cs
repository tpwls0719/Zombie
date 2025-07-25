﻿using UnityEngine;

// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindFirstObjectByType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
        
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태

    private Coroutine zombieTimerCoroutine;

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        FindFirstObjectByType<PlayerHealth>().onDeath += EndGame;
    }

    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore)
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score);
        }
    }

    // 게임 오버 처리
    public void EndGame()
    {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);
    }
    
    public void StartZombieTimer()
    {
        if (zombieTimerCoroutine != null)
            StopCoroutine(zombieTimerCoroutine);
        zombieTimerCoroutine = StartCoroutine(ZombieTimer());
    }

    private System.Collections.IEnumerator ZombieTimer()
    {
        float timer = 30f;
        //Debug.Log("좀비 타이머 시작");

        while (timer > 0f)
        {
            if (ZombieSpawner.instance.ZombieCount == 0)
            {
                UIManager.instance.UpdateZombieTimer(0f); // 타이머 초기화
                yield break;
            }

            UIManager.instance.UpdateZombieTimer(timer); // ⏱ UI 갱신

            timer -= Time.deltaTime;
            yield return null;
        }

        //Debug.Log("제한 시간 초과");
        EndGame();
    }
}