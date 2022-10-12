﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository_Layer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }
        [ForeignKey("Notes")]
        public long NotesId { get; set; }
        public virtual NotesEntity Notes { get; set; }
    }
}
