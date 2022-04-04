using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestStory : IQuestStory
{
    private readonly List<IQuest> _questsCollection;
    private QuestDoorView _questCompleteActionView;

    public bool IsDone => _questsCollection.All(value => value.IsCompleted);

    public QuestStory(List<IQuest> questsCollection, QuestDoorView questDoorView)
    {
        _questCompleteActionView = questDoorView;
        _questsCollection = questsCollection;
        Subscribe();
        ResetQuest(0);
    }

    public void Dispose()
    {
        UnSubscribe();
    }

    private void Subscribe()
    {
        foreach (var quest in _questsCollection)
            quest.Completed += OnQuestCompleted;
    }

    private void UnSubscribe()
    {
        foreach (var quest in _questsCollection)
            quest.Completed -= OnQuestCompleted;
    }

    private void OnQuestCompleted(IQuest quest)
    {
        var index = _questsCollection.IndexOf(quest);

        if (IsDone)
        {
            Debug.Log("Story done!");
            QuestCompleteAction();
        }   
        else
            ResetQuest(++index);
    }

    private void ResetQuest(int index)
    {
        if (index < 0 || index >= _questsCollection.Count)
            return;

        var nextQuest = _questsCollection[index];

        if (nextQuest.IsCompleted)
            OnQuestCompleted(nextQuest);
        else
            nextQuest.Reset();
    }

    private void QuestCompleteAction()
    {
        Object.Destroy(_questCompleteActionView.gameObject);
    }
}
