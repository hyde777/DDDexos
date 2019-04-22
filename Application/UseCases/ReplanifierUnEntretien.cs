using Commun.Dto;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class ReplanifierUnEntretien : IUseCase<Entretien>
    {
        private int id;
        private CreneauDto creneau;
        public ReplanifierUnEntretien(IGenerateur<int> generateur, CreneauDto creneau)
        {
            this.id = generateur.GetNewId();
            this.creneau = creneau;
        }

        public IEnumerable<Entretien> Execute(List<Entretien> enume)
        {
            if(enume.Exists(ent => ent.id == id))
            {
                Entretien entretien = enume.Find(ent => ent.id == id);
                enume.Remove(entretien);
                entretien.Replanifier(creneau);
                enume.Add(entretien);
                return enume;
            }
            else throw new EntretienNonExistantException();
        }
    }
}
