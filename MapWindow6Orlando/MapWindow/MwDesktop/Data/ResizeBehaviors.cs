//********************************************************************************************************
// Product Name: MapWindow.dll Alpha
// Description:  The basic module for MapWindow version 6.0
//********************************************************************************************************
// The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
// you may not use this file except in compliance with the License. You may obtain a copy of the License at 
// http://www.mozilla.org/MPL/ 
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
// ANY KIND, either expressed or implied. See the License for the specificlanguage governing rights and 
// limitations under the License. 
//
// The Original Code is from MapWindow.dll version 6.0
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 1/15/2009 4:50:36 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************


namespace MapWindow.Data
{


    /// <summary>
    /// ResizeBehaviors are for legacy support more than for affecting MW6.
    /// </summary>
    public enum ResizeBehaviors
    {
      
        /// <summary>
        /// Classic
        /// </summary>
        Classic = 0,
        
        /// <summary>
        /// Intuitive
        /// </summary>
        Intuitive = 2,

        /// <summary>
        /// Modern
        /// </summary>
        Modern = 1,

        /// <summary>
        /// Warp
        /// </summary>
        Warp = 3



    }
}
