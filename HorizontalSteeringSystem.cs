using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// TODO
    /// 1. [ ] Horizontal Steering System
    ///     1. [ ] X Axis Dead Zone.
    ///         1. [ ] Listens to Key Input System On Key Down X Y.
    ///         1. [ ] Listens to Click System On Axis X Y.
    ///         1. [ ] If X in Dead Zone, stop here.
    ///         1. [ ] If reset X time remaining, stop here.
    ///         1. [ ] Publish On Steer X.
    ///     1. [ ] Reset X Time
    ///         1. [ ] Afterward reset X to zero.
    ///         1. [ ] Listens to pause system delta time.
    /// </summary>
    [Serializable]
    public sealed class HorizontalSteeringSystem : ASingleton<HorizontalSteeringSystem>
    {
        public static event Action<float> onSteerX;
    }
}
