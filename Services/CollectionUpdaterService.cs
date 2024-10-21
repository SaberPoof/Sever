using System.Collections.Generic;
using System.Collections.ObjectModel;
public class CollectionUpdaterService
{
    /// <summary>
    /// Обновление данных в таблице. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetCollection"></param>
    /// <param name="sourceCollection"></param>
    public void UpdateCollection<T>(ObservableCollection<T> targetCollection, IEnumerable<T> sourceCollection)
    {
        targetCollection.Clear();
        foreach (var item in sourceCollection)
        {
            targetCollection.Add(item);
        }
    }
}
