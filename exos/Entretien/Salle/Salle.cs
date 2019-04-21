using System;
using Commun;

namespace Model
{
    public class Salle
    {
        public readonly string nameId;
        internal readonly SalleStatut statut;

        internal Salle(string nameId, SalleStatut statut)
        {
            this.nameId = nameId;
            this.statut = statut;
        }

        public override bool Equals(object obj)
        {
            if(obj is Salle s)
                if (s.nameId == nameId)
                    return true;
            
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
