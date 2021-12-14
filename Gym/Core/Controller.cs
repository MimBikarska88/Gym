using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Equipment;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipmentRepository;
        private List<IGym> Gyms;

        public Controller()
        {
            equipmentRepository = new EquipmentRepository();
            Gyms = new List<IGym>();
        }
        public string AddGym(string gymType, string gymName)
        {
            switch (gymType)
            {
                case "BoxingGym": Gyms.Add(new BoxingGym(gymName));
                    return string.Format(OutputMessages.SuccessfullyAdded, gymType);
                case"WeightliftingGym": Gyms.Add(new WeightliftingGym(gymName));
                    return string.Format(OutputMessages.SuccessfullyAdded, gymType);
                default: throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }
        }

        public string AddEquipment(string equipmentType)
        {
            switch (equipmentType)
            {
                case"Kettlebell" : equipmentRepository.Add(new Kettlebell());
                    return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
                case"BoxingGloves": equipmentRepository.Add(new BoxingGloves());
                    return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
                default: throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var equipment = equipmentRepository.FindByType(equipmentType);
            if (equipment == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment,
                    equipmentType));
            }

            var gym = Gyms.FirstOrDefault(gym_ => gym_.Name == gymName);
            gym.AddEquipment(equipment);
            equipmentRepository.Remove(equipment);
            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            var gym = Gyms.FirstOrDefault(gym1 => gym1.Name == gymName);
            switch (athleteType)
            {
                case"Boxer":
                    if (gym.GetType().Name == "BoxingGym")
                    {
                        gym.AddAthlete(new Boxer(athleteName,motivation,numberOfMedals));
                        return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
                    }
                    else
                    {
                        return OutputMessages.InappropriateGym;
                    }
                case"Weightlifter":
                    if (gym.GetType().Name == "WeightliftingGym")
                    {
                        gym.AddAthlete(new Weightlifter(athleteName,motivation,numberOfMedals));
                        return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
                    }
                    else
                    {
                        return OutputMessages.InappropriateGym;
                    }
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
        }

        public string TrainAthletes(string gymName)
        {
            var gym = Gyms.FirstOrDefault(gym1 => gym1.Name == gymName);
            gym.Exercise();
            return string.Format(OutputMessages.AthleteExercise,gym.Athletes.Count);
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = Gyms.FirstOrDefault(gym1 => gym1.Name == gymName);
            double weight = gym.EquipmentWeight;
            return string.Format(OutputMessages.EquipmentTotalWeight,gymName,weight);
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var gym in Gyms)
            {
                stringBuilder.Append(gym.GymInfo() + Environment.NewLine);
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}