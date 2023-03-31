using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resolution", menuName = "Screen/Resolution")]
public class BaseResolution : ScriptableObject
{
    public ResolutionWrapper[] ResolutionWrapper;

    public ResolutionWrapper CurrentResolution(int number)
    {
        return ResolutionWrapper[number];
    }
}

[System.Serializable]
public class ResolutionWrapper
{
    public ResolutionType ResolutionType;
    public int Width;
    public int Height;
}

[System.Serializable]
public enum ResolutionType
{
    StandardDefinition = 0,
    HighDefinition = 1,
    FullHD = 2,
    QuadHD = 3,
    UltraHD = 4,
    FullUltraHD = 5,
}
