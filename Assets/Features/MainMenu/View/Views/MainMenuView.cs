﻿using Core.EventSystem;
using Features.MainMenu.Events;

namespace Features.MainMenu.View.Views
{
    public abstract class MainMenuView : Core.ViewSystem.View
    {
        protected override void RaiseViewCreatedEvent()
        {
            EventBus.Raise(new MainMenuViewCreatedEvent(this));
        }
    }
}