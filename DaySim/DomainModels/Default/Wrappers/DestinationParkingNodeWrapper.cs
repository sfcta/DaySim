// Copyright 2005-2008 Mark A. Bradley and John L. Bowman
// Copyright 2011-2013 John Bowman, Mark Bradley, and RSG, Inc.
// You may not possess or use this file without a License for its use.
// Unless required by applicable law or agreed to in writing, software
// distributed under a License for its use is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

using System;
using System.Collections.Generic;
using DaySim.Framework.Core;
using DaySim.Framework.DomainModels.Models;
using DaySim.Framework.DomainModels.Wrappers;
using DaySim.Framework.Factories;
using DaySim.Framework.ShadowPricing;

namespace DaySim.DomainModels.Default.Wrappers {
	[Factory(Factory.WrapperFactory, Category = Category.Wrapper, DataType = DataType.Default)]
	public class DestinationParkingNodeWrapper : IDestinationParkingNodeWrapper {
		private readonly IDestinationParkingNode _destinationParkingNode;

		[UsedImplicitly]
		public DestinationParkingNodeWrapper(IDestinationParkingNode destinationParkingNode) {
			_destinationParkingNode = destinationParkingNode;
		}

		#region domain model properies

		public int Id {
			get { return _destinationParkingNode.Id; }
			set { _destinationParkingNode.Id = value; }
		}

		public int ZoneId {
			get { return _destinationParkingNode.ZoneId; }
			set { _destinationParkingNode.ZoneId = value; }
		}

		public int XCoordinate {
			get { return _destinationParkingNode.XCoordinate; }
			set { _destinationParkingNode.XCoordinate = value; }
		}

		public int YCoordinate {
			get { return _destinationParkingNode.YCoordinate; }
			set { _destinationParkingNode.YCoordinate = value; }
		}

        public int Type      {
            get { return _destinationParkingNode.Type; }
            set { _destinationParkingNode.Type = value; }
        }

        public int Capacity {
			get { return _destinationParkingNode.Capacity; }
			set { _destinationParkingNode.Capacity = value; }
		}

		public int ParcelId {
			get { return _destinationParkingNode.ParcelId; }
			set { _destinationParkingNode.ParcelId = value; }
		}

		public int NodeId {
			get { return _destinationParkingNode.NodeId; }
			set { _destinationParkingNode.NodeId = value; }
		}

		public int MaxDuration {
			get { return _destinationParkingNode.MaxDuration; }
			set { _destinationParkingNode.MaxDuration = value; }
		}

        public double PreOccupiedDay {
            get { return _destinationParkingNode.PreOccupiedDay; }
            set { _destinationParkingNode.PreOccupiedDay = value; }
        }

        public double PreOccupiedOther   {
            get { return _destinationParkingNode.PreOccupiedOther; }
            set { _destinationParkingNode.PreOccupiedOther = value; }
        }

        public double Price7AM {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price8AM        {
            get { return _destinationParkingNode.Price8AM; }
            set { _destinationParkingNode.Price8AM = value; }
        }

        public double Price9AM     {
            get { return _destinationParkingNode.Price9AM; }
            set { _destinationParkingNode.Price9AM = value; }
        }

        public double Price10AM         {
            get { return _destinationParkingNode.Price10AM; }
            set { _destinationParkingNode.Price10AM = value; }
        }

        public double Price11AM       {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price12PM       {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price1PM      {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price2PM       {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price3PM       {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price4PM       {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price5PM         {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price6PM        {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price7PM       {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price8PM        {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price9PM        {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price10PM     {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price11PM        {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }

        public double Price12AM        {
            get { return _destinationParkingNode.Price7AM; }
            set { _destinationParkingNode.Price7AM = value; }
        }
        #endregion

        #region flags/choice model/etc. properties

        public double[] ShadowPriceDifference { get; set; }

		public double[] ShadowPrice { get; set; }

		public double[] ExogenousLoad { get; set; }

		public double[] ParkingLoad { get; set; }

		#endregion

		#region wrapper methods

		public virtual void SetDestinationParkingShadowPricing(Dictionary<int, IDestinationParkingShadowPriceNode> destinationParkingShadowPrices) {
			if (destinationParkingShadowPrices == null) {
				throw new ArgumentNullException("destinationParkingShadowPrices");
			}

			if (!Global.DestinationParkingNodeIsEnabled || !Global.Configuration.ShouldUseDestinationParkingShadowPricing || Global.Configuration.IsInEstimationMode) {
				return;
			}

			IDestinationParkingShadowPriceNode destinationParkingShadowPriceNode;

			ShadowPriceDifference = new double[Global.Settings.Times.MinutesInADay];
			ShadowPrice = new double[Global.Settings.Times.MinutesInADay];
			ExogenousLoad = new double[Global.Settings.Times.MinutesInADay];
			ParkingLoad = new double[Global.Settings.Times.MinutesInADay];

			if (!destinationParkingShadowPrices.TryGetValue(Id, out destinationParkingShadowPriceNode)) {
				return;
			}

			ShadowPriceDifference = destinationParkingShadowPrices[Id].ShadowPriceDifference;
			ShadowPrice = destinationParkingShadowPrices[Id].ShadowPrice;
			ExogenousLoad = destinationParkingShadowPrices[Id].ExogenousLoad;
			// ParkAndRideLoad = parkAndRideShadowPrices[Id].ParkAndRideLoad; {JLB 20121001 commented out this line so that initial values of load are zero for any run}
		}

		#endregion
	}
}