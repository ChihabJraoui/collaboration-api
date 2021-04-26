using Collaboration.ShareDocs.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class DateTimeRepository:IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
