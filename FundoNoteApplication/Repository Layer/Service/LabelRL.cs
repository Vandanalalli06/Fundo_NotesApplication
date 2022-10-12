using Repository_Layer.Context;
using Repository_Layer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundoContext fundoContext;

        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.UserId = UserId;
                label.NotesId = NoteId;
                label.LabelName = LabelName;
                fundoContext.Add(label);
                int res = fundoContext.SaveChanges();
                if (res > 0)
                {
                    return label;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LabelEntity> GetLabel(long UserId)
        {
            try
            {
                var result = fundoContext.LabelTable.Where(u => u.UserId == UserId).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
    
            
