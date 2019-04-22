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
        private string raison;

        internal Creneau creneau { get; private set; }

        public Entretien(int id,
                         CreneauDto creneau,
                         EntretienStatut statut,
                         CandidatDto candidat,
                         RecruteurDto recruteur,
                         SalleDto salle)
                            : this(id,
                                   new Creneau(creneau.date, creneau.duree),
                                   statut,
                                   new Candidat(candidat.name, candidat.specialite, candidat.experience),
                                   new Recruteur(recruteur.name, recruteur.specialite, recruteur.experience),
                                   new Salle(salle.name, salle.statut))
        {
            
        }

        internal Entretien(int id,
                          Creneau creneau,
                          EntretienStatut statut,
                          Candidat candidat,
                          Recruteur recruteur,
                          Salle salle)
        {
            if (recruteur.specialite != candidat.specialite)
                throw new SpecialiteIncompatibleException();

            if (recruteur.experience <= candidat.experience)
                throw new RecruteurSousExperimenterException();

            if (salle.statut == SalleStatut.Occupee)
                throw new SalleOccuperException();

            this.id = id;
            this.creneau = creneau;
            this.statut = statut;
            this.recruteur = recruteur;
            this.candidat = candidat;
            this.salle = salle;
        }

        public void Confirmer()
        {
            this.statut = EntretienStatut.Confirmer;
        }

        public void Annuler(RaisonDto raison)
        {
            this.raison = raison.raison;
            this.statut = EntretienStatut.Annuler;
        }

        public void Replanifier(CreneauDto creneau)
        {
            this.creneau = new Creneau(creneau.date, creneau.duree);
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

        public bool PeutPreceder(Entretien autreEntretien)
        {
            if (autreEntretien.creneau.fin <= this.creneau.debut
                && this.candidat == autreEntretien.candidat)
                return true;
            return false;
        }

        public bool PeutSuivre(Entretien autreEntretien)
        {
            if (autreEntretien.creneau.debut >= this.creneau.fin
                && this.candidat == autreEntretien.candidat)
                return true;
            return false;
        }

        public bool PeutSePlanifierParRapport(Entretien entretien)
        {
            if (entretien.creneau == creneau)
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
