﻿using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class GetUsersOutput : BasePagingOutput
    {
        public List<UserOutput> UserList { get; set; } = new List<UserOutput>();
    }
}
