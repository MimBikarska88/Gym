using System;
using Gym.Utilities.Messages;

namespace Gym.Models.Athletes
{
    public class Boxer : Athlete
    {
        public Boxer(string fullName, string motivation, int medals) : base(fullName, motivation, medals, 60)
        {
        }

        public override int Stamina
        {
            get => stamina;
            protected set
            {
                if(value > 100)
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
            this.Stamina += 15;
        }
    }
}