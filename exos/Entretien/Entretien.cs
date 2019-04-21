using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Commun.Dto;
using Commun;

[assembly: InternalsVisibleTo("ModelTest")]
namespace Model
{
    public class Entretien
    {
        public readonly int id;
        public EntretienStatut statut { get; private set; }
        internal readonly Candidat candidat;
        private readonly Salle salle;
        private Recruteur recruteur;
        private Raison raison;

        internal Creneau créneau { get; private set; }

        public Entretien(int id,
                         CréneauDto créneau,
                         EntretienStatut statut,
                         CandidatDto candidat,
                         RecruteurDto recruteur,
                         SalleDto salle)
                            : this(id,
                                   new Creneau(créneau.date, créneau.durée),
                                   statut,
                                   new Candidat(candidat.name, candidat.spécialité, candidat.expérience),
                                   new Recruteur(recruteur.name, recruteur.spécialité, recruteur.expérience),
                                   new Salle(salle.name, salle.statut))
        {
            
        }

        internal Entretien(int id,
                          Creneau créneau,
                          EntretienStatut statut,
                          Candidat candidat,
                          Recruteur recruteur,
                          Salle salle)
        {
            if (recruteur.spécialité != candidat.spécialité)
                throw new SpécialitéIncompatibleException();

            if (recruteur.expérience <= candidat.expérience)
                throw new RecruteurSousExpérimenterException();

            if (salle.statut == SalleStatut.Occupée)
                throw new SalleOccuperException();

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

        public void Annuler(string raisonName)
        {
            this.raison = new Raison(raisonName);
            this.statut = EntretienStatut.Annuler;
        }

        public void Replanifier(CréneauDto creneau)
        {
            créneau = new Creneau(creneau.date, creneau.durée);
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
