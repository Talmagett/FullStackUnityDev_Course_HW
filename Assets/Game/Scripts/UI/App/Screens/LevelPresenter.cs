using UnityEngine;
using Game.UI.App.Screens;
using Game.UI.App.Level;
using Game.App.Levels;
using System;
public class LevelPresenter : MonoBehaviour
{
    public event Action<LevelConfig> OnLevelSelected;
    [SerializeField] private LevelView _levelView;
    public LevelConfig LevelConfig{private get; set;}
    
    private Sprite openedLevelSprite;
    private Sprite lockedLevelSprite;
    private Sprite completedStarSprite;
    private Sprite emptyStarSprite;

    void OnEnable()
    {
        _levelView.OnLevelButtonClicked += OnLevelButtonClicked;
    }

    void OnDisable()
    {
        _levelView.OnLevelButtonClicked -= OnLevelButtonClicked;
    }

    private void OnLevelButtonClicked()
    {
        OnLevelSelected?.Invoke(LevelConfig);
    }

    internal void InitImages(Sprite openedLevelSprite, Sprite lockedLevelSprite, Sprite completedStarSprite, Sprite emptyStarSprite)
    {
        this.openedLevelSprite = openedLevelSprite;
        this.lockedLevelSprite = lockedLevelSprite;
        this.completedStarSprite = completedStarSprite;
        this.emptyStarSprite = emptyStarSprite;
    }

    internal void SetState(bool isInteractable, bool isCurrent)
    {
        _levelView.SetInteractable(isInteractable);
        _levelView.PlayBounce(isCurrent);
        _levelView.SetLevelImage(isInteractable ? openedLevelSprite : lockedLevelSprite);
        _levelView.SetStarImage(isInteractable&&!isCurrent ? completedStarSprite : emptyStarSprite);
    }
}