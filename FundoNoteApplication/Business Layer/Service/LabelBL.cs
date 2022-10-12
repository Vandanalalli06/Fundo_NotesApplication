using BusinessLayer.Interface;
using Repository_Layer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                return labelRL.CreateLabel(UserId, NoteId, LabelName);
            }
            catch (Exception )
            {
                throw ;
            }
        }
    }
}
