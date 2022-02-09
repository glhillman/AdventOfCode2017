using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class Vector
    {
        public Vector (long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public bool IsEqualTo(Vector other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
        public override string ToString()
        {
            return String.Format("<{0},{1},{2}>", X, Y, Z);
        }
    }
    internal class Particle
    {
        public Particle(int id, Vector position, Vector velocity, Vector acceleration)
        {
            Id = id;
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Manhattan = Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
            IsDuplicate = false;
        }

        public int Id { get; private set; }
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Acceleration { get; set; }
        public long Manhattan { get; set; }
        public long DoTick()
        {
            Velocity.X += Acceleration.X;
            Velocity.Y += Acceleration.Y;
            Velocity.Z += Acceleration.Z;
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
            Manhattan = Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
            return Manhattan;
        }
        public bool IsEqualTo(Particle other)
        {
            return Position.IsEqualTo(other.Position);
        }
        public bool IsDuplicate { get; set; }
        public override string ToString()
        {
            return String.Format("Id: {0}, p={1}, v={2}>, a={3}, Man: {4}", Id, Position, Velocity, Acceleration, Manhattan);
        }
    }
}
