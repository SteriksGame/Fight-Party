﻿using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FightParty.Game
{
    public class EventSlider : Slider
    {
        public event Action PointerUp; 

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            PointerUp?.Invoke();
        }
    }
}
