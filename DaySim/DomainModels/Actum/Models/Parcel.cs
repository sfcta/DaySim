﻿// Copyright 2005-2008 Mark A. Bradley and John L. Bowman
// Copyright 2011-2013 John Bowman, Mark Bradley, and RSG, Inc.
// You may not possess or use this file without a License for its use.
// Unless required by applicable law or agreed to in writing, software
// distributed under a License for its use is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

using System.Runtime.InteropServices;
using DaySim.DomainModels.Actum.Models.Interfaces;
using DaySim.Framework.Factories;

namespace DaySim.DomainModels.Actum.Models {
  [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
  [Factory(Factory.PersistenceFactory, Category = Category.Model, DataType = DataType.Actum)]
  public sealed class Parcel : DaySim.DomainModels.Default.Models.Parcel, IActumParcel {
    public bool StopAreaDistanceArrayPositionsSet { get; set; } = false;

    public int FirstPositionInStopAreaDistanceArray { get; set; } = -1;

    public int LastPositionInStopAreaDistanceArray { get; set; } = -1;

  }
}
