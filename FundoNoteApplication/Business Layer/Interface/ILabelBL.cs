using Repository_Layer.Entity;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long UserId, long NoteId, string labelName);
        public List<LabelEntity> GetLabel(long userId);
    }
}