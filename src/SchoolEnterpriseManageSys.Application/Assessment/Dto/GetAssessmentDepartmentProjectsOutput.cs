﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class GetAssessmentDepartmentProjectsOutput
    {
        public List<AssessmentProjectOutput> ProjectList { get; set; } = new List<AssessmentProjectOutput>();
    }
}
