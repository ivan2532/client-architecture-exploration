using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Models
{
    public struct MainCharacterModel
    {
        public Vector3 Velocity { get; set; }

        public MainCharacterViewModel CreateViewModel()
        {
            return new MainCharacterViewModel(Velocity);
        }
    }
}