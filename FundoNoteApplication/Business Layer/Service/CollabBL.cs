using Business_Layer.Interface;
using Repository_Layer.Entity;
using Repository_Layer.Interafce;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public CollabEntity AddCollab(long notesId, string email)
        {
            try
            {
                return collabRL.AddCollab(notesId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteCollab(long collabId, string email)
        {
            try
            {
                return collabRL.DeleteCollab(collabId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CollabEntity> GetCollab(long userId)
        {
            try
            {
                return collabRL.GetCollab(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}




