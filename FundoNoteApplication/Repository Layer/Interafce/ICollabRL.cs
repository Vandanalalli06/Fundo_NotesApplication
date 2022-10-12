using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interafce
{
    public interface ICollabRL
    {
        public CollabEntity AddCollab(long notesId, string email);
        public string DeleteCollab(long collabId, string email);
        public List<CollabEntity> GetCollab(long userId);
    }
}
