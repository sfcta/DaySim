﻿using DaySim.Framework.ChoiceModels;
using DaySim.Framework.Core;
using DaySim.Framework.DomainModels.Wrappers;
using DaySim.Framework.Roster;


namespace DaySim.ChoiceModels.Default.Models {
  internal class JAX_WorkLocationModel : WorkLocationModel {
    protected override void RegionSpecificCustomizations(ChoiceProbabilityCalculator.Alternative alternative, IPersonWrapper _person, IParcelWrapper destinationParcel) {

      double intracounty = ImpedanceRoster.GetValue("intracounty", Global.Settings.Modes.Sov, Global.Settings.PathTypes.FullNetwork, Global.Settings.ValueOfTimes.DefaultVot,
                1, _person.Household.ResidenceParcel, destinationParcel).Variable;

      alternative.AddUtilityTerm(45, intracounty);
    }
  }
}
