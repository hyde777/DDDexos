using System;
using Commun;

namespace Model
{
    public class Recruteur
    {
        public readonly string nom;
        internal readonly Spécialité spécialité;
        public readonly TimeSpan expérience;

        internal Recruteur(string name, Spécialité spécialité, TimeSpan jourDexpérience)
        {
            this.nom = name;
            this.spécialité = spécialité;
            this.expérience = jourDexpérience;
        }

        public override bool Equals(object obj)
        {
            if(obj is Recruteur r)
                if (r.nom == nom 
                    && r.spécialité == spécialité
                    && r.expérience == expérience)
                    return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}