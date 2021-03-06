//********************************************************************************************************
// Product Name: MapWindow.dll Alpha
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
using System;
using System.Collections.Generic;
using System.Data;
using MapWindow.Main;
using MapWindow.Geometries;
using MapWindow.Projections;

namespace MapWindow.Data
{
    /// <summary>
    /// A layer contains a list of features of a specific type that matches the geometry type.
    /// While this supports IRenderable, this merely forwards the drawing instructions to
    /// each of its members, but does not allow the control of any default layer properties here.
    /// Calling FeatureDataSource.Open will create a number of layers of the appropriate
    /// specific type and also create a specific layer type that is derived from this class
    /// that does expose default "layer" properties, as well as the symbology elements.
    /// </summary>
    public interface IFeatureSet: IDataSet, IAttributeSource
    {
        #region Events

        /// <summary>
        /// Occurs when the vertices are invalidated, encouraging a re-draw
        /// </summary>
        event EventHandler VerticesInvalidated;

        /// <summary>
        /// Occurs when a new feature is added to the list
        /// </summary>
        event EventHandler<FeatureEventArgs> FeatureAdded;

        /// <summary>
        /// Occurs when a feature is removed from the list.
        /// </summary>
        event EventHandler<FeatureEventArgs> FeatureRemoved;

        #endregion

        #region Methods

        /// <summary>
        /// If this featureset is in index mode, this will append the vertices and shapeindex of the shape.
        /// Otherwise, this will add a new feature based on this shape.  If the attributes of the shape are not null,
        /// this will attempt to append a new datarow It is up to the developer
        /// to ensure that the object array of attributes matches the this featureset.  If the Attributes of this feature are loaded,
        /// this will add the attributes in ram only.  Otherwise, this will attempt to insert the attributes as a 
        /// new record using the "AddRow" method.  The schema of the object array should match this featureset's column schema.
        /// </summary>
        /// <param name="shape">The shape to add to this featureset.</param>
        void AddShape(Shape shape);

        /// <summary>
        /// Adds any type of list or array of shapes.  If this featureset is not in index moded,
        /// it will add the features to the featurelist and suspend events for faster copying.
        /// </summary>
        /// <param name="shapes">An enumerable collection of shapes.</param>
        void AddShapes(IEnumerable<Shape> shapes);
        

        /// <summary>
        /// Gets the specified feature by constructing it from the vertices, rather
        /// than requiring that all the features be created. (which takes up a lot of memory).
        /// </summary>
        /// <param name="index">The integer index</param>
        IFeature GetFeature(int index);

        /// <summary>
        /// Gets a shape at the specified shape index.  If the featureset is in 
        /// indexmode, this returns a copy of the shape.  If not, it will create
        /// a new shape based on the specified feature.
        /// </summary>
        /// <param name="index">The zero based integer index of the shape.</param>
        /// <param name="getAttributes">If getAttributes is true, then this also try to get attributes for that shape.
        /// If attributes are loaded, then it will use the existing datarow.  Otherwise, it will read the attributes
        /// from the file.  (This second option is not recommended for large repeats.  In such a case, the attributes
        /// can be set manually from a larger bulk query of the data source.)</param>
        /// <returns>The Shape object</returns>
        Shape GetShape(int index, bool getAttributes);
       

        /// <summary>
        /// Given a datarow, this will return the associated feature.  This FeatureSet
        /// uses an internal dictionary, so that even if the items are re-ordered
        /// or have new members inserted, this lookup will still work.
        /// </summary>
        /// <param name="row">The DataRow for which to obtaind the feature</param>
        /// <returns>The feature to obtain that is associated with the specified data row.</returns>
        IFeature FeatureFromRow(DataRow row);
     

        /// <summary>
        /// Generates a new feature, adds it to the features and returns the value.
        /// </summary>
        /// <returns>The feature that was added to this featureset</returns>
        IFeature AddFeature(IBasicGeometry geometry);

        /// <summary>
        /// Adds the FID values as a field called FID, but only if the FID field
        /// does not already exist
        /// </summary>
        void AddFid();
        

        /// <summary>
        /// Copies all the features from the specified featureset.  
        /// </summary>
        /// <param name="source">The source IFeatureSet to copy features from.</param>
        /// <param name="copyAttributes">Boolean, true if the attributes should be copied as well.  If this is true,
        /// and the attributes are not loaded, a FillAttributes call will be made.</param>
        void CopyFeatures(IFeatureSet source, bool copyAttributes);

        

        /// <summary>
        /// Retrieves a subset using exclusively the features matching the specified values.
        /// </summary>
        /// <param name="indices">An integer list of indices to copy into the new FeatureSet</param>
        /// <returns>A FeatureSet with the new items.</returns>
        IFeatureSet CopySubset(List<int> indices);

        /// <summary>
        /// Copies the subset of specified features to create a new featureset that is restricted to
        /// just the members specified.
        /// </summary>
        /// <param name="filterExpression">The string expression to test.</param>
        /// <returns>A FeatureSet that has members that only match the specified members.</returns>
        IFeatureSet CopySubset(string filterExpression);
        
        /// <summary>
        /// Copies only the names and types of the attribute fields, without copying any of the attributes or features.
        /// </summary>
        /// <param name="source">The source featureSet to obtain the schema from.</param>
        void CopyTableSchema(IFeatureSet source);

        /// <summary>
        /// Copies the Table schema (column names/data types)
        /// from a DatatTable, but doesn't copy any values.
        /// </summary>
        /// <param name="sourceTable">The Table to obtain schema from.</param>
        void CopyTableSchema(DataTable sourceTable);


        /// <summary>
        /// Instructs the shapefile to read all the attributes from the file.
        /// This may also be a cue to read values from a database.
        /// </summary>
        void FillAttributes();


        /// <summary>
        /// Instructs the shapefile to read all the attributes from the file.
        /// This may also be a cue to read values from a database.
        /// </summary>
        void FillAttributes(IProgressHandler progressHandler);
     

        /// <summary>
        /// Obtains the list of feature indices that match the specified filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to find features for.</param>
        /// <returns>The list of integers that are the FIDs of the specified values.</returns>
        List<int> Find(string filterExpression);

        /// <summary>
        /// This forces the vertex initialization so that Vertices, ShapeIndices, and the
        /// ShapeIndex property on each feature will no longer be null.
        /// </summary>
        void InitializeVertices();
      

        /// <summary>
        /// Switches a boolean so that the next time that the vertices are requested,
        /// they must be re-calculated from the feature coordinates.
        /// </summary>
        /// <remarks>This only affects reading values from the Vertices cache</remarks>
        void InvalidateVertices();

        /// <summary>
        /// Reprojects all of the in-ram vertices of this featureset.
        /// This will also update the projection to be the specified projection.
        /// </summary>
        /// <param name="targetProjection">The projection information to reproject the coordinates to.</param>
        void Reproject(ProjectionInfo targetProjection);
       
        #endregion


        #region Properties

        /// <summary>
        /// Gets whether or not the attributes have all been loaded into the data table.
        /// </summary>
        bool AttributesPopulated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the coordinate type across the entire featureset.
        /// </summary>
        CoordinateTypes CoordinateType
        {
            get;
            set;
           
        }


        /// <summary>
        /// Gets the DataTable associated with this specific feature.
        /// </summary>
        DataTable DataTable
        {
            get;
        }

        /// <summary>
        /// Returns the bounding envelope in spatial coordinates for the entire layer
        /// </summary>
        IEnvelope Envelope
        {
            get;
        }

        /// <summary>
        /// The cached extent from the shapefile.  This will not attempt to calculate the extent.
        /// </summary>
        Extent Extent
        {
            get; set;
        }

        /// <summary>
        /// Gets the list of all the features that are included in this layer.
        /// </summary>
        IFeatureList Features
        {
            get; set;
        }

        /// <summary>
        /// Gets the feature lookup Table itself.
        /// </summary>
        Dictionary<DataRow, IFeature> FeatureLookup
        { 
            get;
        }

        /// <summary>
        /// Gets an enumeration indicating the type of feature represented in this dataset, if any.
        /// </summary>
        FeatureTypes FeatureType
        {
            get; set;
        }

        /// <summary>
        /// Gets the string filename for this feature layer, if any
        /// </summary>
        string Filename
        {
            get;
            set;
        }

       

        /// <summary>
        /// Gets or sets the progress handler to use for methods called by this featureset.
        /// Setting this resets the progress meter to work with the new handler.
        /// </summary>
        IProgressHandler ProgressHandler
        {
            get;
            set;
        }

        /// <summary>
        /// These specifically allow the user to make sense of the Vertices array.  These are 
        /// fast acting sealed classes and are not meant to be overridden or support clever
        /// new implementations.
        /// </summary>
        List<ShapeRange> ShapeIndices
        {
            get; set;
        }

        /// <summary>
        /// If this is true, then the ShapeIndices and Vertex values are used,
        /// and features are created on demand.  Otherwise the list of Features
        /// is used directly.
        /// </summary>
        bool IndexMode
        {
            get; set;
        }

        /// <summary>
        /// Skips the features themselves and uses the shapeindicies instead.
        /// </summary>
        /// <param name="region">The region to select members from</param>
        /// <returns>A list of integer valued shape indices that are selected.</returns>
        List<int> SelectIndices(Extent region);
        

         /// <summary>
        /// Gets an array of Vertex structures with X and Y coordinates
        /// </summary>
        double[] Vertex
        {
            get; set;
        }

        ///// <summary>
        ///// Gets a double array.  This can drastically speed up data access by bypassing the
        ///// property accessors to get the values.  However, to get the benefit, you should
        ///// use double[][] myVertices = myFeatureSet.Vertice, and then access the values
        ///// directly from myVertices, rather than accessing FeatureSet.Vertices[12] directly.
        ///// </summary>
        //double[][] Vertices
        //{
        //    get;
        //}

      
        /// <summary>
        /// Z coordinates
        /// </summary>
        double[] Z
        {
            get; set;
        }
        /// <summary>
        /// M coordinates
        /// </summary>
        double[] M
        {
            get; set;
        }
        /// <summary>
        /// Gets a boolean that indicates whether or not the InvalidateVertices has been called
        /// more recently than the cached vertex array has been built.
        /// </summary>
        bool VerticesAreValid
        {
            get;
        }
       

        #endregion

       

        

   
        

        

       


        /// <summary>
        /// Saves the information in the Layers provided by this datasource onto its existing file location
        /// </summary>
        void Save();

        /// <summary>
        /// Saves a datasource to the file.
        /// </summary>
        /// <param name="filename">The string filename location to save to</param>
        /// <param name="overwrite">Boolean, if this is true then it will overwrite a file of the existing name.</param>
        void SaveAs(string filename, bool overwrite);

       
       
        /// <summary>
        /// returns only the features that have envelopes that
        /// intersect with the specified envelope.
        /// </summary>
        /// <param name="region">The specified region to test for intersect with</param>
        /// <returns>A List of the IFeature elements that are contained in this region</returns>
        List<IFeature> Select(IEnvelope region);
     

        /// <summary>
        /// returns only the features that have envelopes that
        /// intersect with the specified envelope.
        /// </summary>
        /// <param name="region">The specified region to test for intersect with</param>
        /// <param name="selectedRegion">This returns the geographic extents for the entire selected area.</param>
        /// <returns>A List of the IFeature elements that are contained in this region</returns>
        List<IFeature> Select(IEnvelope region, out IEnvelope selectedRegion);

        /// <summary>
        /// The string filter expression to use in order to return the desired features.
        /// </summary>
        /// <param name="filterExpression">The features to return.</param>
        /// <returns>The list of desired features.</returns>
        List<IFeature> SelectByAttribute(string filterExpression);

        /// <summary>
        /// This version is more tightly integrated to the DataTable and returns the row indices, rather
        /// than attempting to link the results to features themselves, which may not even exist.
        /// </summary>
        /// <param name="filterExpression">The filter expression</param>
        /// <returns>The list of indices</returns>
        List<int> SelectIndexByAttribute(string filterExpression);
      
       

        /// <summary>
        /// After changing coordinates, this will force the re-calculation of envelopes on a feature
        /// level as well as on the featureset level.
        /// </summary>
        void UpdateEnvelopes();


    }
}
