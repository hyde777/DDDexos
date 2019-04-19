using System;

namespace exos
{
    public class Candidat
    {
        public readonly string nom;
        internal readonly Spécialité spécialité;
        public readonly TimeSpan jourDexpérience;

        internal Candidat(string nom, Spécialité spécialité, TimeSpan jourDexpérience)
        {
            this.nom = nom;
            this.spécialité = spécialité;
            this.jourDexpérience = jourDexpérience;
        }

        public override bool Equals(object obj)
        {
            if (obj is Candidat c)
                if (c.nom == nom
                    && c.spécialité == spécialité
                    && c.jourDexpérience == jourDexpérience)
                    return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}