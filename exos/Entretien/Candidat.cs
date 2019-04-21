using System;
using Commun;

namespace Model
{
    internal class Candidat
    {
        public readonly string nom;
        internal readonly Spécialité spécialité;
        public readonly TimeSpan expérience;

        internal Candidat(string nom, Spécialité spécialité, TimeSpan jourDexpérience)
        {
            this.nom = nom;
            this.spécialité = spécialité;
            this.expérience = jourDexpérience;
        }

        public override bool Equals(object obj)
        {
            if (obj is Candidat c)
                if (c.nom == nom
                    && c.spécialité == spécialité
                    && c.expérience == expérience)
                    return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}