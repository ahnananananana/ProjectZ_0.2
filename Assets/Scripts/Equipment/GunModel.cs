using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class GunModel : ObjectModel
    {
        public Transform Muzzle, LeftHandRef, RightHandRef;
        public ParticleSystem ShootParticle;
    }
}