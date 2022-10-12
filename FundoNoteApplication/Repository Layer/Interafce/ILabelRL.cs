using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interafce
{
    public interface ILabelRL
    {
        public LabelEntity Create(long userId, long noteId, string labelName);
    }
}
