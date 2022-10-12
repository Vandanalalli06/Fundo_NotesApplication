using Repository_Layer.Entity;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long UserId, long NoteId, string labelName);
        public List<LabelEntity> GetLabel(long userId);
        public LabelEntity UpdateLabel(long labelId, string newLabelName);
        public string LabelDelete(long labelId, long noteId);
    }
}