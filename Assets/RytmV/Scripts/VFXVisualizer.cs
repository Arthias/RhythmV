using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFXVisualizer : MonoBehaviour
{
    #region Fields
    [Header("General")]
    //Reference the VFX graph
    [SerializeField] private VisualEffect visualEffect;
    //PeerPlays Script for retrieving SpectrumData
    [SerializeField] private AudioController audioDataSource;

    [Space(1)]
    [Header("Multipliers")]
    [Tooltip("Manages amount of particles")]
    [SerializeField] private float VFXIntensityMultiplier = 5;
    [Tooltip("Manages size of the area particles spawn in")]
    [SerializeField] private float VFXRadiusMultiplier = 1;

    [SerializeField] private float VFXGravityMinMultiplier = -2;
    [SerializeField] private float VFXGravityMaxMultiplier = 2;

    #endregion


    #region Enums and Propertys

    #endregion

    #region Unity Methods


    void Update()
    {
        SetBasicVFXValues();
        ApplyVFXGravity();
    }
    #endregion


    #region VFX Methods

    /// <summary>
    /// Sets the values: Intensity/Radius/..
    /// </summary>
    private void SetBasicVFXValues()
    {
        //You can also flip the functionality by using SetFloat and then multiplying the values wihtin the VFXgraph using a multiply node.
        visualEffect.SetFloat("VFX_Intensity", audioDataSource.Amplitude * VFXIntensityMultiplier);
        visualEffect.SetFloat("VFX_Radius", audioDataSource.Amplitude * VFXRadiusMultiplier);
    }


    /// <summary>
    /// Adds dynamic gravity to the particles.
    /// </summary>
    private void ApplyVFXGravity()
    {
        //Im taking multiple frequencybands together not just 1
        float freqBand_0_to_1 = audioDataSource._freqBand[0] + audioDataSource._freqBand[1];
        float freqBand_3_to_4 = audioDataSource._freqBand[3] + audioDataSource._freqBand[4];

        float gravityStrenght = freqBand_0_to_1 * VFXGravityMinMultiplier + freqBand_3_to_4 * VFXGravityMaxMultiplier;

        Vector3 gravity = new Vector3(0, 0, gravityStrenght);

        visualEffect.SetVector3("VFX_Gravity", gravity);
    }
    #endregion
}