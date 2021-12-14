using System;
using Gym.Utilities.Messages;

namespace Gym.Models.Athletes
{
    public class Weightlifter : Athlete
    {
        public Weightlifter(string fullName, string motivation, int medals) : base(fullName, motivation, medals, 50)
        {
        }

        public override int Stamina
        {
            get => this.stamina;
            protected set
            {
                if (value > 100)
                {
                    stamina = 100;
                    throw new ArgumentException(ExceptionMessages.InvalidStamina);
                }
                else
                {
                    stamina = value;
                }
            }
        }

        public override void Exercise()
        {
            this.Stamina += 10;
        }
    }
}