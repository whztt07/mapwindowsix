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
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/27/2008 12:59:41 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************

using System;

namespace MapWindow.Geometries
{


    /// <summary>
    /// A 4 x 4 matrix is required for transformations in 3 dimensions
    /// </summary>
    public class MatrixD : IMatrixD
    {
        #region Private Variables

        private double[,] _values;
        private int _n;
        private int _m;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new squre identity matrix of the specified size
        /// </summary>
        /// <param name="m">The size of the matrix to create</param>
        public MatrixD(int m)
        {
            _values = new double[m, m];
            _m = m;
            _n = m;
            
        }

        /// <summary>
        /// Creates a new instance of Matrix with m rows and n columns
        /// </summary>
        public MatrixD(int m, int n)
        {
            _values = new double[m, n];
            _m = m;
            _n = n;
        }

        /// <summary>
        /// Creates a matrix using the specified values.
        /// </summary>
        /// <param name="values"></param>
        public MatrixD(double[,] values)
        {
            _values = values;
            _m = values.GetLength(0);
            _n = values.GetLength(1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Matrix multiplication only works if the number of columns of the first matrix is the same
        /// as the number of rows of the second matrix.  The first matrix is this object, so this
        /// will only work if inMatrix has the same number of rows as this matrix has columns.
        /// </summary>
        /// <param name="inMatrix">The IMatrix to multiply against this matrix</param>
        /// <returns></returns>
        public IMatrixD Multiply(IMatrixD inMatrix)
        {
            if (inMatrix.NumRows != NumColumns) throw new ArgumentException("Matrix multiplication only works if the number of columns of the first matrix is the same as the number of rows of the second matrix");
            
            int m = NumRows;
            int n = inMatrix.NumColumns;
            double[,] vals = new double[m, n];
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    vals[row, col] = 0;
                    for(int I = 0; I < NumColumns; I++)
                    {
                        vals[row, col] += _values[row, I] * inMatrix.Values[I, col];
                    }
                }
            }
            return new MatrixD(vals);
            
        }
        IMatrix IMatrix.Multiply(IMatrix inMatrix)
        {
            IMatrixD mat = inMatrix as IMatrixD;
            if (mat == null) throw new ArgumentException("Invalid Matrix provided for inMatrix");
            return Multiply(mat);
        }

        /// <summary>
        /// Multiplies this matrix by the specified scalar value.
        /// </summary>
        /// <param name="inScalar"></param>
        /// <returns></returns>
        public IMatrixD Multiply(double inScalar)
        {
            double[,] vals = new double[NumRows, NumColumns];
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumColumns; col++)
                {
                    vals[row, col] = _values[row, col] * inScalar;                   
                }
            }
            return new MatrixD(vals);
        }


        #endregion

        #region Properties

        
        /// <summary>
        /// Gets the number of rows
        /// </summary>
        public int NumRows
        {
            get { return _m; }
        }

        /// <summary>
        /// Gets the number of rows
        /// </summary>
        public int M
        {
            get { return _m; }
        }

        /// <summary>
        /// Gets the number of columns
        /// </summary>
        public int NumColumns
        {
            get { return _n; }
        }

        /// <summary>
        /// Gets the number of columns
        /// </summary>
        public int N
        {
            get { return _n; }
        }

        /// <summary>
        /// Gets or sets the values for this matrix
        /// </summary>
        public double[,] Values
        {
            get { return _values; }
            set
            {
                _values = value;
                _m = _values.GetLength(0);
                _n = _values.GetLength(1);
            }
        }



        #endregion

       
    }
}
