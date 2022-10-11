using Business_Layer.Interface;
using Common_Layer.Model;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Entity;
using Repository_Layer.Interafce;
using Repository_Layer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesEntity Create(NotesModel notes, long UserId)
        {
            try
            {
                return notesRL.Create(notes, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity Update(NotesModel notesUpdate, long UserId, long NoteId)
        {
            try
            {
                return notesRL.Update(notesUpdate, UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<NotesEntity> NotesRetrieve(long UserId)
        {
            try
            {
                return notesRL.NotesRetrieve(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool NoteDelete(long UserId, long NoteId)
        {
            try
            {
                return notesRL.NoteDelete(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity NotePin(long NoteId, long UserId)
        {
            try
            {
                return notesRL.NotePin(NoteId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity NoteArchive(long UserId, long NoteId)
        {
            try
            {
                return notesRL.NoteArchive(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool NoteTrash(long UserId, long NoteId)
        {
            try
            {
                return notesRL.NoteTrash(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity NoteColourChange(long notesId, string Colour)
        {
            try
            {
                return notesRL.NoteColourChange(notesId, Colour);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string Image(long userId, long notesId, IFormFile file)
        {
            try
            {
                return notesRL.Image(userId, notesId, file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
