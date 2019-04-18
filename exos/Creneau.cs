using System;
using System.Collections.Generic;
using System.Text;

namespace exos
{
    public class Creneau
    {
        public readonly DateTime début;
        public readonly DateTime fin;

        public Creneau(DateTime date, TimeSpan durée)
        {
        //    if (date.Minute != 00 || date.Minute != 30)
        //        throw new HeureIncorrecteException();
            //if (date.Second != 00 && date.Millisecond != 00)
            //    throw new HeureIncorrecteException();
            this.début = date;
            if (durée < TimeSpan.FromHours(1))
                throw new DuréeMinimaleInvalideException();
            this.fin = date.Add(durée);
        }

        public override bool Equals(object obj)
        {
            if (obj is Creneau cr)
            {
                if(cr.début == début
                    && cr.fin == fin)
                {
                    return true;
                }
            }
            return false; 
        }
    }
}
