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
        private CréneauDto créneau;
        public ReplanifierUnEntretien(int id, CréneauDto créneau)
        {
            this.id = id;
            this.créneau = créneau;
        }

        public IEnumerable<Entretien> Execute(List<Entretien> enume)
        {
            if(enume.Exists(ent => ent.id == id))
            {
                Entretien entretien = enume.Find(ent => ent.id == id);
                enume.Remove(entretien);
                entretien.Replanifier(créneau);
                enume.Add(entretien);
                return enume;
            }
            else throw new EntretienNonExistantException();
        }
    }
}
