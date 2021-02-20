using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Models
{
    public class BirdModel
    {
        public int DistanceFromGround { get; private set; } = 100;
        public int JumpStrength { get; private set; } = 50;
        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }

        public bool IsOnGround()
        {
            return DistanceFromGround <= 0;
        }
        public void Jump()
        {
            if (DistanceFromGround <= 430)
                DistanceFromGround += JumpStrength;
        }
    }
}
