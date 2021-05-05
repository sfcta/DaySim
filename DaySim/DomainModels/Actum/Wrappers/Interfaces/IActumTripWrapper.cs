﻿// Copyright 2005-2008 Mark A. Bradley and John L. Bowman
// Copyright 2011-2013 John Bowman, Mark Bradley, and RSG, Inc.
// You may not possess or use this file without a License for its use.
// Unless required by applicable law or agreed to in writing, software
// distributed under a License for its use is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

using DaySim.DomainModels.Actum.Models.Interfaces;
using DaySim.Framework.DomainModels.Wrappers;

namespace DaySim.DomainModels.Actum.Wrappers.Interfaces {
  public interface IActumTripWrapper : ITripWrapper, IActumTrip {
    bool IsBusinessDestinationPurpose();

    bool IsBusinessOriginPurpose();

    //new IActumHouseholdWrapper Household { get; set; }

    //new IActumPersonWrapper Person { get; set; }

    //new IActumPersonDayWrapper PersonDay { get; set; }

    //new IActumTourWrapper Tour { get; set; }

  }
}
