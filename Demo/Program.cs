using System.Collections.ObjectModel;
using BatchObservableCollection;

var myBatchCollection = new BatchObservableCollection<string>();
myBatchCollection.CollectionChanged += (_, e) => Console.WriteLine($"Batch Collection changed ({e.Action}) - has now {myBatchCollection.Count} items.");

var myCollection = new ObservableCollection<string>();
myCollection.CollectionChanged += (_, e) => Console.WriteLine($"Observable Collection changed ({e.Action}) - has now {myCollection.Count} items.");

// Add single items
Console.WriteLine("Adding single Item");
var val = Guid.NewGuid().ToString();
myBatchCollection.Add(val);
myCollection.Add(val);

// Add range
Console.WriteLine("\r\nAdding range");
var batch = new List<string>();
for (var i = 0; i < 10; i++) batch.Add(Guid.NewGuid().ToString());
myBatchCollection.AddRange(batch);
batch.ForEach(o => myCollection.Add(o));

// Replace
Console.WriteLine("\r\nReplacing all elements");
myBatchCollection.Replace(new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() });

// Replace Element
Console.WriteLine("\r\nReplacing single element");
myBatchCollection.ReplaceElement(Guid.NewGuid().ToString(), myBatchCollection.First());

// Remove Elements
Console.WriteLine("\r\nRemoving with evaluation function");
myBatchCollection.RemoveAll(o => o.Contains('1'));

Console.WriteLine("\r\nThis is the end. Press RETURN to exit.");
Console.ReadLine();