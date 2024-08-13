using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public List<Light> lightsToChange; // 要改变颜色的灯光列表
    public AudioSource audioSource; // 用于播放音频的AudioSource
    public AudioClip initialClip; // 第一个要播放的音频片段
    public AudioClip loopClip; // 要循环播放的音频片段
    public GameObject disable;
    public GameObject enable;

    private bool hasBeenTriggered = false; // 标记函数是否已经被触发

    void Start()
    {
        ChangeLightsAndPlayAudio();
    }
    
    // Public函数，用于改变灯光颜色并播放音频
    public void ChangeLightsAndPlayAudio()
    {
        if (!hasBeenTriggered)
        {
            // 将列表中的所有灯光颜色改为红色
            foreach (Light light in lightsToChange)
            {
                if (light != null)
                {
                    light.color = Color.red; // 改变灯光颜色为红色
                }
            }
            
            disable.SetActive(false);
            enable.SetActive(true);

            // 启用并播放音频
            if (audioSource != null && initialClip != null)
            {
                audioSource.clip = initialClip;
                audioSource.Play();

                // 监听第一个音频片段播放完毕
                Invoke(nameof(PlayLoopClip), initialClip.length);
            }

            hasBeenTriggered = true; // 标记为已触发，确保函数只执行一次
        }
    }

    // 在第一个Clip播放完后调用该函数
    private void PlayLoopClip()
    {
        if (audioSource != null && loopClip != null)
        {
            audioSource.clip = loopClip;
            audioSource.loop = true; // 设置为循环播放
            audioSource.Play();
        }
    }
}