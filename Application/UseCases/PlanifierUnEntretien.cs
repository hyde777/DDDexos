﻿using System;
using Model;
using Commun.Dto;
using Commun;
using System.Collections.Generic;
using System.Linq;


namespace Application
{
    public class PlanifierUnEntretien : IUseCase<Entretien>
    {
        private Entretien entretien;
        public PlanifierUnEntretien(IGenerateur<int> generateur,
                                    CreneauDto creneau, 
                                    CandidatDto candidat, 
                                    RecruteurDto recruteur,
                                    SalleDto salle)
        {
            entretien = new Entretien(generateur.GetNewId(),
                                      creneau,
                                      EntretienStatut.Planifier,
                                      candidat,
                                      recruteur,
                                      salle);
        }

        public IEnumerable<Entretien> Execute(List<Entretien> entretiens)
        {
            if (entretiens.Exists(ent => entretien.Equals(ent)))
                return entretiens;
            return entretiens.Append(entretien);
        }
    }
}
