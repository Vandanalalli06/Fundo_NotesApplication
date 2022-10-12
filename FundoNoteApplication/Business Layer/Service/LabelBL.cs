using Business_Layer.Interface;
using Microsoft.Data.SqlClient.DataClassification;
using Repository_Layer.Entity;
using Repository_Layer.Interafce;
using Repository_Layer.Service;
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

        public LabelEntity Create(long userId, long noteId, string LabelName)
        {
            try
            {
                return labelRL.Create(userId, noteId, LabelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

       