using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commun
{
    public abstract class Component:AuditableEntity
    {
        public string Name { get; set; }
        public Folder Parent { get; set; }

        public Component(string name)
        {
            this.Name = name;
        }
        

    }
}
