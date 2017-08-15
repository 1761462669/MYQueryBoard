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
    public class ParticulateLevel
    {
        /// <summary>
        /// Gets or sets the particulate count.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the daily rainfall.
        /// </summary>
        public DateTime X { get; set; }

        /// <summary>
        /// Initializes a new instance of the ParticulateLevel class.
        /// </summary>
        public ParticulateLevel()
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

                for (int i = 1; i < 10; i++)
                {
                    levelsInRainfall.Add(new ParticulateLevel { Y = 10 * i, X = new DateTime(2015, 9, 1,i,0,0) });
                }

                return levelsInRainfall;
            }
        }
    }
}