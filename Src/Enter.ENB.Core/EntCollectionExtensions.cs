using Enter.ENB.Statics;

namespace Enter.ENB;

public static class EntCollectionExtensions
  {
    /// <summary>
    /// Checks whatever given collection object is null or has no item.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T>? source) => source == null || source.Count <= 0;

    /// <summary>
    /// Adds an item to the collection if it's not already in the collection.
    /// </summary>
    /// <param name="source">The collection</param>
    /// <param name="item">Item to check and add</param>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <returns>Returns True if added, returns False if not.</returns>
    public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
    {
      if (source.Contains(item))
        return false;
      source.Add(item);
      return true;
    }

    /// <summary>
    /// Adds items to the collection which are not already in the collection.
    /// </summary>
    /// <param name="source">The collection</param>
    /// <param name="items">Item to check and add</param>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <returns>Returns the added items.</returns>
    public static IEnumerable<T> AddIfNotContains<T>(
      this ICollection<T> source,
      IEnumerable<T> items)
    {
      EntCheck.NotNull<ICollection<T>>(source, nameof (source));
      List<T> objList = new List<T>();
      foreach (T obj in items)
      {
        if (!source.Contains(obj))
        {
          source.Add(obj);
          objList.Add(obj);
        }
      }
      return (IEnumerable<T>) objList;
    }

    /// <summary>
    /// Adds an item to the collection if it's not already in the collection based on the given <paramref name="predicate" />.
    /// </summary>
    /// <param name="source">The collection</param>
    /// <param name="predicate">The condition to decide if the item is already in the collection</param>
    /// <param name="itemFactory">A factory that returns the item</param>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <returns>Returns True if added, returns False if not.</returns>
    public static bool AddIfNotContains<T>(
      this ICollection<T> source,
      Func<T, bool> predicate,
      Func<T> itemFactory)
    {
      EntCheck.NotNull<ICollection<T>>(source, nameof (source));
      EntCheck.NotNull<Func<T, bool>>(predicate, nameof (predicate));
      EntCheck.NotNull<Func<T>>(itemFactory, nameof (itemFactory));
      if (source.Any<T>(predicate))
        return false;
      source.Add(itemFactory());
      return true;
    }

    /// <summary>
    /// Removes all items from the collection those satisfy the given <paramref name="predicate" />.
    /// </summary>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <param name="source">The collection</param>
    /// <param name="predicate">The condition to remove the items</param>
    /// <returns>List of removed items</returns>
    public static IList<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
    {
      List<T> list = source.Where<T>(predicate).ToList<T>();
      foreach (T obj in list)
        source.Remove(obj);
      return (IList<T>) list;
    }

    /// <summary>Removes all items from the collection.</summary>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <param name="source">The collection</param>
    /// <param name="items">Items to be removed from the list</param>
    public static void RemoveAll<T>(this ICollection<T> source, IEnumerable<T> items)
    {
      foreach (T obj in items)
        source.Remove(obj);
    }
  }