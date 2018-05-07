using UnityEngine;
using System.Collections;

public class GlitchEffect : MonoBehaviour {
    public Material mMaterial = null;

    public Texture2D displacementMap;
    float glitchUp, glitchDown, flicker, glitchUpTime = 0.05f, glitchDownTime = 0.05f, flickerTime = 0.5f;

    [Header("Glitch Intensity")]

    [Range(0, 1)]
    public float intensity;
    
    [Range(0, 1)]
    public float flipIntensity;

    [Range(0, 1)]
    public float colorIntensity;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(mMaterial)
        {
            mMaterial.SetFloat("_Intensity", intensity);
            mMaterial.SetFloat("_ColorIntensity", colorIntensity);
            mMaterial.SetTexture("_DispTex", displacementMap);
            
            flicker += Time.deltaTime * colorIntensity;

            if(flicker > flickerTime)
            {
                mMaterial.SetFloat("filterRadius", Random.Range(-3f, 3f) * colorIntensity);
                mMaterial.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
                flicker = 0;
                flickerTime = Random.value;
            }

            if(colorIntensity == 0)
            {
                mMaterial.SetFloat("filterRadius", 0);
            }

            glitchUp += Time.deltaTime * flipIntensity;
            if(glitchUp > glitchUpTime)
            {
                if(Random.value < 0.1f *flipIntensity)
                {
                    mMaterial.SetFloat("flip_up", Random.Range(0,1f) * flipIntensity);
                }
                else
                {
                    mMaterial.SetFloat("flip_up", 0);
                }

                glitchUp = 0;
                glitchUpTime = Random.value/10f;
            }

            if(flipIntensity == 0)
            {
                mMaterial.SetFloat("flip_up", 0);
            }

            glitchDown += Time.deltaTime * flipIntensity;
            if (glitchDown > glitchDownTime)
            {
                if(Random.value < 0.1f * flipIntensity)
                {
                    mMaterial.SetFloat("flip_down", 1 - Random.Range(0, 1f) * flipIntensity);
                }
                else
                {
                    mMaterial.SetFloat("flip_down", 1);
                }
                
                glitchDown = 0;
                glitchDownTime = Random.value/10f;
            }

            if(flipIntensity == 0)
            {
                mMaterial.SetFloat("flip_down", 1);
            }

            if(Random.value < 0.05 * intensity)
            {
                mMaterial.SetFloat("displace", Random.value * intensity);
			    mMaterial.SetFloat("scale", 1 - Random.value * intensity);
            }
            else
            {
                mMaterial.SetFloat("displace", 0);
            }

            Graphics.Blit(src, dest, mMaterial);

        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
