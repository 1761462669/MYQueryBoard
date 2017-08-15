// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections;
using System.Windows.Controls;

namespace MDF.UISample.ControlStyle.DataChart
{
    /// <summary>
    /// ParticulateLevel business object used for charting samples.
    /// </summary>
    public class ParticulateLevel2
    {
        /// <summary>
        /// Gets or sets the particulate count.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the daily rainfall.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Initializes a new instance of the ParticulateLevel class.
        /// </summary>
        public ParticulateLevel2()
        {
        }

        /// <summary>
        /// Gets a collection of particulate levels for rainfall.
        /// </summary>
        /// <remarks>
        /// Sample data from http://office.microsoft.com/en-us/help/HA102274781033.aspx.
        /// </remarks>
        public static ObjectCollection LevelsInRainfall
        {
            get
            {
                ObjectCollection levelsInRainfall = new ObjectCollection();
                //levelsInRainfall.Add(new ParticulateLevel { Y = 222, X = 4.1 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 217, X = 4.3 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 212, X = 5.7 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 214, X = 5.4 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 210, X = 5.9 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 214, X = 5.0 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 228, X = 3.6 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 237, X = 1.9 });
                //levelsInRainfall.Add(new ParticulateLevel { Y = 204, X = 7.3 });
                return levelsInRainfall;
            }
        }
    }
}