using System;

namespace Model
{
    internal class Creneau
    {
        public readonly DateTime debut;
        public readonly DateTime fin;

        public Creneau(DateTime date, TimeSpan duree)
        {
            this.debut = date;
            if (!Enum.IsDefined(typeof(AcceptableMinutes), this.debut.Minute))
                throw new HeureIncorrecteException();
            if (duree < TimeSpan.FromHours(1))
                throw new DureeMinimaleInvalideException();
            this.fin = date.Add(duree);
            if (!Enum.IsDefined(typeof(AcceptableMinutes), this.fin.Minute))
                throw new HeureIncorrecteException();
        }

        public override bool Equals(object obj)
        {
            if (obj is Creneau cr)
            {
                if(cr.debut == debut
                    && cr.fin == fin)
                {
                    return true;
                }
            }
            return false; 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
