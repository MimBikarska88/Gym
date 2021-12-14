using System;
using Gym.Models.Athletes.Contracts;
using Gym.Utilities.Messages;

namespace Gym.Models.Athletes
{
    public abstract class Athlete : IAthlete
    {
        protected string fullName;
        protected string motivation;
        protected int medals;
        protected int stamina;

        protected Athlete(string fullName, string motivation, int medals, int stamina)
        {
            this.FullName = fullName;
            this.Motivation = motivation;
            this.NumberOfMedals = medals;
            this.Stamina = stamina;
        }
        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteName);
                }

                fullName = value;
            }
        }

        public string Motivation
        {
            get => motivation;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteMotivation);
                }

                motivation = value;
            }
        }

        public virtual int Stamina
        {
            get => stamina;
            protected set
            {
                stamina = value;
            }
        }

        public int NumberOfMedals
        {
            get => medals;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteMedals);
                }

                medals = value;
            }
        }

        public abstract void Exercise();
    }
}