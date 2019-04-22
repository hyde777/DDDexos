using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class GenerateurDidentifiant : IGenerateur<int>
    {
        static int ids = 0;

        public int GetNewId()
        {
            return ids++;
        }
    }
}
