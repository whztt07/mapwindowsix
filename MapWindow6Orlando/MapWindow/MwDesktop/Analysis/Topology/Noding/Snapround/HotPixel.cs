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
using System.Collections;
using System.Text;

using MapWindow.Geometries;
using MapWindow.Analysis.Topology.Algorithm;
using MapWindow.Analysis.Topology.Utilities;
namespace MapWindow.Analysis.Topology.Noding.Snapround
{

    /// <summary>
    /// Implements a "hot pixel" as used in the Snap Rounding algorithm.
    /// A hot pixel contains the interior of the tolerance square and the boundary
    /// minus the top and right segments.
    /// The hot pixel operations are all computed in the integer domain
    /// to avoid rounding problems.
    /// </summary>
    public class HotPixel
    {
        private LineIntersector li = null;

        private Coordinate pt = null;
        private Coordinate originalPt = null;        

        private Coordinate p0Scaled = null;
        private Coordinate p1Scaled = null;

        private double scaleFactor;

        private double minx;
        private double maxx;
        private double miny;
        private double maxy;

        /*
         * The corners of the hot pixel, in the order:
         *  10
         *  23
         */
        private Coordinate[] corner = new Coordinate[4];

        private Envelope safeEnv = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotPixel"/> class.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="scaleFactor"></param>
        /// <param name="li"></param>
        public HotPixel(Coordinate pt, double scaleFactor, LineIntersector li)
        {
            originalPt = pt;
            this.pt = pt;
            this.scaleFactor = scaleFactor;
            this.li = li;            
            if(scaleFactor != 1.0)
            {
                this.pt = new Coordinate(Scale(pt.X), Scale(pt.Y));
                p0Scaled = new Coordinate();
                p1Scaled = new Coordinate();
            }
            InitCorners(this.pt);
        }

        /// <summary>
        /// 
        /// </summary>
        public Coordinate Coordinate
        {
            get
            {
                return originalPt;
            }
        }

        /// <summary>
        /// Returns a "safe" envelope that is guaranteed to contain the hot pixel.
        /// </summary>
        /// <returns></returns>
        public Envelope GetSafeEnvelope()
        {
            if (safeEnv == null)
            {
                double safeTolerance = 0.75 / scaleFactor;
                safeEnv = new Envelope(originalPt.X - safeTolerance, originalPt.X + safeTolerance,
                                       originalPt.Y - safeTolerance, originalPt.Y + safeTolerance);
            }
            return safeEnv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        private void InitCorners(Coordinate pt)
        {
            double tolerance = 0.5;
            minx = pt.X - tolerance;
            maxx = pt.X + tolerance;
            miny = pt.Y - tolerance;
            maxy = pt.Y + tolerance;

            corner[0] = new Coordinate(maxx, maxy);
            corner[1] = new Coordinate(minx, maxy);
            corner[2] = new Coordinate(minx, miny);
            corner[3] = new Coordinate(maxx, miny);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private double Scale(double val)
        {
            return (double)Math.Round(val * scaleFactor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public bool Intersects(Coordinate p0, Coordinate p1)
        {
            if (scaleFactor == 1.0)
                return IntersectsScaled(p0, p1);

            CopyScaled(p0, p0Scaled);
            CopyScaled(p1, p1Scaled);
            return IntersectsScaled(p0Scaled, p1Scaled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pScaled"></param>
        private void CopyScaled(Coordinate p, Coordinate pScaled)
        {
            pScaled.X = Scale(p.X);
            pScaled.Y = Scale(p.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public bool IntersectsScaled(Coordinate p0, Coordinate p1)
        {
            double segMinx = Math.Min(p0.X, p1.X);
            double segMaxx = Math.Max(p0.X, p1.X);
            double segMiny = Math.Min(p0.Y, p1.Y);
            double segMaxy = Math.Max(p0.Y, p1.Y);

            bool isOutsidePixelEnv = maxx < segMinx || minx > segMaxx || 
                                     maxy < segMiny || miny > segMaxy;
            if (isOutsidePixelEnv)
                return false;
            bool intersects = IntersectsToleranceSquare(p0, p1);           
            Assert.IsTrue(!(isOutsidePixelEnv && intersects), "Found bad envelope test");
            return intersects;
        }

        /// <summary>
        /// Tests whether the segment p0-p1 intersects the hot pixel tolerance square.
        /// Because the tolerance square point set is partially open (along the
        /// top and right) the test needs to be more sophisticated than
        /// simply checking for any intersection.  However, it
        /// can take advantage of the fact that because the hot pixel edges
        /// do not lie on the coordinate grid.  It is sufficient to check
        /// if there is at least one of:
        ///  - a proper intersection with the segment and any hot pixel edge.
        ///  - an intersection between the segment and both the left and bottom edges.
        ///  - an intersection between a segment endpoint and the hot pixel coordinate.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        private bool IntersectsToleranceSquare(Coordinate p0, Coordinate p1)
        {
            bool intersectsLeft = false;
            bool intersectsBottom = false;

            li.ComputeIntersection(p0, p1, corner[0], corner[1]);
            if(li.IsProper) return true;

            li.ComputeIntersection(p0, p1, corner[1], corner[2]);
            if(li.IsProper) return true;
            if(li.HasIntersection) intersectsLeft = true;

            li.ComputeIntersection(p0, p1, corner[2], corner[3]);
            if(li.IsProper) return true;
            if(li.HasIntersection) intersectsBottom = true;

            li.ComputeIntersection(p0, p1, corner[3], corner[0]);
            if(li.IsProper) return true;

            if(intersectsLeft && intersectsBottom) return true;

            if(p0.Equals(pt)) return true;
            if(p1.Equals(pt)) return true;

            return false;
        }

        /// <summary>
        /// Test whether the given segment intersects
        /// the closure of this hot pixel.
        /// This is NOT the test used in the standard snap-rounding
        /// algorithm, which uses the partially closed tolerance square instead.
        /// This routine is provided for testing purposes only.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        private bool IntersectsPixelClosure(Coordinate p0, Coordinate p1)
        {
            li.ComputeIntersection(p0, p1, corner[0], corner[1]);
            if(li.HasIntersection) return true;
            li.ComputeIntersection(p0, p1, corner[1], corner[2]);
            if(li.HasIntersection) return true;
            li.ComputeIntersection(p0, p1, corner[2], corner[3]);
            if(li.HasIntersection) return true;
            li.ComputeIntersection(p0, p1, corner[3], corner[0]);
            if(li.HasIntersection) return true;
            return false;
        }

    }
}
