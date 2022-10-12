using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_Layer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interafce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundoContext fundoContext;
        //public object UserId { get; private set; }
        private readonly IConfiguration configuration;
        public NotesRL(FundoContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;

        }
        public NotesEntity Create(NotesModel notes, long UserId)
        {
            try
            {
                NotesEntity notesent = new NotesEntity();
                var result = fundoContext.NotesTable.Where(e => e.UserId == UserId);
                if (result != null)
                {


                    notesent.UserId = UserId;
                    notesent.Title = notes.Title;
                    notesent.Description = notes.Description;
                    notesent.colour = notes.colour;
                    notesent.Image = notes.Image;
                    notesent.Archive = notes.Archive;
                    notesent.Pin = notes.Pin;
                    notesent.Trash = notes.Trash;
                    notesent.Reminder = notes.Reminder;
                    notesent.Created = notes.Created;
                    notesent.Edited = notes.Edited;

                    fundoContext.NotesTable.Add(notesent);
                    fundoContext.SaveChanges();
                    return notesent;
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
        public NotesEntity Update(NotesModel notesUpdate, long UserId, long NoteId)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.UserId == UserId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = notesUpdate.Title;
                    result.Description = notesUpdate.Description;
                    result.colour = notesUpdate.colour;
                    result.Reminder = notesUpdate.Reminder;
                    result.Image = notesUpdate.Image;
                    result.Edited = DateTime.Now;
                    fundoContext.SaveChanges();
                    return result;
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
        //public List<NotesEntity> Retrieve(long UserId)
        //{
        //    try
        //    {
        //        var result = fundoContext.NotesTable.Where(x => x.UserId == UserId).FirstOrDefault();
        //        return null;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public List<NotesEntity> NotesRetrieve(long UserId)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.UserId == UserId);
                return null;
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
                var result = fundoContext.NotesTable.Where(x => x.UserId == UserId && x.NotesId == NoteId).FirstOrDefault();
                if (result != null)
                {
                    fundoContext.NotesTable.Remove(result);
                    this.fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundoContext.NotesTable.Where(x => x.UserId == UserId && x.NotesId == NoteId).FirstOrDefault();
                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundoContext.SaveChanges();
                    return result;
                }
                else
                {
                    result.Pin = true;
                    fundoContext.SaveChanges();
                    return null;
                }
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
                var result = fundoContext.NotesTable.Where(x => x.UserId == UserId && x.NotesId == NoteId).FirstOrDefault();
                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundoContext.SaveChanges();
                    return null;
                }
                else
                {
                    result.Archive = true;
                    fundoContext.SaveChanges();

                    return result;
                }
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
                var result = fundoContext.NotesTable.Where(x => x.UserId == UserId && x.NotesId == NoteId).FirstOrDefault();
                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundoContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundoContext.SaveChanges();
                    return true;
                }
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
                var result = fundoContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
                if (result != null)
                {
                    result.colour = Colour;
                    fundoContext.NotesTable.Update(result);
                    fundoContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
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
                var result = fundoContext.NotesTable.Where(u => u.UserId == userId && u.NotesId == notesId).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                       this.configuration["CloudinarySettings:CloudName"],
                       this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary _cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };
                    var uploadresult = _cloudinary.Upload(uploadParams);
                    string ImagePath = uploadresult.Url.ToString();
                    result.Image=ImagePath;
                    fundoContext.SaveChanges();
                    return "Image upload Successfully";

                }
                else
                {
                    return null;
                }

            }
            catch (Exception )
            {
                throw;
            }

        }


    }
}







