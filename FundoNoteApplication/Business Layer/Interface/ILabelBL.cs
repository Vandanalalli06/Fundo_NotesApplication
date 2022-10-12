using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    internal interface ILabelBL
    {
        public LabelEntity Create(long userId, long noteId, string labelName);
    }
}
