//********************************************************************************************************
// Product Name: MapWindow.dll Alpha
// Description:  The basic module for MapWindow version 6.0
//********************************************************************************************************
// The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
// you may not use this file except in compliance with the License. You may obtain a copy of the License at 
// http://www.mozilla.org/MPL/  Alternately, you can access an earlier version of this content from
// the Net Topology Suite, which is protected by the GNU Lesser Public License
// http://www.gnu.org/licenses/lgpl.html and the sourcecode for the Net Topology Suite
// can be obtained here: http://sourceforge.net/projects/nts.
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
// ANY KIND, either expressed or implied. See the License for the specificlanguage governing rights and 
// limitations under the License. 
//
// The Original Code is from the Net Topology Suite
//
// The Initial Developer to integrate this code into MapWindow 6.0 is Ted Dunsford.
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************
using System;


namespace MapWindow.Geometries
{
   
    /// <summary>
    /// 
    /// </summary>
    public static class Location
    {
        /// <summary>
        /// Converts the location value to a location symbol, for example, <c>EXTERIOR => 'e'</c>.
        /// </summary>
        /// <param name="locationValue"></param>
        /// <returns>Either 'e', 'b', 'i' or '-'.</returns>
        public static char ToLocationSymbol(Locations locationValue)
        {
            switch (locationValue)
            {
                case Locations.Exterior:
                    return 'e';
                case Locations.Boundary:
                    return 'b';
                case Locations.Interior:
                    return 'i';
                case Locations.Null:
                    return '-';
            }
            throw new ArgumentException("Unknown location value: " + locationValue);
        }
    }   
}
