using System;

public class Quest : IQuest, IDisposable
{
    private readonly QuestObjectView _view;
    private readonly IQuestIntersection _intersection;

    private bool _active;

    public bool IsCompleted { get; private set; }
    public event Action<IQuest> Completed;

    public Quest(QuestObjectView view, IQuestIntersection intersection)
    {
        _view = view;
        _intersection = intersection;
    }

    public void Dispose()
    {
        _view.OnLevelObjectContact -= OnContact;
    }

    public void Reset()
    {
        if (_active)
            return;

        _active = true;
        IsCompleted = false;
        _view.OnLevelObjectContact += OnContact;
        _view.ProcessActivate();
    }

    private void OnContact(PlayerView playerView)
    {
        var complete = _intersection.TryComplete(playerView.gameObject);

        if (complete)
            Complete();
    }

    private void Complete()
    {
        if (!_active)
            return;

        _active = false;
        IsCompleted = true;
        _view.ProcessComplete();
        Completed?.Invoke(this);

        Dispose();
    }
}
