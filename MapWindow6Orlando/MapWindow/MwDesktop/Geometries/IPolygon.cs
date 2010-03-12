//********************************************************************************************************
// Product Name: MapWindow.Interfaces Alpha
// Description:  The core libraries for the MapWindow 6.0 project.
//
//********************************************************************************************************
// The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
// you may not use this file except in compliance with the License. You may obtain a copy of the License at 
// http://www.mozilla.org/MPL/ 
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
// ANY KIND, either expressed or implied. See the License for the specificlanguage governing rights and 
// limitations under the License. 
//
// The Original Code is MapWindow.dll for the MapWindow 6.0 project
//
// The Initial Developer of this Original Code is Ted Dunsford. Created in August, 2007.
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************


namespace MapWindow.Geometries
{
    /// <summary>
    /// Full powered topology style Polygon
    /// </summary>
    public interface IPolygon: IGeometry, IBasicPolygon
    {

        /// <summary>
        /// Gets a specific <see cref="ILinearRing">ILinearRing</see> identified by the 0 based index n
        /// </summary>
        /// <param name="n">A 0 based integer index enumerating the rings</param>
        /// <returns><see cref="ILinearRing">ILinearRing</see> outlining the n'th hole in the polygon</returns>
        ILineString GetInteriorRingN(int n);

        /// <summary>
        /// Gets the <see cref="ILinearRing">ILinearRing</see> for the Exterior Ring.
        /// </summary>
        new ILinearRing Shell{get;}

        /// <summary>
        /// Gets the System.Array of <see cref="ILinearRing">ILinearRing</see>s that make up the holes in the polygon.
        /// </summary>
        new ILinearRing[] Holes { get;}

    }
}