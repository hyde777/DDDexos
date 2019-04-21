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

        public AnnulerUnEntretien(int id, string raison)
        {
            this.id = id;
            this.raison = raison;
        }

        public IEnumerable<Entretien> Execute(List<Entretien> enume)
        {
            if(enume.Exists(ent => ent.id == id))
            {
                Entretien entretien = enume.Find(ent => ent.id == id);
                entretien.Annuler(raison);
                enume.Remove(entretien);
                enume.Add(entretien);
                return enume;
            }
            else throw new EntretienNonExistantException();
        }
    }
}
