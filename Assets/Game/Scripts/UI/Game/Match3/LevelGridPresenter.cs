using System.Collections.Generic;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.Scripts.UI.Game.Match3;
using Game.System.Gameplay.Match3;

namespace Game.UI.Game.Match3
{
    public class LevelGridPresenter
    {
        private readonly LevelGrid _model;
        private readonly LevelGridView _view;
        private readonly Dictionary<ItemView,Item> _itemViews=new();
        
        public LevelGridPresenter(LevelGrid model, LevelGridView view)
        {
            _model = model;
            _view = view;
        }

        public Item GetItem(ItemView itemView)
        {
            return _itemViews[itemView];
        }
    }
}