using System;
using System.Collections.Generic;

namespace SampleGame.App
{
    public interface ISaveLoadData
    {
        public Dictionary<string,string> SaveData();
        public void LoadData(Dictionary<string, string> properties);
    }
}