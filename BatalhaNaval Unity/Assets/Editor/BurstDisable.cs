using Unity.Burst;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
public static class BurstDisable
{
    static BurstDisable()
    {
        BurstCompiler.Options.EnableBurstCompilation = false;
        BurstCompiler.Options.EnableBurstSafetyChecks = false;

        Debug.Log("Burst compilation desativado para evitar erro de resolução de assembly da build do projeto.");
    }
}
#endif
