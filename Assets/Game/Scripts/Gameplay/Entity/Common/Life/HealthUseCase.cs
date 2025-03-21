using System;
using Atomic.Elements;
using Atomic.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public static class HealthUseCase
    {
        public static bool IsAlive(in IEntity entity)
        {
            return entity.GetHealth().Value > 0;
        }
    }
}