using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game.Quest
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private Text questProgress;
        [SerializeField] private Image questTask;
        
        public Vector2 Position => questTask.transform.position;
        
        public void SetProgress(string progress)
        {
            questProgress.text = progress;
        }
        
        public void SetQuestTask(Sprite task)
        {
            questTask.sprite = task;
        }

        public void Bounce()
        {
            questTask.transform.DOKill(true);
            questProgress.transform.DOKill(true);
            
            questTask.transform.DOPunchScale(Vector3.one*0.5f, 0.5f);
            questProgress.transform.DOPunchScale(Vector3.one*0.5f, 0.5f);
        }
    }
}