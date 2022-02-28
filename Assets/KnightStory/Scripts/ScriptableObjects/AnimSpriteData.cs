using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimSpriteData", menuName = "ScriptableObjects/AnimSpriteData", order = 1)]
public class AnimSpriteData : ScriptableObject
{
    [SerializeField]
    private List<AnimSpritesSequence> _animSpritesSequences;

    public List<AnimSpritesSequence> AnimSpritesSequences => _animSpritesSequences;
}
