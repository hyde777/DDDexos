using System;
using System.Collections.Generic;
using System.Text;

namespace exos
{
    public class Entretien
    {
        private readonly EntretienID id;
        private Creneau créneau;
        public EntretienStatut statut;
        private Candidat candidat;
        private Recruteur recruteur;

        public Entretien(EntretienID id, Creneau créneau, EntretienStatut statut, Candidat candidat, Recruteur recruteur)
        {
            this.id = id;
            this.créneau = créneau;
            this.statut = statut;
            this.recruteur = recruteur;
            this.candidat = candidat;
        }

        public void confirmer()
        {
            this.statut = EntretienStatut.Confirmer;
        }

        public void annuler(Raison raison)
        {
            this.statut = EntretienStatut.Annuler;
        }

        public override bool Equals(object obj)
        {
            if(obj is Entretien ent)
            {
                if (ent.id.Equals(id))
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
