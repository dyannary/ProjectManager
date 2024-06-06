﻿using ProjectManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectManager.Application.DataTransferObjects.User
{
    public class AddUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Add user avatar
        public string PhotoPath {  get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public int RoleId { get; set; } 
    }
}
