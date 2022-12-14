using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollab(long notesId, string email);
        public string DeleteCollab(long collabId, string email);
        public List<CollabEntity> GetCollab(long userId);
    }
}
