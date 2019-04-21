using System;
using Model;
using Commun.Dto;
using Commun;

namespace Application
{
    public class PlanifierUnEntretien
    {
        static int compteur = 1;
        private Entretien entretien;
        public PlanifierUnEntretien(CréneauDto créneau, 
                                    CandidatDto candidat, 
                                    RecruteurDto recruteur,
                                    SalleDto salle)
        {
            entretien = new Entretien(compteur,
                                      créneau,
                                      EntretienStatut.Planifier,
                                      candidat,
                                      recruteur,
                                      salle);
            compteur++;
        }
    }
}
