using Core.ViewSystem;
using UnityEngine;

namespace Features.MainMenu.Events
{
    public record MainMenuViewCreatedEvent(MonoBehaviour View) : IViewCreatedEvent
    {
        public MonoBehaviour GetView()
        {
            return View;
        }
    }
}