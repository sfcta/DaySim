﻿using DaySim.ChoiceModels.Default.Models;
using DaySim.Framework.ChoiceModels;
using DaySim.Framework.Core;
using DaySim.Framework.DomainModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVRPC.ChoiceModels.Default.Models {
  internal class DVRPC_SchoolLocationModel : SchoolLocationModel {

    protected override void RegionSpecificCustomizations(ChoiceProbabilityCalculator.Alternative alternative, IPersonWrapper _person, IParcelWrapper destinationParcel) {

      //areas 

      int o_int_paphi = (_person.Household.ResidenceParcel.ZoneKey <= 4000).ToFlag();
      int o_int_paoth = (_person.Household.ResidenceParcel.ZoneKey <= 18000).ToFlag() * (1 - o_int_paphi);
      int o_int_nj = (_person.Household.ResidenceParcel.ZoneKey > 18000 && _person.Household.ResidenceParcel.ZoneKey <= 30000).ToFlag();
      int o_ext_pa = (_person.Household.ResidenceParcel.ZoneKey > 50000 && _person.Household.ResidenceParcel.ZoneKey <= 53000).ToFlag();
      int o_ext_nj = (_person.Household.ResidenceParcel.ZoneKey > 53000 && _person.Household.ResidenceParcel.ZoneKey <= 58000).ToFlag();
      int o_ext_oth = (_person.Household.ResidenceParcel.ZoneKey > 58000).ToFlag();

      int d_int_paphi = (destinationParcel.ZoneKey <= 4000).ToFlag();
      int d_int_paoth = (destinationParcel.ZoneKey <= 18000).ToFlag() * (1 - d_int_paphi);
      int d_int_nj = (destinationParcel.ZoneKey > 18000 && destinationParcel.ZoneKey <= 30000).ToFlag();
      int d_ext_pa = (destinationParcel.ZoneKey > 50000 && destinationParcel.ZoneKey <= 53000).ToFlag();
      int d_ext_nj = (destinationParcel.ZoneKey > 53000 && destinationParcel.ZoneKey <= 58000).ToFlag();
      int d_ext_oth = (destinationParcel.ZoneKey > 58000).ToFlag();


      alternative.AddUtilityTerm(131, o_int_paphi * d_int_paphi);
      alternative.AddUtilityTerm(132, o_int_paphi * d_int_paoth);
      alternative.AddUtilityTerm(133, o_int_paphi * d_int_nj);
      alternative.AddUtilityTerm(134, o_int_paphi * d_ext_pa);
      alternative.AddUtilityTerm(135, o_int_paphi * d_ext_nj);
      alternative.AddUtilityTerm(136, o_int_paphi * d_ext_oth);

      alternative.AddUtilityTerm(141, o_int_paoth * d_int_paphi);
      alternative.AddUtilityTerm(142, o_int_paoth * d_int_paoth);
      alternative.AddUtilityTerm(143, o_int_paoth * d_int_nj);
      alternative.AddUtilityTerm(144, o_int_paoth * d_ext_pa);
      alternative.AddUtilityTerm(145, o_int_paoth * d_ext_nj);
      alternative.AddUtilityTerm(146, o_int_paoth * d_ext_oth);

      alternative.AddUtilityTerm(151, o_int_nj * d_int_paphi);
      alternative.AddUtilityTerm(152, o_int_nj * d_int_paoth);
      alternative.AddUtilityTerm(153, o_int_nj * d_int_nj);
      alternative.AddUtilityTerm(154, o_int_nj * d_ext_pa);
      alternative.AddUtilityTerm(155, o_int_nj * d_ext_nj);
      alternative.AddUtilityTerm(156, o_int_nj * d_ext_oth);

    }

  }
}
