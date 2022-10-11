using Common_Layer.Model;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interafce
{
    public interface INotesRL
    {
        public NotesEntity Create(NotesModel notes, long UserId);
        public NotesEntity Update(NotesModel notesUpdate, long UserId, long NoteId);
        public List<NotesEntity> NotesRetrieve(long UserId);
        public bool NoteDelete(long UserId, long NoteId);
        public NotesEntity NotePin(long NoteId, long UserId);
        public NotesEntity NoteArchive(long UserId, long NoteId);
        public bool NoteTrash(long UserId, long NoteId);
        public NotesEntity NoteColourChange(long notesId, string Colour);
        public string Image(long userId, long notesId, IFormFile file);
    }
}
