using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Models
{
    public struct DroneModel
    {
        public Vector3 Position { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }

        public DroneViewModel CreateViewModel()
        {
            return new DroneViewModel(Position, Pitch, Yaw);
        }
    }
}