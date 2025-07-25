﻿using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindFirstObjectByType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI

    public Text waveCenterText; // 화면 중앙에 띄울 웨이브 텍스트

    private int previousWave = 0;

    public Text zombieTimerText;
    

    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
        // 웨이브 바뀔 때만 중앙에 출력
        if (waves != previousWave)
        {
            UpdateWaveCenterText(waves);
            previousWave = waves;
        }
        
    }

    public void UpdateWaveCenterText(int wave)
    {  
        waveCenterText.gameObject.SetActive(true);
        waveCenterText.text = "Wave : "+ wave.ToString();
        StartCoroutine(HideWaveCenterText());
        
    }
    
    private System.Collections.IEnumerator HideWaveCenterText()
    {
        yield return new WaitForSeconds(0.8f);
        waveCenterText.gameObject.SetActive(false);
    }

    public void UpdateZombieTimer(float timeLeft)
    {
        if (zombieTimerText != null)
            zombieTimerText.text = "Time Left : " + timeLeft.ToString("F1") + "s";
    }


    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}