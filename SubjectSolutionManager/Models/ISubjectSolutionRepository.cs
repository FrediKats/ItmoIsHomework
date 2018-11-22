﻿using System;
using System.Collections.Generic;

namespace SubjectSolutionManager.Models
{
    public interface ISubjectSolutionRepository
    {
        SubjectSolutionModel Create(SubjectSolutionModel solution);
        List<SubjectSolutionModel> Read();
        SubjectSolutionModel Update(SubjectSolutionModel solution);
        void Delete(Guid id);
    }
}