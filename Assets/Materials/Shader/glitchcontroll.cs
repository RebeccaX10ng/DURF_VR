using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffectController : MonoBehaviour
{
    public Material glitchMaterial;

    void Update()
    {
        // 动态调整故障强度
        glitchMaterial.SetFloat("_GlitchIntensity", Mathf.PingPong(Time.time * 0.5f, 1.0f));
    }
}
