namespace MapN;

public static class MappingEx
{
    public static T ToClone<T>(this T @this)
        where T : class, new() =>
        JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(@this)) ?? new T();

    public static T ToMerged<T>(this T @this, T incoming)
        where T : class, new() =>
        @this.ToDictionary().ToMerged(incoming.ToDictionary()).ToObject<T>();

    public static O ToOther<T, O>(this T @this)
        where T : class
        where O : class, new() =>
        @this.ToDictionary().ToObject<O>() ?? new O();

    private static T ToObject<T>(this IDictionary<string, object> @this)
        where T : class, new() =>
        JsonConvert.DeserializeObject<T>(@this.ToJson()) ?? new T();

    private static string ToJson(this IDictionary<string, object> @this) =>
        JsonConvert.SerializeObject(@this);

    private static IDictionary<string, object> ToDictionary(this object @this) =>
        JsonConvert.DeserializeObject<IDictionary<string, object>>(
            JsonConvert.SerializeObject(@this)) ?? new Dictionary<string, object>();

    private static IDictionary<string, object> ToMerged(
        this IDictionary<string, object> @this,
        IDictionary<string, object> incoming)
    {
        foreach (var kvp in incoming)
        {
            if (kvp.Value != null)
            {
                if (!@this.ContainsKey(kvp.Key))
                    @this.Add(kvp.Key, kvp.Value);
                else
                    @this[kvp.Key] = kvp.Value;
            }
        }
        return @this;
    }

}
