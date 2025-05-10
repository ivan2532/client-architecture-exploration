using Features.Game.Model;
using UnityEngine;

namespace Features.Game.Mappers
{
    public static class VelocityToVector3Mapper
    {
        public static Vector3 Map(Velocity velocity)
        {
            return new Vector3(velocity.X, velocity.Y, velocity.Z);
        }
    }
}