using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    public AudioMixer audioMixer; // ����� �ͼ�

    // ���� ���� �����̴�
    public Slider BGSlider; // �������
    public Slider SESlider; // ���� ȿ��

    float bgVolume; // ��� ���� ũ��
    float seVolume; // ���� ȿ�� ũ��

    // ���� ����
    public void SetBGVolume() // ��� ����
    {
        audioMixer.SetFloat("BG", Mathf.Log10(BGSlider.value) * 20); // �α� ���� �� ����� �ͼ��� ����
    }
    public void SetSEVolume() // ���� ȿ��
    {
        audioMixer.SetFloat("SE", Mathf.Log10(SESlider.value) * 20); // �α� ���� �� ����� �ͼ��� ����
    }

    private void Update()
    {
        audioMixer.GetFloat("BG", out bgVolume); // ��� ������ ũ�� ����
        BGSlider.value = Mathf.Pow(10f, bgVolume / 20f); // ��� ������ ũ�� �����̴��� �ݿ�
        audioMixer.GetFloat("SE", out seVolume); // ���� ȿ���� ũ�� ����
        SESlider.value = Mathf.Pow(10f, seVolume / 20f); // ���� ȿ���� ũ�� �����̴��� �ݿ�
    }
}
