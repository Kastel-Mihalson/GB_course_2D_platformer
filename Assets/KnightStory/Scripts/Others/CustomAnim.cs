using System.Collections.Generic;
using UnityEngine;

public class CustomAnim
{
    public AnimState animState;
    public List<Sprite> animSpritesSequence;

    public bool loop;
    public bool sleeps;

    public float speed = 10;
    public float counter;

    public void Update()
    {
        if (sleeps) return;

        counter += Time.deltaTime + speed;

        if (loop)
        {
            while (counter > animSpritesSequence.Count)
                counter -= animSpritesSequence.Count;
        } else if (counter > animSpritesSequence.Count)
        {
            counter = animSpritesSequence.Count - 1;
            sleeps = true;
        }
    }
}
