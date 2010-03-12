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
// The Initial Developer of this Original Code is Ted Dunsford. Created 2/20/2009 4:24:20 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************

using System;
using System.Collections.Generic;

namespace MapWindow.Main
{


    /// <summary>
    /// BaseList
    /// </summary>
    [Serializable]
    public class BaseList<T> : BaseCollection<T>, IList<T> where T:class
    {

       
      
        #region Constructors

        /// <summary>
        /// Creates a new instance of FeatureSchemeCategoryCollection
        /// </summary>
        public BaseList()
        {
        }

        #endregion

        #region Methods

    

        /// <summary>
        /// Gets the integer index of the specified item.
        /// </summary>
        /// <param name="item">The implementation of T to obtain the zero based integer index of.</param>
        /// <returns>An integer that is -1 if the item is not found, or the zero based index.</returns>
        public int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }

        /// <summary>
        /// If the read-only property is false, then this inserts the item to the specified index.
        /// </summary>
        /// <param name="index">The zero based integer index describing the target index for the item.</param>
        /// <param name="item">The implementation of T to insert into this index.</param>
        /// <exception cref="ReadOnlyException">If ReadOnly is true, then this method will cause an exception</exception>
        public virtual void Insert(int index, T item)
        {
            DoInsert(index, item);
        }

      

        #endregion

        #region Properties
      
       
        /// <summary>
        /// Gets or sets the value of type T at the specified index
        /// </summary>
        /// <param name="index">The zero-base integer index marking the position of the item</param>
        /// <returns>The item</returns>
        public T this[int index]
        {
            get
            {
                return InnerList[index] as T;
            }
            set
            {
                Exclude(InnerList[index] as T);
                Include(value);
                InnerList[index] = value;
                OnIncludeComplete(value);
            }
        }

        #endregion

        #region Protected Methods

     
      

        

        /// <summary>
        /// This happens after an item of type T was added to the list
        /// </summary>
        protected virtual void OnInsert(int index, T item)
        {
        }

        /// <summary>
        /// This happens after a value has been updated in the list.
        /// </summary>
        /// <param name="index">The index where the replacement took place.</param>
        /// <param name="oldItem">The item that was removed from the position.</param>
        /// <param name="newItem">The new item that is replacing it in the list.</param>
        protected virtual void OnItemSet(int index, T oldItem, T newItem)
        {

        }


        /// <summary>
        /// The happens after an item was removed from the specified index.
        /// </summary>
        /// <param name="index">The index the item was removed from.</param>
        /// <param name="item">The item that was removed</param>
        protected virtual void OnRemoveAt(int index, T item)
        {

        }



        #endregion

        #region Private Methods


        private void DoInsert(int index, T item)
        {
            if (IsReadOnly) throw new ReadOnlyException();
            Include(item);
            InnerList.Insert(index, item);
            OnInsert(index, item);
            OnIncludeComplete(item);
        }

      
        private void DoRemoveAt(int index)
        {
            if (IsReadOnly) throw new ReadOnlyException();
            T item = InnerList[index] as T;
            InnerList.RemoveAt(index);
            Exclude(item);
            OnRemoveAt(index, item);
        
        }

        private void DoItemSet(int index, T newItem)
        {
            if (IsReadOnly) throw new ReadOnlyException();
            T oldItem = InnerList[index] as T;
            InnerList[index] = newItem;
            Exclude(oldItem);
            Include(newItem);
            OnItemSet(index, oldItem, newItem);
        }

        #endregion

      
        #region IList<T> Members

        /// <summary>
        /// Removes the item from the specified index
        /// </summary>
        /// <param name="index">The zero based integer index</param>
        public void RemoveAt(int index)
        {
            T item = InnerList[index];
            InnerList.RemoveAt(index);
            OnExclude(item);
            
        }

        #endregion
    }
}