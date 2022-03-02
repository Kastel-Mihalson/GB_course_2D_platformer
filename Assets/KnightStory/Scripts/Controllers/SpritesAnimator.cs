using System.Collections.Generic;
using UnityEngine;

public class SpritesAnimator
{
    private AnimSpriteData _animSpriteData;
    private Dictionary<SpriteRenderer, CustomAnim> _activeAnim = new Dictionary<SpriteRenderer, CustomAnim>();

    public SpritesAnimator(AnimSpriteData animSpriteData)
    {
        _animSpriteData = animSpriteData;
    }

    private bool _endAnim;

    public bool EndAnim
    {
        get => _endAnim;
        set => _endAnim = value;
    }

    public void StartAnim(SpriteRenderer spriteRenderer, AnimState animState, bool loop, float speed)
    {
        if (_activeAnim.TryGetValue(spriteRenderer, out CustomAnim animation))
        {
            animation.loop = loop;
            animation.speed = speed;
            animation.sleeps = false;

            if (animation.endAnim) EndAnim = true;
            if (animation.animState == animState) return;

            animation.animState = animState;
            animation.animSpritesSequence = _animSpriteData.AnimSpritesSequences.Find(i => i.animState == animState).animSpriteSequence;
            animation.counter = 0;
        } else
        {
            _activeAnim.Add(spriteRenderer, new CustomAnim
            {
                animState = animState,
                animSpritesSequence = _animSpriteData.AnimSpritesSequences.Find(i => i.animState == animState).animSpriteSequence,
                loop = loop,
                speed = speed
            });
        }
    }

    public void StopAnim(SpriteRenderer spriteRenderer)
    {
        if (_activeAnim.ContainsKey(spriteRenderer))
        {
            _activeAnim.Remove(spriteRenderer);
        }
    }

    public void Update()
    {
        foreach (var anim in _activeAnim)
        {
            anim.Value.Update();
            anim.Key.sprite = anim.Value.animSpritesSequence[(int)anim.Value.counter];
        }
    }

    public void Dispose()
    {
        _activeAnim.Clear();
    }
}
