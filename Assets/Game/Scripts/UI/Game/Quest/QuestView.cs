using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Game.Quest
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private Text questProgress;
        [SerializeField] private Image questTask;
        
        public void SetProgress(string progress)
        {
            questProgress.text = progress;
        }
        
        public void SetQuestTask(Sprite task)
        {
            questTask.sprite = task;
        }
    }
}