using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    public AudioMixer audioMixer; // 오디오 믹서

    // 볼륨 조절 슬라이더
    public Slider BGSlider; // 배경음악
    public Slider SESlider; // 음향 효과

    float bgVolume; // 배경 음악 크기
    float seVolume; // 음향 효과 크기

    // 볼륨 조절
    public void SetBGVolume() // 배경 음악
    {
        audioMixer.SetFloat("BG", Mathf.Log10(BGSlider.value) * 20); // 로그 연산 값 오디오 믹서에 전달
    }
    public void SetSEVolume() // 음향 효과
    {
        audioMixer.SetFloat("SE", Mathf.Log10(SESlider.value) * 20); // 로그 연산 값 오디오 믹서에 전달
    }

    private void Update()
    {
        audioMixer.GetFloat("BG", out bgVolume); // 배경 음악의 크기 구함
        BGSlider.value = Mathf.Pow(10f, bgVolume / 20f); // 배경 음악의 크기 슬라이더에 반영
        audioMixer.GetFloat("SE", out seVolume); // 음향 효과의 크기 구함
        SESlider.value = Mathf.Pow(10f, seVolume / 20f); // 음향 효과의 크기 슬라이더에 반영
    }
}
