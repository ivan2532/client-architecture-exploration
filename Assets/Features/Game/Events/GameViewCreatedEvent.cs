using Core.ViewSystem;
using UnityEngine;

namespace Features.Game.Events
{
    public record GameViewCreatedEvent(MonoBehaviour View) : IViewCreatedEvent
    {
        public MonoBehaviour GetView()
        {
            return View;
        }
    }
}