using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        protected string name;
        protected int capacity;
        protected List<IEquipment> _equipment;
        protected List<IAthlete> _athletes;

        protected Gym(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            _equipment = new List<IEquipment>();
            _athletes = new List<IAthlete>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }

                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            set => capacity = value;
        }

        public double EquipmentWeight
        {
            get => _equipment.Select(equipment => equipment.Weight).Sum();
        }

        public ICollection<IEquipment> Equipment
        {
            get => _equipment;
        }
        public ICollection<IAthlete> Athletes
        {
            get => _athletes;
        }
        public void AddAthlete(IAthlete athlete)
        {
            if (this.Athletes.Count == capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
            _athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this._athletes.Remove(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            _equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in _athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{this.Name} is a {this.GetType().Name}:" + Environment.NewLine);
            string athletes = "No athletes";
            if (_athletes.Count == 0)
            {
                stringBuilder.Append(athletes + Environment.NewLine);
            }
            else
            {
                athletes = string.Join(", ", _athletes.Select(athlete => athlete.FullName).ToList());
                stringBuilder.Append($"Athletes: {athletes}" + Environment.NewLine);
            }

            stringBuilder.Append($"Equipment total count: {_equipment.Count}" + Environment.NewLine);
            stringBuilder.Append($"Equipment total weight: {EquipmentWeight:F2} grams" + Environment.NewLine);
            return stringBuilder.ToString().TrimEnd();
        }
    }
}