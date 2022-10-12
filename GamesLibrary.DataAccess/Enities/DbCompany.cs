using System;
using System.Collections.Generic;

namespace GamesLibrary.DataAccess.Enities
{
    public partial class DbCompany
    {
        public DbCompany()
        {
            Games = new HashSet<DbGame>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DbGame> Games { get; set; }
    }
}
