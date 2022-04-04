using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsConfigurator : MonoBehaviour
{
    [SerializeField]
    private QuestStoryConfig[] _questStoryConfigs;

    [SerializeField]
    private QuestObjectView[] _questObjects;

    [SerializeField]
    private QuestDoorView _questCompleteAction;

    [SerializeField]
    private QuestObjectView _simpleQuestView;

    private Quest _simpleQuest;

    private List<IQuestStory> _questStories;

    private readonly Dictionary<QuestType, Func<IQuestIntersection>> _questFactory = 
        new Dictionary<QuestType, Func<IQuestIntersection>>
    {
        {QuestType.Switch, () => new SwitchQuestIntersection() }
    };

    private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactory;

    private void Start()
    {
        _questStoryFactory = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>
        {
            {QuestStoryType.Common, questsCollection => new QuestStory(questsCollection, _questCompleteAction) }
        };

        _simpleQuest = new Quest(_simpleQuestView, new SwitchQuestIntersection());
        _simpleQuest.Reset();

        _questStories = new List<IQuestStory>();

        foreach (var storyConfig in _questStoryConfigs)
            _questStories.Add(CreateQuestStory(storyConfig));
    }

    private void OnDestroy()
    {
        _simpleQuest.Dispose();
    }

    private IQuestStory CreateQuestStory(QuestStoryConfig storyConfig)
    {
        var quests = new List<IQuest>();
        foreach (var questConfig in storyConfig.Quests)
        {
            var quest = CreateQuest(questConfig);

            if (quest == null)
                continue;

            quests.Add(quest);
        }

        return _questStoryFactory[storyConfig.QuestStoryType].Invoke(quests);
    }

    private IQuest CreateQuest(QuestConfig config)
    {
        var questView = _questObjects.FirstOrDefault(value => value.Id == config.Id);

        if (questView == null)
        {
            Debug.LogError("Not questView");
            return null;
        }

        if (_questFactory.TryGetValue(config.QuestType, out var factory))
        {
            var questModel = factory.Invoke();
            return new Quest(questView, questModel);
        }

        return null;
    }
}
