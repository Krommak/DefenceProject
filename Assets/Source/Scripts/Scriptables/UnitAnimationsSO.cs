using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitAnimationsSO", menuName = "ScriptableObjects/Unit/UnitAnimations", order = 1)]
public class UnitAnimationsSO : ScriptableObject
{
    [SerializeField]
    AnimationObject[] animMaterials;

    public bool TryGetAnimByType(UnitAnimationType type, out Material material)
    {
        foreach (var item in animMaterials)
        {
            if (item.type == type)
            {
                material = item.material;
                return true;
            }
        }
        material = default;
        return false;
    }
}

[Serializable]
public class AnimationObject
{
    public UnitAnimationType type;
    public Material material;
}