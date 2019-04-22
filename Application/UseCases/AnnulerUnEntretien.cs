using Commun.Dto;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class AnnulerUnEntretien : IUseCase<Entretien>
    {
        private int id;
        private string raison;

        public AnnulerUnEntretien(IGenerateur<int> generateur, string raison)
        {
            this.id = generateur.GetNewId();
            this.raison = raison;
        }

        public IEnumerable<Entretien> Execute(List<Entretien> enume)
        {
            if(enume.Exists(ent => ent.id == id))
            {
                Entretien entretien = enume.Find(ent => ent.id == id);
                entretien.Annuler(new RaisonDto { raison = this.raison });
                enume.Remove(entretien);
                enume.Add(entretien);
                return enume;
            }
            else throw new EntretienNonExistantException();
        }
    }
}
