using Core.Infrastructure.ViewController;
using UnityEngine;

namespace Features.Game.ViewModels
{
    public record MainCharacterViewModel(Vector3 Velocity) : IViewModel;
}