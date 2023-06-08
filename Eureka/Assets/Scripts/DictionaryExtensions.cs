using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
    /// <summary>
    /// Dictionary 型のインスタンスからランダムに値を取得します
	/// http://baba-s.hatenablog.com/entry/2014/12/24/212921
    /// </summary>
    public static TValue ElementAtRandom<TKey, TValue>( 
        this Dictionary<TKey, TValue> self 
    )
    {
        return self.ElementAt( UnityEngine.Random.Range( 0, self.Count ) ).Value;
    }
}