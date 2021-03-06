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

using MapWindow.Analysis.Topology.Algorithm;
using MapWindow.GeometriesGraph;
using MapWindow.GeometriesGraph.Index;

namespace MapWindow.Analysis.Topology.Operation.Overlay
{
    /// <summary>
    /// Nodes a set of edges.
    /// Takes one or more sets of edges and constructs a
    /// new set of edges consisting of all the split edges created by
    /// noding the input edges together.
    /// </summary>
    public class EdgeSetNoder
    {
        private LineIntersector li = null;
        private IList inputEdges = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="li"></param>
        public EdgeSetNoder(LineIntersector li)
        {
            this.li = li;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edges"></param>
        public virtual void AddEdges(IList edges)
        {
            // inputEdges.addAll(edges);
            foreach (object obj in edges)
                inputEdges.Add(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IList NodedEdges
        {
            get
            {
                EdgeSetIntersector esi = new SimpleMCSweepLineIntersector();
                SegmentIntersector si = new SegmentIntersector(li, true, false);
                esi.ComputeIntersections(inputEdges, si, true);                

                IList splitEdges = new ArrayList();
                IEnumerator i = inputEdges.GetEnumerator();
                while (i.MoveNext()) 
                {
                    Edge e = (Edge)i.Current;
                    e.EdgeIntersectionList.AddSplitEdges(splitEdges);
                }
                return splitEdges;
            }
        }
    }
}
