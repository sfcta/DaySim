﻿// Copyright 2005-2008 Mark A. Bradley and John L. Bowman
// Copyright 2011-2013 John Bowman, Mark Bradley, and RSG, Inc.
// You may not possess or use this file without a License for its use.
// Unless required by applicable law or agreed to in writing, software
// distributed under a License for its use is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

using System.Collections.Generic;
using System.Linq;
using DaySim.DomainModels.Actum.Models.Interfaces;
using DaySim.DomainModels.Actum.Wrappers.Interfaces;
using DaySim.Framework.Core;
using DaySim.Framework.DomainModels.Wrappers;
using DaySim.Framework.Factories;

namespace DaySim.DomainModels.Actum.Wrappers {
  [Factory(Factory.WrapperFactory, Category = Category.Wrapper, DataType = DataType.Actum)]
  public class TourWrapper : Default.Wrappers.TourWrapper, IActumTourWrapper {
    private readonly IActumTour _tour;

    [UsedImplicitly]
    public TourWrapper(Framework.DomainModels.Models.ITour tour, Framework.DomainModels.Wrappers.IPersonWrapper personWrapper, Framework.DomainModels.Wrappers.IPersonDayWrapper personDayWrapper, Framework.DomainModels.Wrappers.IParcelWrapper originParcel, Framework.DomainModels.Wrappers.IParcelWrapper destinationParcel, int destinationArrivalTime, int destinationDepartureTime, int destinationPurpose) : base(tour, personWrapper, personDayWrapper, originParcel, destinationParcel, destinationArrivalTime, destinationDepartureTime, destinationPurpose) {
      _tour = (IActumTour)tour;
    }

    [UsedImplicitly]
    public TourWrapper(Framework.DomainModels.Models.ITour subtour, Framework.DomainModels.Wrappers.ITourWrapper tourWrapper, bool suppressRandomVOT = false) : base(subtour, tourWrapper, Global.Settings.Purposes.PersonalBusiness, suppressRandomVOT) {
      _tour = (IActumTour)subtour;
    }

    [UsedImplicitly]
    public TourWrapper(Framework.DomainModels.Models.ITour subtour, Framework.DomainModels.Wrappers.ITourWrapper tourWrapper, int purpose, bool suppressRandomVOT = false) : base(subtour, tourWrapper, purpose, suppressRandomVOT) {
      _tour = (IActumTour)subtour;
    }

    [UsedImplicitly]
    public TourWrapper(Framework.DomainModels.Models.ITour tour, Framework.DomainModels.Wrappers.IPersonDayWrapper personDayWrapper, bool suppressRandomVOT = false) : base(tour, personDayWrapper, Global.Settings.Purposes.PersonalBusiness, suppressRandomVOT) {
      _tour = (IActumTour)tour;
    }

    [UsedImplicitly]
    public TourWrapper(Framework.DomainModels.Models.ITour tour, Framework.DomainModels.Wrappers.IPersonDayWrapper personDayWrapper, int purpose, bool suppressRandomVOT = false) : base(tour, personDayWrapper, purpose, suppressRandomVOT) {
      _tour = (IActumTour)tour;
    }


    #region relations properties

    //public IActumHouseholdWrapper Household { get; set; }

    //public IActumPersonWrapper Person { get; set; }

    //public IActumPersonDayWrapper PersonDay { get; set; }

    //public IActumTourWrapper ParentTour { get; set; }

    #endregion


    #region flags/choice model/etc. properties

    //JLB 20160323

    public int HalfTour1AccessMode { get; set; }

    public int HalfTour1AccessPathType { get; set; }

    public double HalfTour1AccessTime { get; set; }

    public double HalfTour1AccessCost { get; set; }

    public double HalfTour1AccessDistance { get; set; }

    public int HalfTour1AccessStopAreaKey { get; set; }

    public int HalfTour1EgressMode { get; set; }

    public int HalfTour1EgressPathType { get; set; }

    public double HalfTour1EgressTime { get; set; }

    public double HalfTour1EgressCost { get; set; }

    public double HalfTour1EgressDistance { get; set; }

    public int HalfTour1EgressStopAreaKey { get; set; }

    public int HalfTour2AccessMode { get; set; }

    public int HalfTour2AccessPathType { get; set; }

    public double HalfTour2AccessTime { get; set; }

    public double HalfTour2AccessCost { get; set; }

    public double HalfTour2AccessDistance { get; set; }

    public int HalfTour2AccessStopAreaKey { get; set; }

    public int HalfTour2EgressMode { get; set; }

    public int HalfTour2EgressPathType { get; set; }

    public double HalfTour2EgressTime { get; set; }

    public double HalfTour2EgressCost { get; set; }

    public double HalfTour2EgressDistance { get; set; }

    public int HalfTour2EgressStopAreaKey { get; set; }

    public double HalfTour1TravelTime { get; set; }

    public double HalfTour2TravelTime { get; set; }

    public double TravelCostForPTBikeTour { get; set; }

    public double TravelDistanceForPTBikeTour { get; set; }




    #endregion

    #region wrapper methods

    public override int GetVotALSegment() {
      int segment =
                (DestinationPurpose == Global.Settings.Purposes.Work || DestinationPurpose == Global.Settings.Purposes.School || DestinationPurpose == Global.Settings.Purposes.Escort)
                    ? Global.Settings.VotALSegments.Medium
                    : (DestinationPurpose == Global.Settings.Purposes.Business)
                        ? Global.Settings.VotALSegments.High
                        : Global.Settings.VotALSegments.Low;

      return segment;
    }

    public override void UpdateTourValues() {
      if (Global.Configuration.IsInEstimationMode || (Global.Configuration.ShouldRunTourModels && !Global.Configuration.ShouldRunTourTripModels) || (Global.Configuration.ShouldRunSubtourModels && !Global.Configuration.ShouldRunSubtourTripModels)) {
        return;
      }

      IEnumerable<PathTypeModels.IPathTypeModel> pathTypeModels =
      PathTypeModels.PathTypeModelFactory.Singleton
          .Run(Household.RandomUtility, OriginParcel, DestinationParcel, DestinationArrivalTime, DestinationDepartureTime, DestinationPurpose, CostCoefficient, TimeCoefficient, true, 1, Person.TransitPassOwnership, Household.OwnsAutomatedVehicles > 0, Person.GetTransitFareDiscountFraction(), false, Global.Settings.Modes.Sov);

      PathTypeModels.IPathTypeModel autoPathRoundTrip = pathTypeModels.First();

      AutoTimeOneWay = autoPathRoundTrip.PathTime / 2.0;
      AutoCostOneWay = autoPathRoundTrip.PathCost / 2.0;
      AutoDistanceOneWay = autoPathRoundTrip.PathDistance / 2.0;

      if (HalfTourFromOrigin == null || HalfTourFromDestination == null) {
        return;
      }

      List<ITripWrapper> trips =
                HalfTourFromOrigin
                    .Trips
                    .Union(HalfTourFromDestination.Trips)
                    .ToList();

      for (int i = 0; i < trips.Count; i++) {
        // sets the driver or passenger flag for each trip
        trips[i].SetDriverOrPassenger(trips);

        // sets the activity end time for each trip
        if (trips[i].IsHalfTourFromOrigin) {
          // for trips to intermediate stops on first half tour, use "arrival" time at stop
          if (i > 0) {
            trips[i].SetActivityEndTime(trips[i - 1].ArrivalTime);
          }
          // for trip to primary destination on tours without subtours, use departure time from prim.dest.
          else if (Subtours == null || Subtours.Count == 0) {
            trips[i].SetActivityEndTime(DestinationDepartureTime);
          }
          // for trip to primary destination on tours with subtours, use departure time for first subtour
          else {
            int nextDepartureTime = Global.Settings.Times.MinutesInADay;

            foreach (ITourWrapper subtour in Subtours) {
              if (subtour != null && subtour.OriginDepartureTime > trips[i].DepartureTime && subtour.OriginDepartureTime < nextDepartureTime) {
                nextDepartureTime = subtour.OriginDepartureTime;
              }
            }

            trips[i].SetActivityEndTime(nextDepartureTime);
          }
        } else {
          //second half tour
          // for trips to intermediate stops on second half tour, use departure time from stop
          if (i < trips.Count - 1) {
            trips[i].SetActivityEndTime(trips[i + 1].DepartureTime);
          }
          // if a home-based tour, look for start of next home-based tour}
          else if (ParentTour == null) {
            int nextDepartureTime = Global.Settings.Times.MinutesInADay;

            foreach (ITourWrapper otherTour in PersonDay.Tours) {
              if (otherTour != null && otherTour != this
                  && otherTour.OriginDepartureTime > trips[i].ArrivalTime
                  && otherTour.OriginDepartureTime < nextDepartureTime) {
                nextDepartureTime = otherTour.OriginDepartureTime;
              }
            }

            trips[i].SetActivityEndTime(nextDepartureTime);
          }
          // otherwise, a subtour - look for other subtours, else departure from the parent tour dest.
          else {
            int nextDepartureTime = ParentTour.DestinationDepartureTime;
            foreach (ITourWrapper otherSubtour in ParentTour.Subtours) {
              if (otherSubtour != null && otherSubtour != this && otherSubtour.OriginDepartureTime > trips[i].ArrivalTime && otherSubtour.OriginDepartureTime < nextDepartureTime) {
                nextDepartureTime = otherSubtour.OriginDepartureTime;
              }
            }

            trips[i].SetActivityEndTime(nextDepartureTime);
          }
        }
      }
    }


    //JLB 2016032
    public bool IsBusinessPurpose() {
      return DestinationPurpose == Global.Settings.Purposes.Business;
    }

    public bool IsHovDriverMode() {
      return Mode == Global.Settings.Modes.HovDriver;
    }

    public bool IsHovPassengerMode() {
      return Mode == Global.Settings.Modes.HovPassenger;
    }

    public bool IsPaidRideShareMode() {
      return Mode == Global.Settings.Modes.PaidRideShare;
    }
    #endregion




  }
}
