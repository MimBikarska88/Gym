using System.Collections.Generic;
using System.Linq;
using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;

namespace Gym.Repositories
{
    public class EquipmentRepository: IRepository<IEquipment>
    {
        private List<IEquipment> models;

        public EquipmentRepository()
        {
            this.models = new List<IEquipment>();
        }
        public IReadOnlyCollection<IEquipment> Models
        {
            get => models;
        }

        public void Add(IEquipment model) => models.Add(model);

        public bool Remove(IEquipment model) => models.Remove(model);

        public IEquipment FindByType(string type) => Models.FirstOrDefault(model => model.GetType().Name == type);
    }
}