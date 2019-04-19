using System;
using System.Collections.Generic;
using System.Text;

namespace exos
{
    public class Entretien
    {
        private readonly EntretienID id;
        public EntretienStatut statut;
        public readonly Candidat candidat;
        private readonly Salle salle;
        private Recruteur recruteur;

        public Creneau créneau { get; private set; }

        public Entretien(EntretienID id, Creneau créneau, EntretienStatut statut, Candidat candidat, Recruteur recruteur, Salle salle)
        {
            if (recruteur.spécialité != candidat.spécialité)
                throw new SpécialitéIncompatibleException();

            if (recruteur.jourDexpérience <= candidat.jourDexpérience)
                throw new RecruteurSousExpérimenterException();

            this.id = id;
            this.créneau = créneau;
            this.statut = statut;
            this.recruteur = recruteur;
            this.candidat = candidat;
            this.salle = salle;
        }

        public void Confirmer()
        {
            this.statut = EntretienStatut.Confirmer;
        }

        public void Annuler(Raison raison)
        {
            this.statut = EntretienStatut.Annuler;
        }

        public void Replanifier(Creneau creneau)
        {
            créneau = creneau;
            statut = EntretienStatut.Replanifier;
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

        public bool PeutPrécéder(Entretien autreEntretien)
        {
            if (autreEntretien.créneau.fin <= this.créneau.début
                && this.candidat == autreEntretien.candidat)
                return true;
            return false;
        }

        public bool PeutSuivre(Entretien autreEntretien)
        {
            if (autreEntretien.créneau.début >= this.créneau.fin
                && this.candidat == autreEntretien.candidat)
                return true;
            return false;
        }

        public bool PeutSePlanifierParRapport(Entretien entretien)
        {
            if (entretien.créneau == créneau)
            {
                if (entretien.candidat == candidat)
                    return false;

                if (entretien.recruteur == recruteur)
                    return false;

                if (entretien.salle == salle)
                    return false;
            }
            return true;
        }
    }
}
