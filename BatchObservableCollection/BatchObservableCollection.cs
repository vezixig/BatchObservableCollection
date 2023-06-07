namespace BatchObservableCollection
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    /// <summary>Extension of <see cref="ObservableCollection{T}" /> with added functionality.</summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public class BatchObservableCollection<T> : ObservableCollection<T>
    {
        #region Methods

        /// <summary>Adds a range to the collection notifying only after all items have been added.</summary>
        /// <param name="items">The items to add.</param>
        public void AddRange(IList<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            var startIndex = Count;

            foreach (var item in items) Items.Add(item);

            OnCollectionChanged(new(NotifyCollectionChangedAction.Add, items, startIndex));
            OnPropertyChanged(new(nameof(Count)));
            OnPropertyChanged(new(nameof(Items)));
        }

        /// <summary>Removes all items from the collection that satisfy the specified condition.</summary>
        /// <param name="evalFunc">A delegate that defines the condition to evaluate the items.</param>
        public void RemoveAll(Func<T, bool> evalFunc)
        {
            var itemsToRemove = Items.Where(evalFunc).ToList();
            itemsToRemove.ForEach(o => Items.Remove(o));

            OnCollectionChanged(new(NotifyCollectionChangedAction.Remove, itemsToRemove));
        }

        /// <summary>Replaces all items of a collection with a new collection of replacement items.</summary>
        /// <param name="replacement">The collection of replacement items.</param>
        public void Replace(IList<T> replacement)
        {
            // Clear triggers NotifyCollectionChangedAction.Reset
            Clear();
            AddRange(replacement);
        }

        /// <summary>Replaces an item with a new one.</summary>
        /// <param name="newElement">The new item.</param>
        /// <param name="oldElement">The old item to be replace.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the provided old item is not part of the collection.</exception>
        public void ReplaceElement(T newElement, T oldElement)
        {
            var index = Items.IndexOf(oldElement);
            if (index == -1) throw new ArgumentOutOfRangeException(nameof(oldElement));

            Items[index] = newElement;
            OnCollectionChanged(
                new(NotifyCollectionChangedAction.Replace, new List<T> { newElement }, new List<T> { oldElement })
            );
        }

        #endregion
    }
}