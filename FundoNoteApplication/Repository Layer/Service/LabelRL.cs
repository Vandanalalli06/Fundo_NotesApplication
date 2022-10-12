using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interafce;
using System;
using System.Collections.Generic;
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
        public LabelEntity Create(long userId, long noteId, string labelName)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.UserId = userId;
                label.NotesId = noteId;
                label.LabelName = labelName;
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
    }
}


