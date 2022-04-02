using UnityEngine;

public class SwitchQuestIntersection : IQuestIntersection
{
    public bool TryComplete(GameObject activator)
    {
        return activator.GetComponent<PlayerView>();
    }
}
