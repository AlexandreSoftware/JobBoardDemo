﻿using System.Collections.Generic;
using JobBoardServices.View;

namespace JobBoardRepository.Domain;
public class Applicant
{
    public int id;
    public string name;
    public List<Job> jobs;
}